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

// using UnityEngine;
// using Pathfinding;
//
// namespace Aezakmi.Animals
// {
//     public class WanderController : MonoBehaviour
//     {
//         [SerializeField] private Vector2 MinMaxWanderRadius;
//
//         private AIPath _aiPath;
//         private AIDestinationSetter _aiDestinationSetter;
//
//         public const float NODE_SIZE = 1f;
//         public const int UNWANDERABLE_TAG_INDEX = 1;
//         public const int WAVEAREA_TAG_INDEX = 2;
//
//         private const float DESTINATION_DISTANCE_MARGIN = .6f;
//
//         private void Start()
//         {
//             _aiPath = GetComponent<AIPath>();
//             _aiDestinationSetter = GetComponent<AIDestinationSetter>();
//             MoveToRandomPoint();
//         }
//
//         private void Update()
//         {
//             if (_aiPath.remainingDistance <= DESTINATION_DISTANCE_MARGIN || !_aiPath.hasPath)
//                 MoveToRandomPoint();
//         }
//
//         /// <summary>Returns a random point on graph within a radius</summary>
//         private Vector3 RandomPosition(Vector3 origin, Vector2 minMaxRadius)
//         {
//             Vector3 randPosition;
//             NNInfo nearestNode;
//             GraphNode myNode;
//             var count = 0;
//
//             do
//             {
//                 count++;
//                 var randSphere = Random.insideUnitSphere;
//                 randSphere.y = 0f;
//                 randPosition = randSphere.normalized * Random.Range(minMaxRadius.x, minMaxRadius.y);
//                 randPosition += origin;
//                 nearestNode = AstarPath.active.GetNearest(randPosition);
//                 myNode = AstarPath.active.GetNearest(transform.position).node;
//             } while (Vector3.Distance(randPosition, nearestNode.position) > NODE_SIZE ||
//                      nearestNode.node.Tag == UNWANDERABLE_TAG_INDEX || nearestNode.node.Tag == WAVEAREA_TAG_INDEX ||
//                      !PathUtilities.IsPathPossible(myNode, nearestNode.node));
//
//             return nearestNode.position;
//         }
//
//         public void StopWandering() => _aiPath.isStopped = true;
//
//         private void MoveToRandomPoint() =>
//             _aiPath.destination = RandomPosition(transform.position, MinMaxWanderRadius);
//
// #if UNITY_EDITOR
//         private void OnDrawGizmos()
//         {
//             Gizmos.color = Color.red;
//             Gizmos.DrawWireSphere(transform.position, MinMaxWanderRadius.x);
//             Gizmos.color = Color.green;
//             Gizmos.DrawWireSphere(transform.position, MinMaxWanderRadius.y);
//         }
// #endif
//     }
// }