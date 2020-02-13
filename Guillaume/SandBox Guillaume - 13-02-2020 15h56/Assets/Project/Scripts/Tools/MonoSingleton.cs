using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectIntrus.Tools
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        private static object syncRoot;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        GameObject singleton = new GameObject();
                        instance = singleton.AddComponent<T>();
                        singleton.name = "(MonoSingleton) " + typeof(T).ToString();

                        if (Application.isPlaying)
                        {
                            DontDestroyOnLoad(singleton);
                        }
                    }
                }

                return instance;
            }
        }
    }
}