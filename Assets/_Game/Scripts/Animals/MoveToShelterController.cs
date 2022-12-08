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

        private void OnEnable()
        {
            m_rvoController = GetComponent<RVOController>();
            m_rvoController.layer = RVOLayer.MoveToShelter;
            m_rvoController.collidesWith = RVOLayer.Layer9;
        }

        public void SetDestination(Vector3 pos)
        {
            _aiPath.destination = pos;
            _aiPath.canMove = true;
        }
    }
}
