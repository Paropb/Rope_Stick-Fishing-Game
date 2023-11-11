using MyGame.MGEntity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyGame.CoreSystem
{
    public class Core : MonoBehaviour
    {
        public Entity Entity { get; private set; }
        private List<CoreComponent> coreComponents = new List<CoreComponent>();
        public void MGAwake(Entity entity)
        {
            Entity = entity;

            coreComponents = GetComponentsInChildren<CoreComponent>().ToList();
            coreComponents.ForEach(component => component.MGAwake(this));
        }
        public void MGUpdate()
        {
            foreach (CoreComponent coreComponent in coreComponents)
            {
                coreComponent.MGUpdate();
            }
        }
        public void MGFixedUpdate()
        {
            foreach (CoreComponent coreComponent in coreComponents)
            {
                coreComponent.MGFixedUpdate();
            }
        }
        public void AddComponent(CoreComponent component)
        {
            if (!coreComponents.Contains(component))
                coreComponents.Add(component);
        }
        public T GetCoreComponent<T>() where T : CoreComponent
        {
            var comp = coreComponents.OfType<T>().FirstOrDefault();

            if (comp == null)
            {
                comp = GetComponentInChildren<T>();
                if (comp == null)
                    Debug.LogWarning($"{typeof(T).Name} not found on {transform.parent.name}");
            }

            return comp;
        }

        public T GetCoreComponent<T>(ref T value) where T : CoreComponent
        {
            value = GetCoreComponent<T>();
            return value;
        }
    }
}
