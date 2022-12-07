using UnityEngine;
using Pathfinding;
using Pathfinding.RVO;

namespace Aezakmi.Animals
{
    public class MoveToShelterController : MonoBehaviour
    {
        private Animator _animator;
        private AIPath _aiPath;
        private RVOController m_rvoController;

        private void Awake()
        {
            _aiPath = GetComponent<AIPath>();
            _animator = GetComponent<Animator>();
            _animator.SetBool("IsRunning", true);
        }

        private void Start()
        {
            m_rvoController = GetComponent<RVOController>();
            Debug.LogWarning($"Prelayer: {m_rvoController.layer}. Precollider: {m_rvoController.collidesWith}");
            m_rvoController.layer = RVOLayer.MoveToShelter;
            m_rvoController.collidesWith = RVOLayer.MoveToShelter;
            Debug.LogWarning($"Prelayer: {m_rvoController.layer}. Precollider: {m_rvoController.collidesWith}");
        }

        public void SetDestination(Vector3 pos)
        {
            _aiPath.destination = pos;
            _aiPath.canMove = true;
        }
    }
}
