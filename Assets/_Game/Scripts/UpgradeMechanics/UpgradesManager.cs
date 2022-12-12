using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi.UpgradeMechanics
{
    public partial class UpgradesManager : GloballyAccessibleBase<UpgradesManager>
    {
        public int maxUpgrades = 8;

        public int baseCost;
        public float firstWaveConstant;
        public float secondWaveConstant;
        public float thirdWaveConstant;

        // limit upgrading until all upgrades reach levels in list
        public List<int> levelsAtWhichToLimitUpgrades = new List<int>() { 3, 6 };

        public List<UpgradeBase> upgrades = new List<UpgradeBase>();
        [SerializeField] private List<UpgradeButton> upgradeButtons = new List<UpgradeButton>();

        // Used to determine whether we can afford any of the upgrades (for arrow indication toward upgrade zone).
        [HideInInspector] public int leastExpensiveUpgradeCost = 0;

        private void Start()
        {
            LoadData();
            AddButtonEventListeners();
            UpdateShopUI();
        }

        /// <summary>Returns whether or not buying an upgrade was successful.</summary>
        public bool BuyUpgrade(int upgradeIndex)
        {
            // Validate if upgrade affordable
            var cost = upgrades[upgradeIndex].baseCost;
            if (GameDataManager.Instance.gameData.money < cost)
                return false;

            // Affordable, buy it
            GameDataManager.Instance.gameData.money -= cost;
            Upgrade(upgradeIndex, 1, true);
            UpdateShopUI();
            UpdateLeastExpensiveUpgrade();
            return true;
        }

        public void Upgrade(int upgradeIndex, int levels, bool isRelative)
        {
            upgrades[upgradeIndex].Upgrade(levels, isRelative);
        }

        private void UpdateLeastExpensiveUpgrade()
        {
            leastExpensiveUpgradeCost = upgrades[0].baseCost;

            for (int i = 0; i < upgrades.Count; i++)
            {
                var levelCost = upgrades[i].baseCost;
                if (levelCost < leastExpensiveUpgradeCost)
                    leastExpensiveUpgradeCost = levelCost;
            }
        }

        public bool AreAllUpgradesMaxed()
        {
            foreach (var upgrade in upgrades)
            {
                if (upgrade.relativeLevel < maxUpgrades) return false;
            }

            return true;
        }

        private void LoadData()
        {
            for (int i = 0; i < upgrades.Count; i++)
                upgrades[i].level = GameDataManager.Instance.gameData.upgradeLevels[i];
        }

        // Saved on level complete
        public void SaveData()
        {
            for (int i = 0; i < upgrades.Count; i++)
                GameDataManager.Instance.gameData.upgradeLevels[i] = upgrades[i].level;
        }
    }
}
