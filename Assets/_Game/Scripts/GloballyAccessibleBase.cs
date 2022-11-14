using UnityEngine;

namespace Aezakmi
{
    public abstract class GloballyAccessibleBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance;

        protected virtual void Awake()
        {
            if (Instance != null)
                Destroy(gameObject);

            Instance = gameObject.GetComponent<T>();
        }
    }
}
