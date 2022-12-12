using UnityEngine;

namespace Aezakmi.Drone
{
    public class DroneMeshRotator : MonoBehaviour
    {
        [SerializeField] private Vector3 movingEulerAngles;
        [SerializeField] private float rotateSpeed;
        [SerializeField] private DroneMovement droneMovement;

        private Vector3 m_targetEuler;

        private void Update()
        {
            m_targetEuler = droneMovement.target == null ? Vector3.zero : movingEulerAngles;
            transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, m_targetEuler, rotateSpeed * Time.deltaTime);
        }
    }
}
