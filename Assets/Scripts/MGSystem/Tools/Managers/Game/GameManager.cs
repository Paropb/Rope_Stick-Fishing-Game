using MyGame.MGEntity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MyGame.MGSystem
{
	public enum GameStates
    {
		Gameplay,
		PlayerDeath
    }
 //   /// <summary>
 //   /// A list of the possible Corgi Engine base events
 //   /// LevelStart : triggered by the LevelManager when a level starts
 //   ///	LevelComplete : can be triggered when the end of a level is reached
 //   /// LevelEnd : same thing
 //   ///	Pause : triggered when a pause is starting
 //   ///	UnPause : triggered when a pause is ending and going back to normal
 //   ///	PlayerDeath : triggered when the player character dies
 //   ///	Respawn : triggered when the player character respawns
 //   ///	StarPicked : triggered when a star bonus gets picked
 //   ///	GameOver : triggered by the LevelManager when all lives are lost
 //   /// CharacterSwitch : triggered when the character gets switched
 //   /// CharacterSwap : triggered when the character gets swapped
 //   /// TogglePause : triggered to request a pause (or unpause)
 //   /// </summary>
 //   public enum RobotGameEventTypes
 //   {
 //       SpawnCharacterStarts,
 //       LevelStart,
 //       LevelComplete,
 //       LevelEnd,
 //       Pause,
 //       UnPause,
 //       PlayerDeath,
 //       Respawn,
 //       StarPicked,
 //       GameOver,
 //       CharacterSwitch,
 //       CharacterSwap,
 //       TogglePause,
 //       LoadNextScene,
 //       LevelEdit,
 //       LevelResume
 //   }

 //   /// <summary>
	///// A type of events used to signal level start and end (for now)
	///// </summary>
	//public struct RobotGameEvent
 //   {
 //       public RobotGameEventTypes EventType;

 //       /// <summary>
 //       /// Initializes a new instance of the <see cref="MoreMountains.RobotGame.RobotGameEvent"/> struct.
 //       /// </summary>
 //       /// <param name="eventType">Event type.</param>
 //       public RobotGameEvent(RobotGameEventTypes eventType)
 //       {
 //           Debug.Log("RobotGameEvent---RobotGameEvent");
 //           EventType = eventType;
 //       }

 //       static RobotGameEvent e;

 //       public static void Trigger(RobotGameEventTypes eventType)
 //       {
 //           e.EventType = eventType;
 //           RGEventManager.TriggerEvent(e);
 //       }
 //   }

 //   /// <summary>
 //   /// A list of the methods available to change the current score
 //   /// </summary>
 //   public enum PointsMethods
 //   {
 //       Add,
 //       Set
 //   }

 //   public struct RobotGameStarEvent
 //   {
 //       public string SceneName;
 //       public int StarID;

 //       public RobotGameStarEvent(string sceneName, int starID)
 //       {
 //           Debug.Log("RobotGameStarEvent---RobotGameStarEvent");
 //           SceneName = sceneName;
 //           StarID = starID;
 //       }

 //       static RobotGameStarEvent e;
 //       public static void Trigger(string sceneName, int starID)
 //       {
 //           Debug.Log("RobotGameStarEvent---Trigger");
 //           e.SceneName = sceneName;
 //           e.StarID = starID;
 //           RGEventManager.TriggerEvent(e);
 //       }

 //   }

 //   /// <summary>
	///// A type of event used to signal changes to the current score
	///// </summary>
	//public struct RobotGamePointsEvent
 //   {
 //       public PointsMethods PointsMethod;
 //       public int Points;

 //       /// <summary>
 //       /// Initializes a new instance of the <see cref="MoreMountains.RobotGame.RobotGamePointsEvent"/> struct.
 //       /// </summary>
 //       /// <param name="pointsMethod">Points method.</param>
 //       /// <param name="points">Points.</param>
 //       public RobotGamePointsEvent(PointsMethods pointsMethod, int points)
 //       {
 //           Debug.Log("RobotGamePointsEvent---RobotGamePointsEvent");
 //           PointsMethod = pointsMethod;
 //           Points = points;
 //       }
 //       static RobotGamePointsEvent e;

 //       public static void Trigger(PointsMethods pointsMethod, int points)
 //       {
 //           Debug.Log("RobotGamePointsEvent---Trigger");
 //           e.PointsMethod = pointsMethod;
 //           e.Points = points;
 //           RGEventManager.TriggerEvent(e);
 //       }
 //   }

 //   public enum PauseMethods
 //   {
 //       PauseMenu,
 //       NoPauseMenu
 //   }

 //   public class PointsOfEntryStorage
 //   {
 //       public string LevelName;
 //       public int PointOfEntryIndex;
 //       public RGCharacter.FacingDirections FacingDirection;

 //       public PointsOfEntryStorage(string levelName, int pointOfEntryIndex, RGCharacter.FacingDirections facingDirection)
 //       {
 //           Debug.Log("PointsOfEntryStorage---PointsOfEntryStorage");
 //           LevelName = levelName;
 //           FacingDirection = facingDirection;
 //           PointOfEntryIndex = pointOfEntryIndex;
 //       }
 //   }
    public class GameManager : PersistentSingleton<GameManager>
                               //IRGEventListener<RGGameEvent>,
                               //IRGEventListener<RobotGameEvent>,
                               //IRGEventListener<RobotGamePointsEvent>
    {
        [Header("Constants"), InlineEditor]
        public ConstantDataSO ConstantData;

		protected float _timer;

		public Player Player { get; private set; }
		public void SetPlayer(Player Player)
        {
			this.Player = Player;
        }
        private void Update()
        {
			_timer += Time.deltaTime;

			if(InputHandler.Instance.R.Started)
            {
				SceneManager.LoadScene("SampleScene");
            }
        }
  //      [Header("Settings")]
  //      /// the target frame rate for the game
  //      [Tooltip("the target frame rate for the game")]
  //      public int TargetFrameRate = 300;

  //      [Header("Lives")]
  //      /// the maximum amount of lives the character can currently have
		//[Tooltip("the maximum amount of lives the character can currently have")]
  //      public int MaximumLives = 0;
  //      /// the current number of lives 
  //      [Tooltip("the current number of lives ")]
  //      public int CurrentLives = 0;

  //      [Header("Game Over")]
  //      /// if this is true, lives will be reset on game over
  //      [Tooltip("if this is true, lives will be reset on game over")]
  //      public bool ResetLivesOnGameOver = true;
  //      /// if this is true, the persistent character will be cleared on game over
  //      [Tooltip("if this is true, the persistent character will be cleared on game over")]
  //      public bool ResetPersistentCharacterOnGameOver = true;
  //      /// if this is true, the stored character will be cleared on game over
  //      [Tooltip("if this is true, the stored character will be cleared on game over")]
  //      public bool ResetStoredCharacterOnGameOver = true;
  //      /// the name of the scene to redirect to when all lives are lost
  //      [Tooltip("the name of the scene to redirect to when all lives are lost")]
  //      public string GameOverScene;

  //      /// the current number of game points
		//public int Points { get; private set; }
  //      /// true if the game is currently paused
  //      public bool Paused { get; set; }
  //      // true if we've stored a map position at least once
  //      public bool StoredLevelMapPosition { get; set; }
  //      /// the current player
  //      public Vector2 LevelMapPosition { get; set; }
  //      /// the stored selected character
  //      public RGCharacter StoredCharacter { get; set; }
  //      /// the stored selected character
  //      public RGCharacter PersistentCharacter { get; set; }
  //      /// the list of points of entry and exit
  //      public List<PointsOfEntryStorage> PointsOfEntry { get; set; }

  //      protected bool _inventoryOpen = false;
  //      protected bool _pauseMenuOpen = false;
  //      protected RGInventoryInputManager _inventoryInputManager;
  //      protected int _initialMaximumLives;
  //      protected int _initialCurrentLives;

  //      protected override void Awake()
  //      {
  //          base.Awake();

  //          if(Application.isPlaying)
  //          {
  //              //Cursor.visible = false;
  //          }
  //      }
  //      protected virtual void Update()
  //      {
            
  //      }
  //      /// <summary>
  //      /// On Start(), sets the target framerate to whatever's been specified
  //      /// </summary>
  //      protected virtual void Start()
  //      {
  //          Debug.Log("GameManager-----Start");
  //      }

  //      /// <summary>
  //      /// this method resets the whole game manager
  //      /// </summary>
  //      public virtual void Reset()
  //      {
  //          Debug.Log("GameManager-----Reset");
  //      }

  //      /// <summary>
  //      /// Use this method to decrease the current number of lives
  //      /// </summary>
  //      public virtual void LoseLife()
  //      {
  //          Debug.Log("GameManager-----LoseLife");
  //      }

  //      /// <summary>
  //      /// Use this method when a life (or more) is gained
  //      /// </summary>
  //      /// <param name="lives">Lives.</param>
  //      public virtual void GainLives(int lives)
  //      {
  //          Debug.Log("GameManager-----GainLives");
  //      }

  //      /// <summary>
  //      /// Use this method to increase the max amount of lives, and optionnally the current amount as well
  //      /// </summary>
  //      /// <param name="lives">Lives.</param>
  //      /// <param name="increaseCurrent">If set to <c>true</c> increase current.</param>
  //      public virtual void AddLives(int lives, bool increaseCurrent)
  //      {
  //          Debug.Log("GameManager-----AddLives");
  //      }

  //      /// <summary>
		///// Resets the number of lives to their initial values.
		///// </summary>
		//public virtual void ResetLives()
  //      {
  //          Debug.Log("GameManager-----ResetLives");
  //      }

  //      /// <summary>
  //      /// Adds the points in parameters to the current game points.
  //      /// </summary>
  //      /// <param name="pointsToAdd">Points to add.</param>
  //      public virtual void AddPoints(int pointsToAdd)
  //      {
  //          Debug.Log("GameManager-----AddPoints");
  //      }

  //      /// <summary>
		///// use this to set the current points to the one you pass as a parameter
		///// </summary>
		///// <param name="points">Points.</param>
		//public virtual void SetPoints(int points)
  //      {
  //          Debug.Log("GameManager-----SetPoints");
  //      }

  //      protected virtual void SetActiveInventoryInputManager(bool status)
  //      {
  //          Debug.Log("GameManager-----SetActiveInventoryInputManager");
  //      }


  //      /// <summary>
		///// Pauses the game or unpauses it depending on the current state
		///// </summary>
		//public virtual void Pause(PauseMethods pauseMethod = PauseMethods.PauseMenu)
  //      {
  //          Debug.Log("GameManager-----Pause");
  //      }

  //      /// <summary>
  //      /// Unpauses the game
  //      /// </summary>
  //      public virtual void UnPause(PauseMethods pauseMethod = PauseMethods.PauseMenu)
  //      {
  //          Debug.Log("GameManager-----UnPause");
  //      }

  //      /// <summary>
  //      /// Deletes all save files
  //      /// </summary>
  //      public virtual void ResetAllSaves()
  //      {
  //          Debug.Log("GameManager---ResetAllSaves");
  //      }

  //      /// <summary>
  //      /// Stores the points of entry for the level whose name you pass as a parameter.
  //      /// </summary>
  //      /// <param name="levelName">Level name.</param>
  //      /// <param name="entryIndex">Entry index.</param>
  //      /// <param name="exitIndex">Exit index.</param>
  //      public virtual void StorePointsOfEntry(string levelName, int entryIndex, RGCharacter.FacingDirections facingDirection)
  //      {
  //          Debug.Log("GameManager---StorePointsOfEntry");
  //      }

  //      /// <summary>
		///// Gets point of entry info for the level whose scene name you pass as a parameter
		///// </summary>
		///// <returns>The points of entry.</returns>
		///// <param name="levelName">Level name.</param>
		//public virtual PointsOfEntryStorage GetPointsOfEntry(string levelName)
  //      {
  //          Debug.Log("GameManager---GetPointsOfEntry");
  //          return null;
  //      }

  //      /// <summary>
		///// Clears the stored point of entry infos for the level whose name you pass as a parameter
		///// </summary>
		///// <param name="levelName">Level name.</param>
		//public virtual void ClearPointOfEntry(string levelName)
  //      {
  //          Debug.Log("GameManager---ClearPointOfEntry");
  //      }

  //      /// <summary>
  //      /// Clears all points of entry.
  //      /// </summary>
  //      public virtual void ClearAllPointsOfEntry()
  //      {
  //          Debug.Log("GameManager---ClearAllPointsOfEntry");
  //      }

  //      /// <summary>
  //      /// Sets a new persistent character
  //      /// </summary>
  //      /// <param name="newCharacter"></param>
  //      public virtual void SetPersistentCharacter(RGCharacter newCharacter)
  //      {
  //          Debug.Log("GameManager---SetPersistentCharacter");
  //      }

  //      /// <summary>
  //      /// Destroys a persistent character if there's one
  //      /// </summary>
  //      public virtual void DestroyPersistentCharacter()
  //      {
  //          Debug.Log("GameManager---DestroyPersistentCharacter");
  //      }


  //      /// <summary>
		///// Stores the selected character for use in upcoming levels
		///// </summary>
		///// <param name="selectedCharacter">Selected character.</param>
		//public virtual void StoreSelectedCharacter(RGCharacter selectedCharacter)
  //      {
  //          Debug.Log("GameManager---StoreSelectedCharacter");
  //      }


  //      /// <summary>
		///// Clears the selected character.
		///// </summary>
		//public virtual void ClearStoredCharacter()
  //      {
  //          Debug.Log("GameManager---ClearStoredCharacter");
  //      }


  //      /// <summary>
		///// Catches RGGameEvents and acts on them, playing the corresponding sounds
		///// </summary>
		///// <param name="gameEvent">RGGameEvent event.</param>
  //      public void OnRGEvent(RGGameEvent gameEvent)
  //      {
  //          switch (gameEvent.EventName)
  //          {
  //              case "inventoryOpens":
  //                  Debug.Log("GameManager---inventoryOpens");
  //                  break;

  //              case "inventoryCloses":
  //                  Debug.Log("GameManager---inventoryCloses");
  //                  break;
  //          }
  //      }

  //      /// <summary>
  //      /// Catches RobotGameEvents and acts on them, playing the corresponding sounds
  //      /// </summary>
  //      /// <param name="engineEvent">RobotGameEvent event.</param>
  //      public void OnRGEvent(RobotGameEvent engineEvent)
  //      {
  //          switch (engineEvent.EventType)
  //          {
  //              case RobotGameEventTypes.TogglePause:
  //                  Debug.Log("GameManager---TogglePause");
  //                  break;

  //              case RobotGameEventTypes.Pause:
  //                  Debug.Log("GameManager---Pause");
  //                  break;

  //              case RobotGameEventTypes.UnPause:
  //                  Debug.Log("GameManager---UnPause");
  //                  break;
  //          }
  //      }

  //      /// <summary>
  //      /// Catches RobotGamePointsEvents and acts on them, playing the corresponding sounds
  //      /// </summary>
  //      /// <param name="pointEvent">RobotGamePointsEvent event.</param>
  //      public void OnRGEvent(RobotGamePointsEvent pointEvent)
  //      {
  //          switch (pointEvent.PointsMethod)
  //          {
  //              case PointsMethods.Set:
  //                  Debug.Log("GameManager---Set");
  //                  break;

  //              case PointsMethods.Add:
  //                  Debug.Log("GameManager---Add");
  //                  break;
  //          }
  //      }

  //      /// <summary>
  //      /// On enable, we start listening to events
  //      /// </summary>
  //      protected virtual void OnEnable()
  //      {
  //          this.RGEventStartListening<RGGameEvent>();
  //          this.RGEventStartListening<RobotGameEvent>();
  //          this.RGEventStartListening<RobotGamePointsEvent>();
  //          Cursor.visible = true;
  //      }

  //      /// <summary>
  //      /// On disable, we stop listening to events
  //      /// </summary>
  //      protected virtual void OnDisable()
  //      {
  //          this.RGEventStopListening<RGGameEvent>();
  //          this.RGEventStopListening<RobotGameEvent>();
  //          this.RGEventStopListening<RobotGamePointsEvent>();
  //      }
    }
}
