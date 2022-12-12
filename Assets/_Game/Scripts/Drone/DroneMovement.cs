using UnityEngine;

namespace Aezakmi.Drone
{
    public class DroneMovement : GloballyAccessibleBase<DroneMovement>
    {
        public float movementSpeed;
        [SerializeField] private float rotateSpeed = 3;
        [SerializeField] private Vector3 originalRotation;
        [SerializeField] private Transform rescueZone;
        [SerializeField] private Transform station;

        public Transform target;
        private const float INFINITESIMAL = .001f;

        private void Update()
        {
            if (target == null) return;

            if (Vector3.Distance(transform.position, target.position) <= INFINITESIMAL)
            {
                target = null;
                return;
            }

            Vector3 direction = (target.position - transform.position).normalized;
            var eulerAngles = Quaternion.LookRotation(direction).eulerAngles;
            eulerAngles.x = 0;
            eulerAngles.z = 0;

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(eulerAngles), rotateSpeed * Time.deltaTime);

            var step = movementSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }

        public void MoveToShelter() => target = rescueZone;
        public void MoveToStation() => target = station;
        public void ResetRotation() => transform.localEulerAngles = originalRotation;
    }
}
