using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGSystem
{
    /// <summary>
    /// Game object extensions
    /// </summary>
    public static class GameObjectExtensions
    {
        static List<Component> m_ComponentCache = new List<Component>();

        /// <summary>
        /// Grabs a component without allocating memory uselessly
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static T RGGetComponentNoAlloc<T>(this GameObject @this) where T : Component
        {
            @this.GetComponents(typeof(T), m_ComponentCache);
            Component component = m_ComponentCache.Count > 0 ? m_ComponentCache[0] : null;
            m_ComponentCache.Clear();
            return component as T;
        }
    }
}
