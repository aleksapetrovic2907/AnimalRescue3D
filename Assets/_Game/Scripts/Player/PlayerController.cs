using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi.Player
{
    public class PlayerController : GloballyAccessibleBase<PlayerController>
    {
        public int CurrentCaught = 0;
        public int MaximumCaught;
        public bool IsFull { get { return CurrentCaught >= MaximumCaught; } private set { } }

        [SerializeField] private GameObject FullIndicator;
        [SerializeField] private GameObject ShelterIndicator;
        [SerializeField] private GameObject NewLevelIndicator;

        private CatchController _catchController;
        private PlayerMovement _playerMovement;
        private PlayerAnimatorController _playerAnimatorController;

        // ! private void OnEnable() => EventManager.StartListening(GameEvents.LevelFinished, StopMoving);
        // ! private void OnDisable() => EventManager.StopListening(GameEvents.LevelFinished, StopMoving);

        private void Start()
        {
            _catchController = GetComponent<CatchController>();
            _playerMovement = GetComponent<PlayerMovement>();
            _playerAnimatorController = GetComponent<PlayerAnimatorController>();
        }

        private void Update()
        {
            _catchController.enabled = CurrentCaught < MaximumCaught;
        }

        public void AnimalCaught()
        {
            CurrentCaught++;
            ToggleFullIndicator();
        }

        public void AnimalRescued()
        {
            CurrentCaught--;
            ToggleFullIndicator();
        }

        public void ToggleFullIndicator()
        {
            FullIndicator.SetActive(IsFull);
            ShelterIndicator.SetActive(IsFull);
        }

        public void ToggleNewLevelIndicator()
        {
            NewLevelIndicator.SetActive(!NewLevelIndicator.activeSelf);
        }

        private void StopMoving(Dictionary<string, object> message)
        {
            // Happens when level finishes
            _playerMovement.BaseMovementSpeed = 0f;
            _playerMovement.enabled = false;

            _playerAnimatorController.StopMoving();
            _playerAnimatorController.enabled = false;
        }
    }
}
