using UnityEngine;
using Aezakmi.Player;

namespace Aezakmi.UpgradeMechanics
{
    public class SpeedUpgrade : UpgradeBase
    {
        private PlayerMovement m_playerMovement;
        private float m_originalBaseSpeed;

        private void Start()
        {
            m_playerMovement = ReferenceManager.Instance.player.GetComponent<PlayerMovement>();
            m_originalBaseSpeed = m_playerMovement.BaseMovementSpeed;
        }

        public override void Upgrade(int levels, bool isRelative)
        {
            base.Upgrade(levels, isRelative);

            m_playerMovement.BaseMovementSpeed = m_originalBaseSpeed + levels * m_originalBaseSpeed / (float)UpgradesManager.Instance.maxUpgrades;
        }

        public override int Cost(int level)
        {
            return level * 0;
        }
    }
}
