using UnityEngine;
using Aezakmi.Player;

namespace Aezakmi.UpgradeMechanics
{
    public class UpgradeZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GameTags.Player))
            {
                UpgradesManager.Instance.EnteredShopArea();
                PlayerController.Instance.EnterUpgradeZone();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(GameTags.Player))
            {
                UpgradesManager.Instance.LeftShopArea();
            }
        }
    }
}
