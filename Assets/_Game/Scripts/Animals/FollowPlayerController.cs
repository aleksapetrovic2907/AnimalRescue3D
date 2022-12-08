#pragma warning disable 618

using UnityEngine;
using Aezakmi.Player;
using Pathfinding;
using Pathfinding.RVO;

namespace Aezakmi.Animals
{
    public class FollowPlayerController : MonoBehaviour
    {
        public Transform targetToFollow;

        [SerializeField] private float DistanceToStartWaiting;

        private Animator m_animator;
        private Seeker m_seeker;
        private AIPath m_aiPath;
        private RVOController m_rvoController;

        private void Start()
        {
            m_animator = GetComponent<Animator>();
            m_seeker = GetComponent<Seeker>();
            m_aiPath = GetComponent<AIPath>();
            m_rvoController = GetComponent<RVOController>();
            m_seeker.traversableTags |= (1 << 1);
            m_rvoController.layer = RVOLayer.FollowPlayer;
            m_rvoController.collidesWith = RVOLayer.FollowPlayer;
        }

        private void Update()
        {
            if (IsPlayerTooClose(targetToFollow.position))
            {
                m_animator.SetBool("IsRunning", false);
                m_aiPath.canMove = false;
            }
            else
            {
                m_animator.SetBool("IsRunning", true);
                m_aiPath.isStopped = false;
                m_aiPath.canMove = true;
                m_aiPath.maxSpeed = PlayerMovement.Instance.TotalMovementSpeed;
                m_aiPath.destination = targetToFollow.position;
            }
        }

        private bool IsPlayerTooClose(Vector3 playerPosition)
        {
            playerPosition = new Vector3(playerPosition.x, 0f, playerPosition.z);
            var myPos = new Vector3(transform.position.x, 0f, transform.position.z);

            return Vector3.Distance(playerPosition, myPos) <= DistanceToStartWaiting;
        }

        public void StopFollowing() => m_aiPath.canMove = false;
        public void SetTarget(Transform target) => targetToFollow = target;
    }
}
