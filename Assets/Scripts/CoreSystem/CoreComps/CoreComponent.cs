using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.CoreSystem
{
    public class CoreComponent : MonoBehaviour
    {
        protected Core Core { get; private set; }
        #region Unity CallBacks
        protected virtual void OnEnable() { }
        protected virtual void OnDisable() { }
        #endregion
        public virtual void MGAwake(Core core)
        {
            this.Core = core;
        }
        public virtual void MGUpdate() { }
        public virtual void MGFixedUpdate() { }
    }
}
