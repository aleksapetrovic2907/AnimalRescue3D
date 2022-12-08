using UnityEngine;
using Pathfinding;

namespace Aezakmi.Animals
{
    public class WaveBehaviour : MonoBehaviour
    {
        private AIPath _aiPath;
        private Seeker _seeker;
        private WanderController _wanderController;

        private const float DISTANCE = 1f;
        private const float ACCEPTED_DISTANCE_TO_END = 3f;

        public void SetDestination(Vector3 wanderableAreaPos)
        {
            var randomRange = Random.insideUnitSphere * DISTANCE;
            randomRange.y = 0;
            transform.position += randomRange;

            _aiPath = GetComponent<AIPath>();
            _seeker = GetComponent<Seeker>();
            _wanderController = GetComponent<WanderController>();
            GetComponent<Animator>().SetBool("IsRunning", true);
            Debug.Log(_seeker.traversableTags);
            _seeker.traversableTags |= 4; // be able to move in wave area // 2^n
            _aiPath.destination = wanderableAreaPos;
        }

        private void Update()
        {
            if (_aiPath.remainingDistance <= ACCEPTED_DISTANCE_TO_END)
            {
                _seeker.traversableTags |= 4;
                _wanderController.enabled = true;
                Destroy(this);
            }
        }
    }
}
