using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi.Drone
{
    public class DroneUpgradeManager : GloballyAccessibleBase<DroneUpgradeManager>
    {
        public int cooldownLevel { get; private set; } = 0;
        public int capacityLevel { get; private set; } = 0;
        public int cdMaxLevel { get; private set; } = 5;
        public int cpMaxLevel { get; private set; } = 5;

        [Header("Costs")]
        public List<int> cdCosts; // 0th index has default value
        public List<int> cpCosts;

        [Header("Values")]
        public List<int> cdValues; // 0th index has default value
        public List<int> cpValues;

        private void Start()
        {
            if (GameDataManager.Instance == null) return;

            LoadUpgradesData();
            DroneShopUI.Instance.UpdateUI();
        }

        private void LoadUpgradesData()
        {
            cooldownLevel = GameDataManager.Instance.gameData.droneCooldownLevel;
            capacityLevel = GameDataManager.Instance.gameData.droneCapacityLevel;
            DroneController.Instance.cooldownDuration = cdValues[cooldownLevel];
            DroneController.Instance.maxCapacity = cpValues[capacityLevel];
        }

        public void LevelUpCooldown()
        {
            if (cooldownLevel == cdMaxLevel - 1 || GameDataManager.Instance.gameData.gems < cdCosts[cooldownLevel + 1]) return;
            GameDataManager.Instance.gameData.gems -= cdCosts[++cooldownLevel];
            GameDataManager.Instance.gameData.droneCooldownLevel = cooldownLevel;
            DroneController.Instance.cooldownDuration = cdValues[cooldownLevel];
            DroneShopUI.Instance.UpdateUI();
            DroneSkinController.Instance.UpdateSkin();
        }

        public void LevelUpCapacity()
        {
            if (capacityLevel == cpMaxLevel - 1 || GameDataManager.Instance.gameData.gems < cpCosts[capacityLevel + 1]) return;
            GameDataManager.Instance.gameData.gems -= cpCosts[++capacityLevel];
            GameDataManager.Instance.gameData.droneCapacityLevel = capacityLevel;
            DroneController.Instance.maxCapacity = cpValues[capacityLevel];
            DroneShopUI.Instance.UpdateUI();
            DroneSkinController.Instance.UpdateSkin();
        }
    }
}
