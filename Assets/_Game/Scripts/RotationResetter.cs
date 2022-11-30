using UnityEngine;

namespace Aezakmi
{
    // Sets rotation after every frame
    public class RotationResetter : MonoBehaviour
    {
        [SerializeField] private Vector3 eulerAngle;
        private void LateUpdate() => transform.eulerAngles = eulerAngle;
    }
}
