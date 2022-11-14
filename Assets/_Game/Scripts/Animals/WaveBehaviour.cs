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

        public void SetDestination(Vector3 wanderableAreaPos)
        {
            var randomRange = Random.insideUnitSphere * DISTANCE;
            randomRange.y = 0;
            transform.position += randomRange;

            _aiPath = GetComponent<AIPath>();
            _seeker = GetComponent<Seeker>();
            _wanderController = GetComponent<WanderController>();
            GetComponent<Animator>().SetBool("IsRunning", true);
            _seeker.traversableTags |= 2; // be able to move in wave area
            _aiPath.destination = wanderableAreaPos;
        }

        private void Update()
        {
            if (_aiPath.reachedDestination)
            {
                _seeker.traversableTags |= 2;
                _wanderController.enabled = true;
                Destroy(this);
            }
        }
    }
}
