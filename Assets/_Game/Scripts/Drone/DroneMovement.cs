using UnityEngine;
using DG.Tweening;

namespace Aezakmi.Drone
{
    public class DroneMovement : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
        [SerializeField] private Transform rescueZone;
        [SerializeField] private Transform station;

        [Header("Lift Tween Settings")]
        [SerializeField] private float moveAmount; // y-axis
        [SerializeField] private float moveDuration;
        [SerializeField] private Ease moveEase;

        private Transform m_targetToMoveTo;

        private void Update()
        {
            if (m_targetToMoveTo == null) return;

            var step = movementSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, m_targetToMoveTo.position, step);
        }

        public void MoveToShelter()
        {
            Tween moveUp = transform.DOLocalMoveY(transform.localPosition.y + moveAmount, moveDuration).SetEase(moveEase).OnComplete(delegate
            {
                m_targetToMoveTo = rescueZone;
            });
        }
    }
}
