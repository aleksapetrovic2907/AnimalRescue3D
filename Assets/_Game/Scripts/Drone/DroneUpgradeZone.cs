using UnityEngine;

namespace Aezakmi
{
    public class DroneUpgradeZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(GameTags.Player)) return;
            DroneShopUI.Instance.Open();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag(GameTags.Player)) return;
            DroneShopUI.Instance.Close();
        }
    }
}
