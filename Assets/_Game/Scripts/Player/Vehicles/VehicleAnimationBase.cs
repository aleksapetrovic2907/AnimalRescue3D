using UnityEngine;

namespace Aezakmi.Player.Vehicles
{
    public abstract class VehicleAnimationBase : MonoBehaviour
    {
        [SerializeField] protected PlayerMovement playerMovement;

        protected bool isPlayerMoving = false;

        private bool m_lastMovingState = false;

        protected virtual void Update()
        {
            isPlayerMoving = playerMovement.IsMoving;

            if (isPlayerMoving == true && m_lastMovingState == false) StartedMoving();
            if (isPlayerMoving == false && m_lastMovingState == true) StoppedMoving();

            m_lastMovingState = isPlayerMoving;
        }

        protected virtual void StartedMoving() { }
        protected virtual void StoppedMoving() { }
    }
}
