using System.Collections.Generic;
using UnityEngine;
using Aezakmi.Player;

namespace Aezakmi.Skins
{
    public partial class SkinsManager : GloballyAccessibleBase<SkinsManager>
    {
        public List<Skin> skins;

        public const int TOTAL_SKINS = 11;

        private void Start()
        {
            LoadSkinsData();
            AddSkinsToMenu();
        }

        private void LoadSkinsData()
        {
            if (GameDataManager.Instance == null) return;

            for (int i = 0; i < GameDataManager.Instance.gameData.skinsBought.Length; i++)
                skins[i].bought = GameDataManager.Instance.gameData.skinsBought[i];
        }

        public void BuySkin(int index)
        {
            // Check if skin is already bought
            if (skins[index].bought == true)
                SetActiveSkin(index);

            // If not bought, check if player has enough gems
            else if (GameDataManager.Instance.gameData.gems >= skins[index].cost)
            {
                GameDataManager.Instance.gameData.gems -= skins[index].cost;
                GameDataManager.Instance.gameData.skinsBought[index] = true;
                skins[index].bought = true;
                SetActiveSkin(index);
            }

            RefreshUI();
        }

        public void SetActiveSkin(int index)
        {
            GameDataManager.Instance.gameData.activeSkinIndex = index;
            PlayerSkinController.Instance.SkinChanged();
        }
    }

    [System.Serializable]
    public class Skin
    {
        public Sprite sprite;
        public int cost;
        public bool bought = false;
    }
}


