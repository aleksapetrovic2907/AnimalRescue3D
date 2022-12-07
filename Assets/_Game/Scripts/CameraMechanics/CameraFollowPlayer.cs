using UnityEngine;

namespace Aezakmi.CameraMechanics
{
    [ExecuteInEditMode]
    public class CameraFollowPlayer : MonoBehaviour
    {
        [SerializeField] private Vector3 offset;
        [SerializeField] private Transform target;
        [SerializeField] private int followSpeed;

        private Vector3 m_offset;

        private void Start()
        {
            m_offset = target.lossyScale.x * offset;
            transform.position = target.position + m_offset;
        }

        private void LateUpdate()
        {
            m_offset = target.lossyScale.x * offset;
            transform.position = Vector3.Lerp(transform.position, target.position + m_offset, followSpeed * Time.deltaTime);
        }
    }
}
