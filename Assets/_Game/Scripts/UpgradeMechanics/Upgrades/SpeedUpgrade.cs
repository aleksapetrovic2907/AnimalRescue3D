using UnityEngine;

namespace Aezakmi.UpgradeMechanics
{
    public class SpeedUpgrade : UpgradeBase
    {
        public override void Upgrade(int levels, bool isRelative)
        {
            base.Upgrade(levels, isRelative);

            Debug.Log($"Speed is now {5 * level}km/h");
        }

        public override int Cost(int level)
        {
            return level * 0;
        }
    }
}
