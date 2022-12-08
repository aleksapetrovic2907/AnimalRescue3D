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
            for (int i = 0; i < upgradeButtons.Count; i++)
            {
                var level = upgrades[i].level;
                var cost = upgrades[i].baseCost;

                upgradeButtons[i].UpdateData(level, cost);
                upgradeButtons[i].SetAffordability(GameDataManager.Instance.gameData.money >= cost);
                if (upgrades[i].relativeLevel == maxUpgrades) upgradeButtons[i].SetMaxedOut();
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
