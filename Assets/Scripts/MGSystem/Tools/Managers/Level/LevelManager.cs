using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace MyGame.MGSystem
{
    public struct LevelNameEvent
    {
        public string LevelName;

        /// <summary>
        /// Initializes a LevelNameEvent
        /// </summary>
        /// <param name="levelName"></param>
        public LevelNameEvent(string levelName)
        {
            Debug.Log("LevelNameEvent------LevelNameEvent");
            LevelName = levelName;
        }

        static LevelNameEvent e;
        public static void Trigger(string levelName)
        {
            Debug.Log("LevelNameEvent------Trigger");
            e.LevelName = levelName;
            RGEventManager.TriggerEvent(e);
        }
    }

    /// <summary>
    /// A struct to describe and store points of entry
    /// </summary>
    [Serializable]
    public struct PointOfEntry
    {
        public string Name;
        public Transform Position;
        public RGFeedbacks EntryFeedback;
    }
    /// <summary>
    /// Spawns the player, handles checkpoints and respawn
    /// </summary>
    [AddComponentMenu("Corgi Engine/Managers/Level Manager")]
    public class LevelManager : Singleton<LevelManager>
    {
        /// the possible checkpoint axis
		public enum CheckpointsAxis { x, y, z, CheckpointOrder }
        public enum CheckpointDirections { Ascending, Descending }
        public enum BoundsModes { TwoD, ThreeD }

        /// the prefab you want for your player
		[Header("Instantiate Characters")]
        [RGInformation("The LevelManager is responsible for handling spawn/respawn, checkpoints management and level bounds. Here you can define one or more playable characters for your level..", RGInformationAttribute.InformationType.Info, false)]

        /// the list of player prefabs to instantiate
        [Tooltip("the list of player prefabs to instantiate")]
        public RGCharacter[] PlayerPrefabs;
        /// should the player IDs be auto attributed (usually yes)
		[Tooltip("should the player IDs be auto attributed (usually yes)")]
        public bool AutoAttributePlayerIDs = true;


        [Header("Characters already in the scene")]
        [RGInformation("It's recommended to have the LevelManager instantiate your characters, but if instead you'd prefer to have them already present in the scene, just bind them in the list below.", RGInformationAttribute.InformationType.Info, false)]
        /// a list of Characters already present in the scene before runtime. If this list is filled, PlayerPrefabs will be ignored
		[Tooltip("a list of Characters already present in the scene before runtime. If this list is filled, PlayerPrefabs will be ignored")]
        public List<RGCharacter> SceneCharacters;

        [Header("Checkpoints")]
        [RGInformation("Here you can select a checkpoint attribution axis (if your level is horizontal go for X, Y if it's vertical), and a debug spawn where your player character will spawn from while in editor mode.", RGInformationAttribute.InformationType.Info, false)]
        /// A checkpoint to use to force the character to spawn at
		[Tooltip("A checkpoint to use to force the character to spawn at")]
        public RGCheckPoint DebugSpawn;
        /// the axis on which objects should be compared
		[Tooltip("the axis on which objects should be compared")]
        public CheckpointsAxis CheckpointAttributionAxis = CheckpointsAxis.x;
        /// the direction in which checkpoint order should be determined
        [Tooltip("the direction in which checkpoint order should be determined")]
        public CheckpointDirections CheckpointAttributionDirection = CheckpointDirections.Ascending;

        /// the current checkpoint
		[Tooltip("the current checkpoint")]
        [RGReadOnly]
        public RGCheckPoint CurrentCheckPoint;
        [Space(10)]
        [Header("Points of Entry")]

        /// a list of all the points of entry for this level
        [Tooltip("a list of all the points of entry for this level.")]
        public List<PointOfEntry> PointsOfEntry;


        [Space(10)]
        [Header("Intro and Outro durations")]
        [RGInformation("Here you can specify the length of the fade in and fade out at the start and end of your level. You can also determine the delay before a respawn.", RGInformationAttribute.InformationType.Info, false)]

        /// duration of the initial fade in (in seconds)
		[Tooltip("duration of the initial fade in (in seconds)")]
        public float IntroFadeDuration = 1f;
        /// duration of the fade to black at the end of the level (in seconds)
        [Tooltip("duration of the fade to black at the end of the level (in seconds)")]
        public float OutroFadeDuration = 1f;
        /// the ID to use when triggering the event (should match the ID on the fader you want to use)
        [Tooltip("the ID to use when triggering the event (should match the ID on the fader you want to use)")]
        public int FaderID = 0;
        /// the curve to use for in and out fades
        [Tooltip("the curve to use for in and out fades")]
        public RGTweenType FadeTween = new RGTweenType(RGTween.RGTweenCurve.EaseInOutCubic);
        /// duration between a death of the main character and its respawn
		[Tooltip("duration between a death of the main character and its respawn")]
        public float RespawnDelay = 2f;


        [Space(10)]
        [Header("Level Bounds")]
        [RGInformation("The level bounds are used to constrain the camera's movement, as well as the player character's. You can see it in real time in the scene view as you adjust its size (it's the yellow box).", RGInformationAttribute.InformationType.Info, false)]


        /// whether to use a 3D or 2D collider as level bounds, this will be used by Cinemachine confiners
        [Tooltip("whether to use a 3D or 2D collider as level bounds")]
        public BoundsModes BoundsMode = BoundsModes.ThreeD;
        /// the level limits, camera and player won't go beyond this point.
		[Tooltip("the level limits, camera and player won't go beyond this point.")]
        public Bounds LevelBounds = new Bounds(Vector3.zero, Vector3.one * 10);


        [RGInspectorButton("GenerateColliderBounds")]
        public bool ConvertToColliderBoundsButton;
        public Collider BoundsCollider { get; protected set; }
        public Collider2D BoundsCollider2D { get; protected set; }

        [Header("Scene Loading")]
        public bool SkipLoading = true;
        /// the method to use to load the destination level
        [Tooltip("the method to use to load the destination level")]
        public RGLoadScene.LoadingSceneModes LoadingSceneMode = RGLoadScene.LoadingSceneModes.RGSceneLoadingManager;
        /// the name of the RGSceneLoadingManager scene you want to use
        [Tooltip("the name of the RGSceneLoadingManager scene you want to use")]
        [RGEnumCondition("LoadingSceneMode", (int)RGLoadScene.LoadingSceneModes.RGSceneLoadingManager)]
        public string LoadingSceneName = "LoadingScreen";
        /// the settings to use when loading the scene in additive mode
        [Tooltip("the settings to use when loading the scene in additive mode")]
        [RGEnumCondition("LoadingSceneMode", (int)RGLoadScene.LoadingSceneModes.RGAdditiveSceneLoadingManager)]
        public RGAdditiveSceneLoadingManagerSettings AdditiveLoadingSettings;

        /// the elapsed time since the start of the level
        public TimeSpan RunningTime { get { return DateTime.UtcNow - _started; } }
        //public CameraController LevelCameraController { get; set; }

        // private stuff
        public List<RGCharacter> Players { get; protected set; }
        public List<RGCheckPoint> Checkpoints { get; protected set; }
        protected DateTime _started;
        protected int _savedPoints;
        protected string _nextLevel = null;
        protected BoxCollider _collider;
        protected BoxCollider2D _collider2D;
        protected Bounds _originalBounds;
        /// <summary>
        /// On awake, instantiates the player
        /// </summary>
        protected override void Awake()
        {
            Debug.Log("LevelManager------Awake");
        }

        /// <summary>
        /// Instantiate playable characters based on the ones specified in the PlayerPrefabs list in the LevelManager's inspector.
        /// </summary>
        protected virtual void InstantiatePlayableCharacters()
        {
            // we check if there's a stored character in the game manager we should instantiate

            // player instantiation
            Debug.Log("LevelManager------InstantiatePlayableCharacters");
        }

        protected virtual void OnEnable()
        {
        }
        protected virtual void OnDisable()
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        public void Start()
        {
            // we handle the spawn of the character(ss)
            RGFadeOutEvent.Trigger(OutroFadeDuration, FadeTween, FaderID, true, RGFader.FadeTypes.CloseDoor);

            // we trigger a level start event
            Debug.Log("LevelManager------Start");

        }

        /// <summary>
        /// Gets current camera, points number, start time, etc.
        /// </summary>
        protected virtual void Initialization()
        {
            // storage����ȡGameManager.Instance.Points ��Ϸ�е�save�ļ���

            // if we don't find a bounds collider we generate one

            // ����CheckpointsAxis.x/y/z/CheckpointOrder ���CheckpointDirections.Ascending/Descending 
            // �Թؿ��еļ�������󣬷ŵ�CheckPoints�������棬�Ժ�ʹ��

            // we assign the first checkpoint
            Debug.Log("LevelManager------Initialization");
        }

        /// <summary>
		/// Assigns all respawnable objects in the scene to their checkpoint
		/// </summary>
		protected virtual void CheckpointAssignment()
        {
            // we get all respawnable objects in the scene and attribute them to their corresponding checkpoint
            // ��ɫ��������Ҫ������ļ��㸴���Ҫ�ڸ�������������� respawnable ���
            Debug.Log("LevelManager------CheckpointAssignment");
        }

        /// <summary>
		/// Initializes GUI stuff
		/// </summary>
		protected virtual void LevelGUIStart()
        {
            // set the level name in the GUI

            // fade in
            Debug.Log("LevelManager------LevelGUIStart");
        }

        /// <summary>
        /// Spawns a playable character into the scene
        /// </summary>
        protected virtual void SpawnSingleCharacter()
        {
            Debug.Log("LevelManager------SpawnSingleCharacter");
        }

        /// <summary>
		/// Spawns the character at the selected entry point if there's one, or at the selected checkpoint.
		/// </summary>
		protected virtual void RegularSpawnSingleCharacter()
        {
            Debug.Log("LevelManager------RegularSpawnSingleCharacter");
        }

        /// <summary>
        /// Spawns multiple playable characters into the scene
        /// </summary>
        protected virtual void SpawnMultipleCharacters()
        {
            Debug.Log("LevelManager------SpawnMultipleCharacters");
        }


        /// <summary>
		/// Sets the current checkpoint.
		/// </summary>
		/// <param name="newCheckPoint">New check point.</param>
		public virtual void SetCurrentCheckpoint(RGCheckPoint newCheckPoint)
        {
            Debug.Log("LevelManager------SetCurrentCheckpoint");
        }

        /// <summary>
        /// Sets the name of the next level this LevelManager will point to
        /// </summary>
        /// <param name="levelName"></param>
        public virtual void SetNextLevel(string levelName)
        {
            Debug.Log("LevelManager------SetNextLevel");
        }

        /// <summary>
        /// Loads the next level, as defined via the SetNextLevel method
        /// </summary>
        public virtual void GotoNextLevel()
        {
            Debug.Log("LevelManager------GotoNextLevel");
        }


        /// <summary>
        /// Gets the player to the specified level
        /// </summary>
        /// <param name="levelName">Level name.</param>
        public virtual void GotoLevel(string levelName, bool fadeOut = true, bool save = true)
        {
            //RobotGameEvent.Trigger(RobotGameEventTypes.LevelEnd);
            if (save)
            {
                RGGameEvent.Trigger("Save");
            }
            if (fadeOut)
            {
                if ((Players != null) && (Players.Count > 0))
                {
                    //�Ժ�Ὺ����Fade���ս�ɫ��λ�ã���һ��Ȧ�����л�����
                    Debug.Log("LevelManager-----GotoLevel-----1");
                }
                else
                {
                    RGFadeInEvent.Trigger(OutroFadeDuration, FadeTween, FaderID, true, Vector3.zero, RGFader.FadeTypes.CloseDoor);
                }
            }
            StartCoroutine(GotoLevelCo(levelName, fadeOut));
        }

        /// <summary>
        /// Waits for a short time and then loads the specified level
        /// </summary>
        /// <returns>The level co.</returns>
        /// <param name="levelName">Level name.</param>
        protected virtual IEnumerator GotoLevelCo(string levelName, bool fadeOut = true)
        {
            if (Players != null && Players.Count > 0)
            {
                Debug.Log("LevelManager-----GotoLevelCo-----1");
            }
            if (fadeOut)
            {
                if (Time.timeScale > 0.0f)
                {
                    yield return new WaitForSeconds(OutroFadeDuration);
                }
                else
                {
                    Debug.Log("LevelManager-----GotoLevelCo-----3");
                    yield return new WaitForSecondsRealtime(OutroFadeDuration);
                }
            }
            // we trigger an unPause event for the GameManager (and potentially other classes)
            //RobotGameEvent.Trigger(RobotGameEventTypes.UnPause);
            //RobotGameEvent.Trigger(RobotGameEventTypes.LoadNextScene);
            string destinationScene = (string.IsNullOrEmpty(levelName)) ? "StartScreen" : levelName;
            switch (LoadingSceneMode)
            {
                case RGLoadScene.LoadingSceneModes.UnityNative:
                    Debug.Log("LevelManager-----GotoLevelCo-----2");
                    break;
                case RGLoadScene.LoadingSceneModes.RGSceneLoadingManager:
                    RGSceneLoadingManager.LoadScene(destinationScene, LoadingSceneName, RGFader.FadeTypes.CloseDoor, skipLoading: SkipLoading);
                    break;
                case RGLoadScene.LoadingSceneModes.RGAdditiveSceneLoadingManager:
                    Debug.Log("LevelManager-----GotoLevelCo-----4");
                    break;
            }
        }

        /// <summary>
		/// Kills the player.
		/// </summary>
		public virtual void KillPlayer(RGCharacter player)
        {
            Debug.Log("LevelManager------KillPlayer");
        }

        /// <summary>
		/// Resets lives, removes persistent characters and stored ones if needed
		/// </summary>
		protected virtual void Cleanup()
        {
            Debug.Log("LevelManager------Cleanup");

        }

        /// <summary>
	    /// Coroutine that kills the player, stops the camera, resets the points.
	    /// </summary>
	    /// <returns>The player co.</returns>
	    protected virtual IEnumerator SoloModeRestart()
        {
            // we send a new points event for the GameManager to catch (and other classes that may listen to it too)

            // we trigger a respawn event
            Debug.Log("LevelManager------SoloModeRestart");
            return null;
        }

        /// <summary>
        /// Freezes the character(s)
        /// </summary>
        public virtual void FreezeCharacters()
        {
            Debug.Log("LevelManager------FreezeCharacters");
        }

        /// <summary>
        /// Unfreezes the character(s)
        /// </summary>
        public virtual void UnFreezeCharacters()
        {
            Debug.Log("LevelManager------UnFreezeCharacters");
        }

        /// <summary>
        /// Toggles Character Pause
        /// </summary>
        public virtual void ToggleCharacterPause()
        {
            Debug.Log("LevelManager------ToggleCharacterPause");
        }

        /// <summary>
        /// Resets the level bounds to their initial value
        /// </summary>
        public virtual void ResetLevelBoundsToOriginalBounds()
        {
            Debug.Log("LevelManager------ResetLevelBoundsToOriginalBounds");
        }

        /// <summary>
        /// Sets the level bound's min point to the one in parameters
        /// </summary>
        public virtual void SetNewMinLevelBounds(Vector3 newMinBounds)
        {
            Debug.Log("LevelManager------SetNewMinLevelBounds");
        }

        /// <summary>
        /// Sets the level bound's max point to the one in parameters
        /// </summary>
        /// <param name="newMaxBounds"></param>
        public virtual void SetNewMaxLevelBounds(Vector3 newMaxBounds)
        {
            Debug.Log("LevelManager------SetNewMaxLevelBounds");
        }

        /// <summary>
        /// Sets the level bounds to the one passed in parameters
        /// </summary>
        /// <param name="newBounds"></param>
        public virtual void SetNewLevelBounds(Bounds newBounds)
        {
            Debug.Log("LevelManager------SetNewLevelBounds");
        }

        /// <summary>
        /// Updates the level collider's bounds for Cinemachine (and others that may use it)
        /// </summary>
        protected virtual void UpdateBoundsCollider()
        {
            Debug.Log("LevelManager------UpdateBoundsCollider");
        }


        /// <summary>
        /// A temporary method used to convert level bounds from the old system to actual collider bounds
        /// </summary>
        [ExecuteAlways]
        protected virtual void GenerateColliderBounds()
        {
            Debug.Log("LevelManager------GenerateColliderBounds");
        }
        #region Events
        #endregion
    }
}
