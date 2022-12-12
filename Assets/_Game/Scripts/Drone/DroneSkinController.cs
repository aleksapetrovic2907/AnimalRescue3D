using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi.Drone
{
    public class DroneSkinController : GloballyAccessibleBase<DroneSkinController>
    {
        [SerializeField] private List<DroneSkin> skins;

        private DroneUpgradeManager m_droneUpgradeManager;
        private DroneController m_droneController;

        private void Start()
        {
            m_droneController = GetComponent<DroneController>();
            m_droneUpgradeManager = GetComponent<DroneUpgradeManager>();
            UpdateSkin();
        }

        public void UpdateSkin()
        {
            foreach (var skin in skins)
                skin.gameObject.SetActive(false);

            var level = m_droneUpgradeManager.cooldownLevel <= m_droneUpgradeManager.capacityLevel ? m_droneUpgradeManager.cooldownLevel : m_droneUpgradeManager.capacityLevel;

            skins[level % skins.Count].gameObject.SetActive(true);
            m_droneController.leashOrigins = skins[level % skins.Count].origins;
        }
    }
}
