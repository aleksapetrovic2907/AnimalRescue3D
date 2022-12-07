using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi.Player
{
    public class PlayerAnimatorController : GloballyAccessibleBase<PlayerAnimatorController>
    {
        [SerializeField] private List<Animator> animators;

        private PlayerMovement m_playerMovement;
        private PlayerVehicleManager m_playerVehicleManager;

        private Vehicle m_currentVehicle;

        private void Start()
        {
            m_playerMovement = GetComponent<PlayerMovement>();
            m_playerVehicleManager = GetComponent<PlayerVehicleManager>();
        }

        private void Update()
        {
            foreach (var animator in animators)
                animator.SetBool("IsMoving", m_playerMovement.IsMoving);
        }

        public void UpdateAnimations(Vehicle vehicle)
        {
            m_currentVehicle = vehicle;
            UpdateVehicle();
        }

        public void UpdateVehicle()
        {
            foreach (var animator in animators)
            {
                animator.SetFloat("IdleSpeed", m_currentVehicle.idleSpeed);
                animator.SetFloat("MoveSpeed", m_currentVehicle.moveSpeed);
                animator.runtimeAnimatorController = m_currentVehicle.animatorOverrideController;
            }

        }
    }
}
