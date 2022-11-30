using UnityEngine;

namespace Aezakmi.UpgradeMechanics
{
    public class CapacityUpgrade : UpgradeBase
    {
        public override void Upgrade(int levels, bool isRelative)
        {
            base.Upgrade(levels, isRelative);

            ReferenceManager.Instance.player.transform.localScale += levels * ((GameManager.Instance.scaleOfNextLevel - 1f) / (float)UpgradesManager.Instance.maxUpgrades) * Vector3.one;
        }

        public override int Cost(int level)
        {
            return 0;
        }
    }
}
