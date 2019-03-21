using System.Collections;
using UnityEngine;

namespace Runtime.Utilities
{
    public class CoroutineManager : MonoBehaviour
    {
        public static CoroutineManager _instance;

        public static CoroutineManager Instance
        {
            get
            {
                //Look for instance on scene.
                if (_instance == null)
                    _instance = FindObjectOfType<CoroutineManager>();
                    
                //Create instance if not found.
                if (_instance == null)
                {
                    var coroutineManager = new GameObject("CoroutineManager");

                    _instance = coroutineManager.AddComponent<CoroutineManager>();
                }

                return _instance;
            }
        }
        
        
        private void Awake()
        {
            EnsureSingletoneInstance();
        }

        private void EnsureSingletoneInstance()
        {
            if (Instance == null)
            {
                Debug.LogError("CoroutineManager | Instance haven't been found or created. Something went terribly wrong.");
                return;
            }    
            
            if(Instance != this);
                Destroy(gameObject);
            
        }

        public static Coroutine Start(IEnumerator coroutine)
        {
            return Instance.StartCoroutine(coroutine);
        }

        public static void Stop(IEnumerator coroutine)
        {
            Instance.StopCoroutine(coroutine);
        }
    }
}