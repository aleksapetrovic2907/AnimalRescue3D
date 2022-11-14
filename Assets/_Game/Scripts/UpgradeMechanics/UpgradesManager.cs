using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi.UpgradeMechanics
{
    public partial class UpgradesManager : GloballyAccessibleBase<UpgradesManager>
    {
        [SerializeField] private List<UpgradeBase> upgrades = new List<UpgradeBase>();
        [SerializeField] private List<UpgradeButton> upgradeButtons = new List<UpgradeButton>();

        private void Start()
        {
            AddButtonEventListeners();
            UpdateShopUI();
        }

        /// <summary>Returns whether or not buying an upgrade was successful.</summary>
        public bool BuyUpgrade(int upgradeIndex)
        {
            // Validate if upgrade affordable
            var cost = upgrades[upgradeIndex].Cost(upgrades[upgradeIndex].level + 1);
            if (GameManager.Instance.money < cost)
                return false;

            // Affordable, buy it
            GameManager.Instance.money -= cost;
            Upgrade(upgradeIndex, 1, true);
            UpdateShopUI();
            return true;
        }

        public void Upgrade(int upgradeIndex, int levels, bool isRelative)
        {
            upgrades[upgradeIndex].Upgrade(levels, isRelative);
        }
    }
}
