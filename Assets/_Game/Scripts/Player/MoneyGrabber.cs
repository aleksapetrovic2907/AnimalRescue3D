using UnityEngine;

namespace Aezakmi.Player
{
    public class MoneyGrabber : MonoBehaviour
    {
        [SerializeField] private Vector3 raycastSize;
        [SerializeField] private LayerMask raycastLayers;
        [SerializeField] private bool drawGizmos;
        [SerializeField] private Mesh capsuleMesh;
        [SerializeField] private Vector3 center;

        private Collider[] m_hitMoney;

        private void Update()
        {
            var m_raycastSize = raycastSize * transform.lossyScale.x;
            m_hitMoney = Physics.OverlapCapsule((transform.position + center * transform.lossyScale.x) - Vector3.up * m_raycastSize.y / 2, (transform.position + center) + Vector3.up * m_raycastSize.y / 2, m_raycastSize.x / 2, raycastLayers);

            if (m_hitMoney.Length == 0)
                return;

            foreach (var money in m_hitMoney)
            {
                var mb = money.GetComponent<MoneyBehaviour>();
                GameManager.Instance.AddMoney(mb.value);
                mb.DestroySelf();
                // ! moneyComponent.DestroySelf();
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!drawGizmos) return;

            var m_raycastSize = raycastSize * transform.lossyScale.x;
            Gizmos.color = new Color(0, 1, 0, .25f);
            Gizmos.DrawWireMesh(capsuleMesh, -1, transform.position + center * transform.lossyScale.x, Quaternion.identity, m_raycastSize);
        }
#endif
    }
}
