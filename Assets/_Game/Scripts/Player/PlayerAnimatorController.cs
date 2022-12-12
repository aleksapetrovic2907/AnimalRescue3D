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
        private bool m_forcing = false;

        private void Start()
        {
            m_playerMovement = GetComponent<PlayerMovement>();
            m_playerVehicleManager = GetComponent<PlayerVehicleManager>();
        }

        private void Update()
        {
            if (m_forcing) return;

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

        // Force moving animations regardless of PlayerMovements' inputs
        public void ForceMove(bool IsMoving)
        {
            m_forcing = true;
            foreach (var animator in animators)
                animator.SetBool("IsMoving", IsMoving);
        }
    }
}
