using UnityEngine;

namespace Aezakmi.CameraMechanics
{
    [ExecuteInEditMode]
    public class CameraFollowPlayer : MonoBehaviour
    {
        [SerializeField] private Vector3 offset;
        [SerializeField] private Transform target;
        [SerializeField] private int followSpeed;

        private void LateUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, target.position + offset, followSpeed * Time.deltaTime);
        }
    }
}
