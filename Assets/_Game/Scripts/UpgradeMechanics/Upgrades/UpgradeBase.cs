using UnityEngine;

namespace Aezakmi.UpgradeMechanics
{
    public abstract class UpgradeBase : MonoBehaviour
    {
        public int level = 1;

        /// <summary>
        /// Increases level of the upgrade and executes logic that comes with it.
        /// </summary>
        /// <param name="levels">Positive integer of levels we want to increase by or increase to.</param>
        /// <param name="isRelative">If isRelative is true, we take current level into account.</param>
        public virtual void Upgrade(int levels, bool isRelative)
        {
            level = isRelative ? level + levels : levels;
        }

        /// <summary>
        /// Decreases level of the upgrade and executes logic that comes with it.
        /// </summary>
        /// <param name="levels">Positive integer of levels we want to decrease by or decrease to.</param>
        /// <param name="isRelative">If isRelative is true, we take current level into account.</param>
        public virtual void Downgrade(int levels, bool isRelative)
        {
            level = isRelative ? level - levels : levels;
        }

        /// <summary>
        /// Returns the cost of buying an upgrade of a passed level. Usually determined by a formula.
        /// </summary>
        public abstract int Cost(int level);
    }
}
