using UnityEngine;
using Aezakmi.Player;

namespace Aezakmi.UpgradeMechanics
{
    public class CapacityUpgrade : UpgradeBase
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
            ReferenceManager.Instance.player.transform.localScale += levels * ((GameManager.Instance.scaleOfNextLevel - 1f) / (float)UpgradesManager.Instance.maxUpgrades) * Vector3.one;
            if (UpgradesManager.Instance.capacityLevelsAtWhichWavesStart.Contains(relativeLevel))
            {
                // todo: DO NOT DO IT LIKE THIS, RETARD
                SpawnManager.Instance.SendWave();
                Debug.Log("Updated modifier.");
            }

            m_catchController.UpdateCapacity();

            UpdateCost();
        }
    }
}
