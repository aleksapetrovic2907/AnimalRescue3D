using UnityEngine;
using Aezakmi.Player;

namespace Aezakmi.UpgradeMechanics
{
    public class SpeedUpgrade : UpgradeBase
    {
        private PlayerVehicleManager m_playerVehicleManager;
        private PlayerMovement m_playerMovement;
        private float m_originalBaseSpeed;

        protected override void Start()
        {
            base.Start();

            m_playerVehicleManager = ReferenceManager.Instance.player.GetComponent<PlayerVehicleManager>();
            m_playerMovement = ReferenceManager.Instance.player.GetComponent<PlayerMovement>();
            m_originalBaseSpeed = m_playerMovement.BaseMovementSpeed;
        }

        public override void Upgrade(int levels, bool isRelative)
        {
            base.Upgrade(levels, isRelative);
            m_playerMovement.BaseMovementSpeed += levels * (m_originalBaseSpeed * (GameManager.Instance.scaleOfNextLevel - 1) / (float)UpgradesManager.Instance.maxUpgrades);
            m_playerVehicleManager.UpdateVehicle();
            UpdateCost();
        }
    }
}
