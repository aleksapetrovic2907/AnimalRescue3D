using UnityEngine;
using Aezakmi.UI;

namespace Aezakmi.UpgradeMechanics
{
    // Handles UI
    public partial class UpgradesManager
    {
        [SerializeField] private ShopUI shopUI;

        private void AddButtonEventListeners()
        {
            for (int i = 0; i < upgradeButtons.Count; i++)
            {
                var index = i;
                upgradeButtons[i].Button.onClick.AddListener(() => BuyUpgrade(index));
            }
        }

        private void UpdateShopUI()
        {
            int currentLimit = maxUpgrades;

            int smallestLevel = maxUpgrades;
            for (int i = 0; i < upgradeButtons.Count; i++)
                if (smallestLevel > upgrades[i].relativeLevel) smallestLevel = upgrades[i].relativeLevel;

            for (int i = 0; i < levelsAtWhichToLimitUpgrades.Count; i++)
            {
                if (smallestLevel < levelsAtWhichToLimitUpgrades[i])
                {
                    currentLimit = levelsAtWhichToLimitUpgrades[i];
                    break;
                }
            }

            for (int i = 0; i < upgradeButtons.Count; i++)
            {
                var level = upgrades[i].level;
                var cost = upgrades[i].baseCost;
                bool affordable = GameDataManager.Instance.gameData.money >= cost;
                upgradeButtons[i].UpdateData(level, cost);
                upgradeButtons[i].SetAffordability(affordable);
                if (upgrades[i].relativeLevel == currentLimit) upgradeButtons[i].SetMaxedOut();
                else if (affordable) upgradeButtons[i].SetUnmaxed();
            }
        }

        public void EnteredShopArea()
        {
            UpdateShopUI();
            shopUI.Open();
        }

        public void LeftShopArea() => shopUI.Close();
    }
}
