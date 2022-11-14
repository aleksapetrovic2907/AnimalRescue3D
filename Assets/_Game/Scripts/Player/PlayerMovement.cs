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

        #region VEHICLES
        [Header("Vehicles Settings")]
        [Space(10)]
        [SerializeField] private List<GameObject> PlayerMeshes;
        [SerializeField] private List<Vector2> MeshesCorrespondingLevels;
        private const float INFINITESIMAL = .05f;
        #endregion

        private PlayerController _playerController;
        private CharacterController _characterController;
        private PlayerAnimatorController _playerAnimatorController;
        private CatchController _catchController;
        private Vector3 _velocity;
        private float _isFullSpeedBonus;
        private float _isTransitioningSpeedBonus;

        private void Start()
        {
            _playerController = GetComponent<PlayerController>();
            _characterController = GetComponent<CharacterController>();
            _playerAnimatorController = GetComponent<PlayerAnimatorController>();
            _catchController = GetComponent<CatchController>();
        }

        private void Update()
        {
            UpdateBonuses();
            Move();
        }

        private void Move()
        {
            TotalMovementSpeed = BaseMovementSpeed + _isFullSpeedBonus + _isTransitioningSpeedBonus;


            if (_characterController.isGrounded)
                _velocity.y = 0f;
            else
                _velocity.y += Physics.gravity.y * Time.deltaTime;

            if (!CanMove)
                return;

            _characterController.Move(_velocity * Time.deltaTime);

            if (!IsMoving)
                return;

            float x = MovementJoystick.Horizontal;
            float z = MovementJoystick.Vertical;

            transform.forward = new Vector3(MovementJoystick.Horizontal, 0, MovementJoystick.Vertical);
            _characterController.Move(transform.forward * TotalMovementSpeed * Time.deltaTime);
        }

        public void ChangeVehicle(int speedLevel, bool hasUpgraded)
        {
            var correctLevel = 0;

            // Select mesh with correct vehicle
            for (int i = 0; i < MeshesCorrespondingLevels.Count; i++)
            {
                if (speedLevel >= (int)MeshesCorrespondingLevels[i].x && speedLevel <= (int)MeshesCorrespondingLevels[i].y)
                {
                    correctLevel = i;
                    break;
                }
            }

            // Disable previous mesh
            var previous = hasUpgraded ? 1 : -1;
            if (correctLevel - previous >= 0 && correctLevel - previous < PlayerMeshes.Count)
                PlayerMeshes[correctLevel - previous].SetActive(false);

            // Activate new mesh
            PlayerMeshes[correctLevel].SetActive(true);

            // Set new hand containers
            _catchController.SetNewHands(PlayerMeshes[correctLevel].GetComponent<MeshHandsController>());

            // Set color
            // ! if (speedLevel > 0)
            // !     PlayerMeshes[correctLevel].GetComponent<VehicleColorController>().ChangeColor(speedLevel);
        }

        public void FixPositionY()
        {
            _characterController.Move(Vector3.up * INFINITESIMAL);
        }
    }
}
