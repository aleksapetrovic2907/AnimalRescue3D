using UnityEngine;
using Pathfinding;

namespace Aezakmi.Animals
{
    public class WanderController : MonoBehaviour
    {
        [SerializeField] private float radius;
        [SerializeField] private bool drawGizmos;

        private AIPath _aiPath;

        public const float NODE_SIZE = 1f;
        public const int UNWANDERABLE_TAG_INDEX = 1;
        public const int WAVEAREA_TAG_INDEX = 2;
        public const int MAX_FAILED_SEARCHES = 15;

        private const float DESTINATION_DISTANCE_MARGIN = .6f;

        private void Start()
        {
            _aiPath = GetComponent<AIPath>();
            MoveToRandomPoint();
        }

        private void Update()
        {
            if (!_aiPath.pathPending && (_aiPath.reachedEndOfPath || !_aiPath.hasPath))
            {
                MoveToRandomPoint();
                _aiPath.SearchPath();
            }
        }

        private Vector3 RandomPosition(Vector3 origin, float radius)
        {
            Vector3 randPosition = Vector3.zero;
            Vector3 targetPosition = Vector3.zero;

            for (int i = 0; i < MAX_FAILED_SEARCHES; i++)
            {
                randPosition = (Random.insideUnitSphere * radius) + transform.position;
                var nearestNode = AstarData.active.GetNearest(randPosition).node;

                if (nearestNode.Walkable)
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

        public void StopWandering() => _aiPath.isStopped = true;
        private void MoveToRandomPoint() => _aiPath.destination = RandomPosition(transform.position, radius);

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