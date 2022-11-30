using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi.Player
{
    public class PlayerController : GloballyAccessibleBase<PlayerController>
    {
        public int CurrentCaught = 0;
        public int MaximumCaught;
        public bool IsFull { get { return CurrentCaught >= MaximumCaught; } private set { } }

        [SerializeField] private GameObject fullIndicator;
        [SerializeField] private GameObject shelterIndicator;
        [SerializeField] private GameObject newLevelIndicator;
        [SerializeField] private GameObject upgradeIndicator;

        private CatchController _catchController;
        private PlayerMovement _playerMovement;
        private PlayerAnimatorController _playerAnimatorController;

        private bool m_moneyWasThrown = false;

        private void Start()
        {
            _catchController = GetComponent<CatchController>();
            _playerMovement = GetComponent<PlayerMovement>();
            _playerAnimatorController = GetComponent<PlayerAnimatorController>();
        }

        private void Update()
        {
            _catchController.enabled = CurrentCaught < MaximumCaught;
            CheckForUpgradeIndicator();
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

        #region Indicators
        public void ToggleFullIndicator()
        {
            fullIndicator.SetActive(IsFull);
            shelterIndicator.SetActive(IsFull);
        }

        public void ToggleNewLevelIndicator()
        {
            newLevelIndicator.SetActive(!newLevelIndicator.activeSelf);
        }

        public void CheckForUpgradeIndicator()
        {
            // todo: must also require that player has enough money for an upgrade

            if (ReferenceManager.Instance.moneyParent.childCount > 0)
                m_moneyWasThrown = true;

            if (ReferenceManager.Instance.moneyParent.childCount == 0 && m_moneyWasThrown)
            {
                m_moneyWasThrown = false;
                upgradeIndicator.SetActive(true);
            }
        }

        public void EnterUpgradeZone()
        {
            upgradeIndicator.SetActive(false);
        }
        #endregion
    }
}
