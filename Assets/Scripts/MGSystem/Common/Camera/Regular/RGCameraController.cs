using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace MyGame.MGSystem
{
    [RequireComponent(typeof(Camera))]
    /// <summary>
    /// The Corgi Engine's Camera Controller. Handles camera movement, shakes, player follow.
    /// </summary>
    [AddComponentMenu("Robot Game/Camera/Camera Controller")]
    public class RGCameraController : Singleton<RGCameraController>
    {
        protected CinemachineBrain _cinemachineBrain;
        public GameObject InitialVirtualCamera;
        public GameObject CurrentActiveVirtualCamera { get => _cinemachineBrain.ActiveVirtualCamera.VirtualCameraGameObject; }
        protected override void Awake()
        {
            base.Awake();

            _cinemachineBrain = GetComponent<CinemachineBrain>();
        }
        private void Initialization()
        {
            
        }
        /// <summary>
        /// Switch to the given virtual cam from current one
        /// </summary>
        /// <param name="switchTo"></param>
	    public void SwitchCamera(GameObject switchTo)
        {
            CurrentActiveVirtualCamera.SetActive(false);
            switchTo.SetActive(true);
        }
    }
}
