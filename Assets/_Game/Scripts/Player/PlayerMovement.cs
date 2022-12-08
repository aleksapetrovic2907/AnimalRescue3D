using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi.Player
{
    public partial class PlayerMovement : GloballyAccessibleBase<PlayerMovement>
    {
        public bool IsMoving { get { return !(MovementJoystick.Horizontal == 0f && MovementJoystick.Vertical == 0f); } private set { } }
        public bool CanMove = true;
        public float BaseMovementSpeed;
        public float TotalMovementSpeed { get; private set; }
        [SerializeField] private Joystick MovementJoystick;      

        private PlayerController m_playerController;
        private CharacterController m_characterController;
        private PlayerAnimatorController m_playerAnimatorController;
        private CatchController m_catchController;
        private Vector3 m_velocity;
        private float m_isFullSpeedBonus;
        private float m_isTransitioningSpeedBonus;

        private const float MOVE_MODIFIER_CONST = .002f; // used to lower 'move amount' for achievements

        private void Start()
        {
            m_playerController = GetComponent<PlayerController>();
            m_characterController = GetComponent<CharacterController>();
            m_playerAnimatorController = GetComponent<PlayerAnimatorController>();
            m_catchController = GetComponent<CatchController>();
        }

        private void Update()
        {
            UpdateBonuses();
            Move();
        }

        private void Move()
        {
            TotalMovementSpeed = BaseMovementSpeed + m_isFullSpeedBonus + m_isTransitioningSpeedBonus;


            if (m_characterController.isGrounded)
                m_velocity.y = 0f;
            else
                m_velocity.y += Physics.gravity.y * Time.deltaTime;

            if (!CanMove)
                return;

            m_characterController.Move(m_velocity * Time.deltaTime);

            if (!IsMoving)
                return;

            float x = MovementJoystick.Horizontal;
            float z = MovementJoystick.Vertical;


            transform.forward = new Vector3(MovementJoystick.Horizontal, 0, MovementJoystick.Vertical);
            var moveAmount = transform.forward * TotalMovementSpeed * Time.deltaTime;
            m_characterController.Move(moveAmount);

            if (GameDataManager.Instance != null)
                GameDataManager.Instance.gameData.distanceTravelled += (Mathf.Abs(moveAmount.x) + Mathf.Abs(moveAmount.z)) * MOVE_MODIFIER_CONST;
        }
    }
}
