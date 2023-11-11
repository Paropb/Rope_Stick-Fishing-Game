using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.CoreSystem
{
    public class CoreComp<TCoreComponent> where TCoreComponent : CoreComponent
    {
        private Core core;
        private TCoreComponent comp;

        public TCoreComponent Comp => comp ? comp : core.GetCoreComponent(ref comp);
        public CoreComp(Core core)
        {
            if (core == null)
                Debug.LogError($"Core is NULL for component {typeof(TCoreComponent)}");
            this.core = core;
        }
    }
}
