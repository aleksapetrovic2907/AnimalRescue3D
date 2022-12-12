using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Aezakmi.Drone;

namespace Aezakmi
{
    public class DroneShopUI : GloballyAccessibleBase<DroneShopUI>
    {
        [SerializeField] private List<DroneButtonUI> droneButtonUIs;

        [Header("Tween Settings")]
        [SerializeField] private float moveDuration;
        [SerializeField] private Ease loopEase;

        private static Color s_greyedOutColor = new Color(0.3490566f, 0.3490566f, 0.3490566f);

        private RectTransform m_rectTransform;
        private Vector2 m_downPosition;
        private Vector2 m_upPosition;

        private void Start()
        {
            m_rectTransform = GetComponent<RectTransform>();
            m_downPosition = m_rectTransform.anchoredPosition;
            m_upPosition = new Vector2(m_downPosition.x, -m_downPosition.y);
        }

        public void Open()
        {
            m_rectTransform.DOAnchorPos(m_upPosition, moveDuration).SetEase(loopEase).Play();
            UpdateUI();
        }

        public void Close()
        {
            m_rectTransform.DOAnchorPos(m_downPosition, moveDuration).SetEase(loopEase).Play();
        }

        public void UpgradeCooldown() => DroneUpgradeManager.Instance.LevelUpCooldown();
        public void UpgradeCapacity() => DroneUpgradeManager.Instance.LevelUpCapacity();

        public void UpdateUI()
        {
            var cdLevel = DroneUpgradeManager.Instance.cooldownLevel;
            if (cdLevel == DroneUpgradeManager.Instance.cdMaxLevel - 1)
                droneButtonUIs[0].MaxedOut(cdLevel);
            else
                droneButtonUIs[0].UpdateInfo(cdLevel, DroneUpgradeManager.Instance.cdCosts[cdLevel + 1], DroneUpgradeManager.Instance.cdCosts[cdLevel + 1] <= GameDataManager.Instance.gameData.gems);

            var cpLevel = DroneUpgradeManager.Instance.capacityLevel;
            if (cpLevel == DroneUpgradeManager.Instance.cpMaxLevel - 1)
                droneButtonUIs[1].MaxedOut(cpLevel);
            else
                droneButtonUIs[1].UpdateInfo(cpLevel, DroneUpgradeManager.Instance.cpCosts[cpLevel + 1], DroneUpgradeManager.Instance.cpCosts[cpLevel + 1] <= GameDataManager.Instance.gameData.gems);
        }

    }
}
