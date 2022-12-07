using UnityEngine;
using Aezakmi.Player;

namespace Aezakmi.UpgradeMechanics
{
    public class RangeUpgrade : UpgradeBase
    {
        private CatchController m_catchController;

        protected override void Start()
        {
            base.Start();
            m_catchController = ReferenceManager.Instance.player.GetComponent<CatchController>();
        }

        public override void Upgrade(int levels, bool isRelative)
        {
            base.Upgrade(levels, isRelative);
            m_catchController.raycastRadius += levels * ((GameManager.Instance.scaleOfNextLevel - 1) / UpgradesManager.Instance.maxUpgrades);

            UpdateCost();
        }
    }
}
