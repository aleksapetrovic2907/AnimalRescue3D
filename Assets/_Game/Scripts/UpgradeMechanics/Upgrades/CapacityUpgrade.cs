using UnityEngine;

namespace Aezakmi.UpgradeMechanics
{
    public class CapacityUpgrade : UpgradeBase
    {
        public override void Upgrade(int levels, bool isRelative)
        {
            base.Upgrade(levels, isRelative);

            Debug.Log($"Capacity is now {4 + level}");
        }

        public override int Cost(int level)
        {
            return level * 100;
        }
    }
}
