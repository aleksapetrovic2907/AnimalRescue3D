using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi.LeashBehaviours
{
    public class LeashChain : LeashBase
    {
        [SerializeField] private GameObject linkPrefab;

        private const float ROTATION_OFFSET = 90f;

        private List<GameObject> m_links = new List<GameObject>();
        private int m_linkCount;
        private float m_linkMeshLength;

        private void Awake()
        {
            m_linkMeshLength = linkPrefab.GetComponent<MeshRenderer>().bounds.size.y;
        }

        protected override void Update()
        {
            base.Update();

            SetChainCount();
            RotateTowardsNeckband();
        }

        private void SetChainCount()
        {
            m_linkCount = Mathf.CeilToInt((Vector3.Distance(origin.position, end.position) / m_linkMeshLength) / transform.localScale.y);

            if (m_linkCount > m_links.Count) // Instantiate missing links
            {
                for (int i = m_links.Count; i < m_linkCount; i++)
                {
                    var link = Instantiate(linkPrefab, transform.position, Quaternion.identity, transform);
                    link.transform.localPosition = i * m_linkMeshLength * Vector3.up;
                    link.transform.localEulerAngles = new Vector3(90, 0, 0) + (i % 2) * new Vector3(0, 0, 90f); // Rotate every second link by 90 degrees
                    m_links.Add(link);
                }
            }
            else if (m_linkCount < m_links.Count) // Destroy excess links
            {
                for (int i = m_links.Count - 1; i >= m_linkCount; i--)
                {
                    Destroy(m_links[i].gameObject);
                    m_links.Remove(m_links[i]);
                }
            }
        }

        private void RotateTowardsNeckband()
        {
            transform.localEulerAngles = Vector3.zero;
            transform.LookAt(end, Vector3.up);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x + ROTATION_OFFSET, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }
    }
}