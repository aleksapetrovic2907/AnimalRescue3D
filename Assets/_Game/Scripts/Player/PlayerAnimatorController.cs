using UnityEngine;

namespace Aezakmi.Player
{
    public class PlayerAnimatorController : MonoBehaviour
    {
        private Animator _animator;
        private PlayerMovement _playerMovement;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            _animator.SetBool("IsMoving", _playerMovement.IsMoving);
        }

        public void StopMoving()
        {
            _animator.SetBool("IsMoving", false);
        }
    }
}
