using System.Collections.Generic;
using UnityEngine;
using Aezakmi.UpgradeMechanics;
using Aezakmi.Animals;

namespace Aezakmi.Player
{
    public class PlayerController : GloballyAccessibleBase<PlayerController>
    {
        [SerializeField] private GameObject fullIndicator;
        [SerializeField] private GameObject shelterIndicator;
        [SerializeField] private GameObject newLevelIndicator;
        [SerializeField] private GameObject upgradeIndicator;
        [SerializeField] private RangeIndicator rangeIndicator;

        private CatchController m_catchController;
        private PlayerMovement m_playerMovement;
        private PlayerAnimatorController m_playerAnimatorController;
        private PlayerLevelTransitioner m_playerLevelTransitioner;

        private bool m_moneyWasThrown = false;

        private void Start()
        {
            m_catchController = GetComponent<CatchController>();
            m_playerMovement = GetComponent<PlayerMovement>();
            m_playerAnimatorController = GetComponent<PlayerAnimatorController>();
            m_playerLevelTransitioner = GetComponent<PlayerLevelTransitioner>();
        }

        private void Update()
        {
            CheckForUpgradeIndicator();
        }

        public void TransitionToNextLevel()
        {
            m_playerLevelTransitioner.enabled = true;
            ToggleNewLevelIndicator();
        }

        #region Indicators
        public void ToggleFullIndicator()
        {
            fullIndicator.SetActive(m_catchController.IsFull());
            shelterIndicator.SetActive(m_catchController.IsFull());
        }

        public void ToggleNewLevelIndicator()
        {
            newLevelIndicator.SetActive(!newLevelIndicator.activeSelf);
        }

        public void CheckForUpgradeIndicator()
        {
            if (ReferenceManager.Instance.moneyParent.childCount > 0)
                m_moneyWasThrown = true;

            if (ReferenceManager.Instance.moneyParent.childCount == 0 && m_moneyWasThrown && GameDataManager.Instance.gameData.money >= UpgradesManager.Instance.leastExpensiveUpgradeCost && !UpgradesManager.Instance.AreAllUpgradesMaxed())
            {
                m_moneyWasThrown = false;
                upgradeIndicator.SetActive(true);
            }
        }

        public void EnterUpgradeZone()
        {
            upgradeIndicator.SetActive(false);
            rangeIndicator.Display();
        }

        public void LeaveUpgradeZone()
        {
            rangeIndicator.Disappear();
        }
        #endregion
    }
}
