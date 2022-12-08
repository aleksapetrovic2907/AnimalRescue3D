using UnityEngine;
using Pathfinding;
using Pathfinding.RVO;

namespace Aezakmi.Animals
{
    public class WanderController : MonoBehaviour
    {
        [SerializeField] private float radius;
        [SerializeField] private bool drawGizmos;

        private AIPath m_aiPath;
        private float m_timer = 0f;
        private Vector3 m_lastCheckedPosition;
        private float m_maxStuckDistance; // If animal travelled distance less than this, it's stuck.

        public static float s_lastAnimalPriority = 0f;

        private const float TIME_BEFORE_CHECK_IF_STUCK = 1f;

        public const float NODE_SIZE = 1f;
        public const int UNWANDERABLE_TAG_INDEX = 1;
        public const int WAVEAREA_TAG_INDEX = 2;
        public const int MAX_FAILED_SEARCHES = 15;

        private const float DESTINATION_DISTANCE_MARGIN = .6f;

        private void Start()
        {
            s_lastAnimalPriority += .01f;
            GetComponent<RVOController>().priority = s_lastAnimalPriority;
            m_aiPath = GetComponent<AIPath>();
            m_lastCheckedPosition = transform.position;
            m_maxStuckDistance = m_aiPath.maxSpeed * TIME_BEFORE_CHECK_IF_STUCK * .5f;
            MoveToRandomPoint();
            m_aiPath.SearchPath();
        }

        private void Update()
        {
            m_timer += Time.deltaTime;

            if (m_timer >= TIME_BEFORE_CHECK_IF_STUCK)
            {
                m_timer = 0f;
                CheckIfStuck();
            }

            if (!m_aiPath.pathPending && (m_aiPath.reachedEndOfPath || !m_aiPath.hasPath))
            {
                MoveToRandomPoint();
                m_aiPath.SearchPath();
            }
        }

        private void CheckIfStuck()
        {
            if (Vector3.Distance(transform.position, m_lastCheckedPosition) <= m_maxStuckDistance)
            {
                MoveToRandomPoint();
                m_aiPath.SearchPath();
            }


            m_lastCheckedPosition = transform.position;
        }

        private Vector3 RandomPosition(Vector3 origin, float radius)
        {
            Vector3 randPosition = Vector3.zero;
            Vector3 targetPosition = Vector3.zero;

            for (int i = 0; i < MAX_FAILED_SEARCHES; i++)
            {
                randPosition = (Random.insideUnitSphere * radius) + transform.position;
                var nearestNode = AstarData.active.GetNearest(randPosition).node;

                if (nearestNode.Walkable && nearestNode.Tag != WAVEAREA_TAG_INDEX && nearestNode.Tag != UNWANDERABLE_TAG_INDEX)
                {
                    targetPosition = (Vector3)nearestNode.position;
                    break;
                }
                else if (i == MAX_FAILED_SEARCHES - 1)
                {
                    StopWandering();
                    this.enabled = false;
                }
            }

            return targetPosition;
        }

        public void StopWandering() => m_aiPath.isStopped = true;
        private void MoveToRandomPoint() => m_aiPath.destination = RandomPosition(transform.position, radius);

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!drawGizmos) return;
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
#endif
    }
}