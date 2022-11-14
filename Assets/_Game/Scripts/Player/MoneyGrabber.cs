using UnityEngine;

namespace Aezakmi.Player
{
    public class MoneyGrabber : MonoBehaviour
    {
        [SerializeField] private Vector3 RaycastSize;
        [SerializeField] private LayerMask RaycastLayers;
        [SerializeField] private Mesh CapsuleMesh;
        [SerializeField] private Vector3 Center;

        private Collider[] _hitMoney;

        private void Update()
        {
            _hitMoney = Physics.OverlapCapsule((transform.position + Center) - Vector3.up * RaycastSize.y / 2, (transform.position + Center) + Vector3.up * RaycastSize.y / 2, RaycastSize.x / 2, RaycastLayers);

            if (_hitMoney.Length <= 0)
                return;

            // ! VibrationsManager.VibrateSoft();

            foreach (var money in _hitMoney)
            {
                // ! var moneyComponent = money.GetComponent<Money>();
                // ! UpgradeManager.Instance.GrabMoney(moneyComponent.Amount);
                // ! moneyComponent.DestroySelf();
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 1, 0, .25f);
            Gizmos.DrawWireMesh(CapsuleMesh, -1, transform.position + Center, Quaternion.identity, RaycastSize);
        }
#endif
    }
}
