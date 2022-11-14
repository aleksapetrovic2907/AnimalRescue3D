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
                var cost = upgrades[i].Cost(level + 1);

                upgradeButtons[i].UpdateData(level, cost);
                upgradeButtons[i].SetAffordability(GameManager.Instance.money >= cost);
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
