using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

namespace MyGame.MGSystem
{
    [Serializable]
    public class ProgressEvent : UnityEvent<float> { }


    /// <summary>
    /// A simple class used to store additive loading settings
    /// </summary>
    [Serializable]
    public class RGAdditiveSceneLoadingManagerSettings
    {

    }

    /// <summary>
	/// A class to load scenes using a loading screen instead of just the default API
	/// This is a new version of the classic LoadingSceneManager (now renamed to RGSceneLoadingManager for consistency)
	/// </summary>
    public class RGAdditiveSceneLoadingManager : MonoBehaviour
    {

    }
}
