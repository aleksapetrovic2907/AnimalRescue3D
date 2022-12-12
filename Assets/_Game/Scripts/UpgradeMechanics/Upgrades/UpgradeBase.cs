using UnityEngine;

namespace Aezakmi.UpgradeMechanics
{
    public abstract class UpgradeBase : MonoBehaviour
    {
        public int level = 0;
        public int relativeLevel = 0; // The amount of times upgraded in current scene
        public int baseCost;

        protected virtual void Start() => baseCost = UpgradesManager.Instance.baseCost;

        /// <summary>
        /// Increases level of the upgrade and executes logic that comes with it.
        /// </summary>
        /// <param name="levels">Positive integer of levels we want to increase by or increase to.</param>
        /// <param name="isRelative">If isRelative is true, we take current level into account.</param>
        public virtual void Upgrade(int levels, bool isRelative)
        {
            level = isRelative ? level + levels : levels;
            relativeLevel = isRelative ? relativeLevel + levels : relativeLevel;
            FeedbackManager.Instance.Upgraded();
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

        public virtual void UpdateCost()
        {
            float modifier = 0;

            if (relativeLevel <= 3) modifier = UpgradesManager.Instance.firstWaveConstant;
            else if (relativeLevel <= 6) modifier = UpgradesManager.Instance.secondWaveConstant;
            else modifier = UpgradesManager.Instance.thirdWaveConstant;

            baseCost = (int)(baseCost * modifier);
        }
    }
}
