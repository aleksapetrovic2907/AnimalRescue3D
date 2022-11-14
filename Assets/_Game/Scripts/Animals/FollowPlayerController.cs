#pragma warning disable 618

using UnityEngine;
using UnityEngine.AI;
using Aezakmi.Player;
using Pathfinding;
using NaughtyAttributes;

namespace Aezakmi.Animals
{
    public class FollowPlayerController : MonoBehaviour
    {
        [SerializeField] private float DistanceToStartWaiting;

        private Animator _animator;
        private Seeker _seeker;
        private AIPath _aiPath;
        private Vector3 _playerPosition;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _seeker = GetComponent<Seeker>();
            _aiPath = GetComponent<AIPath>();
            _seeker.traversableTags |= (1 << 1);
        }

        private void Update()
        {
            _playerPosition = PlayerController.Instance.transform.position;

            if (IsPlayerTooClose(_playerPosition))
            {
                _animator.SetBool("IsRunning", false);
                _aiPath.canMove = false;
            }
            else
            {
                _animator.SetBool("IsRunning", true);
                _aiPath.isStopped = false;
                _aiPath.canMove = true;
                _aiPath.maxSpeed = PlayerMovement.Instance.TotalMovementSpeed;
                _aiPath.destination = _playerPosition;
            }
        }

        private bool IsPlayerTooClose(Vector3 playerPosition)
        {
            playerPosition = new Vector3(playerPosition.x, 0f, playerPosition.z);
            var myPos = new Vector3(transform.position.x, 0f, transform.position.z);

            return Vector3.Distance(playerPosition, myPos) <= DistanceToStartWaiting;
        }

        public void StopFollowing() => _aiPath.canMove = false;
    }
}
