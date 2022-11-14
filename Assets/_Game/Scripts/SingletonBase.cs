using UnityEngine;

namespace Aezakmi
{
    public abstract class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance;

        protected virtual void Awake()
        {
            if (Instance != null)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
            Instance = gameObject.GetComponent<T>();
        }
    }
}
