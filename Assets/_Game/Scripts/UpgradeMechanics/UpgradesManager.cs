using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi.UpgradeMechanics
{
    public partial class UpgradesManager : GloballyAccessibleBase<UpgradesManager>
    {
        public int maxUpgrades = 7;

        public int baseCost;
        public float firstWaveConstant;
        public float secondWaveConstant;
        public float thirdWaveConstant;
        public List<int> capacityLevelsAtWhichWavesStart = new List<int>() { 4, 7 };

        public List<UpgradeBase> upgrades = new List<UpgradeBase>();
        [SerializeField] private List<UpgradeButton> upgradeButtons = new List<UpgradeButton>();

        // Used to determine whether we can afford any of the upgrades (for arrow indication toward upgrade zone).
        [HideInInspector] public int leastExpensiveUpgradeCost = 0;

        private void Start()
        {
            LoadUpgradesData();
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

        private void LoadUpgradesData()
        {
            // if (GameDataManager.Instance == null) return;

            // for (int i = 0; i < upgrades.Count; i++)
            // {
            //     var level = GameDataManager.Instance.gameData.upgradeLevels[i] - GameDataManager.Instance.gameData.relativeUpgradeLevels[i];
            //     upgrades[i].level = level;
            //     upgrades[i].relativeLevel = 0;
            //     Upgrade(i, GameDataManager.Instance.gameData.relativeUpgradeLevels[i], true);
            // }
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
                if(upgrade.relativeLevel < maxUpgrades) return false;
            }

            return true;
        }
    }
}
