using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGSystem
{
    [AddComponentMenu("Robot Game/System/GUI/GUIManager")]
    public class GUIManager : Singleton<GUIManager>, IRGEventListener<RGGameEvent>
    {
        public void OnRGEvent(RGGameEvent eventType)
        {
            
        }
        protected virtual void OnEnable()
        {
            this.RGEventStartListening<RGGameEvent>();
        }
        protected virtual void OnDisable()
        {
            this.RGEventStopListening<RGGameEvent>();
        }
        protected virtual void Start()
        {

        }
    }
}
