#pragma warning disable 618
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;

namespace Aezakmi.Animals
{
    public class MoveToShelterController : MonoBehaviour
    {
        private Animator _animator;
        private AIPath _aiPath;

        private void Awake()
        {
            _aiPath = GetComponent<AIPath>();
            _animator = GetComponent<Animator>();
            _animator.SetBool("IsRunning", true);
        }

        public void SetDestination(Vector3 pos)
        {
            _aiPath.destination = pos;
            _aiPath.canMove = true;
        }
    }
}
