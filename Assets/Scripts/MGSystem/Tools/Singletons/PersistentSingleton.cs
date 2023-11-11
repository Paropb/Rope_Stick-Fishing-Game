using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGSystem
{
    public class PersistentSingleton<T> : MonoBehaviour where T : Component
    {
        [Header("Persistent Singleton")]
        [Tooltip("if this is true, this singleton will auto detach if it finds itself parented on awake")]
        public bool AutomaticallyUnparentOnAwake = true;
        protected static T instance;
        public static bool HasInstance => instance != null;
        public static T Current => instance;
        protected bool enable;
        public static T Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if(instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(T).Name + "_AutoCreated";
                        instance = obj.AddComponent<T>();
                    }
                }
                return instance;
            }
        }
        protected virtual void Awake()
        {
            if(!Application.isPlaying)
            {
                return;
            }
            if(AutomaticallyUnparentOnAwake)
            {
                this.transform.SetParent(null);
            }
            if(instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(transform.gameObject);
                enable = true;
            }
            else
            {
                if(this != instance)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
