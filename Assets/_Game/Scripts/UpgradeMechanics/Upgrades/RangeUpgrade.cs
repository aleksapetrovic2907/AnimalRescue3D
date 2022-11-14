using UnityEngine;

namespace Aezakmi.UpgradeMechanics
{
    public class RangeUpgrade : UpgradeBase
    {
        public override void Upgrade(int levels, bool isRelative)
        {
            base.Upgrade(levels, isRelative);

            Debug.Log($"Range is now {2 * level}");
        }

        public override int Cost(int level)
        {
            return level * 0;
        }
    }
}
