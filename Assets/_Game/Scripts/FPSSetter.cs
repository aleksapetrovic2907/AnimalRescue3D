using UnityEngine;

namespace Aezakmi
{
    public class FPSSetter : MonoBehaviour
    {
        private void Start()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            Application.targetFrameRate = 60;
#endif
#if UNITY_IOS && !UNITY_EDITOR
            Application.targetFrameRate = 60;
#endif
        }
    }
}
