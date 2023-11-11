using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGSystem
{
    public class PersistentHumbleSingleton<T> : MonoBehaviour where T : Component
    {
        protected static T instance;
        public static bool HasInstance => instance != null;
        public static T Current => instance;
        
        [RGReadOnly]
        public float InitializationTime;
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
                        obj.hideFlags = HideFlags.HideAndDontSave;
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

            InitializationTime = Time.time;
            DontDestroyOnLoad(this.gameObject);

            T[] check = FindObjectsOfType<T>();
            foreach (var searched in check)
            {
                if(searched != this)
                {
                    //只保存在这之后创建的PersistentHumbleSingleton
                    if(searched.GetComponent<PersistentHumbleSingleton<T>>().InitializationTime < InitializationTime)
                    {
                        Destroy(searched.gameObject);
                    }
                }
            }
            if(instance == null)
            {
                instance = this as T;
            }
        }
    }
}
