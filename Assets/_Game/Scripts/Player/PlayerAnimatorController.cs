using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi.Player
{
    public class PlayerAnimatorController : MonoBehaviour
    {
        [SerializeField] private List<Animator> animators;

        private PlayerMovement m_playerMovement;

        private void Start()
        {
            m_playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            foreach (var animator in animators)
                animator.SetBool("IsMoving", m_playerMovement.IsMoving);
        }
    }
}
