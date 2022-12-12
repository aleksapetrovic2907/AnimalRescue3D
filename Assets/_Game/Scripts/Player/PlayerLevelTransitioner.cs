using UnityEngine;
using DG.Tweening;
using Aezakmi.UpgradeMechanics;
using Aezakmi.UI;

namespace Aezakmi.Player
{
    public class PlayerLevelTransitioner : MonoBehaviour
    {
        private PlayerMovement m_playerMovement;
        private PlayerAnimatorController m_playerAnimatorController;
        private Tween m_tween = null;
        private bool m_calculatedTween = false;
        private bool m_openedShop = false;

        private void Update()
        {
            if (!m_calculatedTween) return;

            if (!UpgradesManager.Instance.AreAllUpgradesMaxed())
            {
                if (m_openedShop) return;
                m_playerAnimatorController.ForceMove(false);
                UpgradesManager.Instance.EnteredShopArea();
                ShopUI.Instance.ForceUpgradesText(true);
                m_openedShop = true;
                return;
            }

            UpgradesManager.Instance.LeftShopArea();
            ShopUI.Instance.ForceUpgradesText(false);
            m_playerAnimatorController.ForceMove(true);
            m_calculatedTween = false;
            m_tween.Play();
        }

        private void OnEnable()
        {
            m_playerMovement = GetComponent<PlayerMovement>();
            m_playerAnimatorController = GetComponent<PlayerAnimatorController>();
            m_playerAnimatorController.ForceMove(true);


            // Movement
            var endPosition = ReferenceManager.Instance.newLevelStartpoint.position;
            var distanceToNextLevel = Vector3.Distance(transform.position, endPosition);
            var crossingSpeed = m_playerMovement.TotalMovementSpeed;
            var timeToCross = distanceToNextLevel / crossingSpeed;

            // Rotation
            var direction = endPosition - transform.position;
            transform.forward = direction;

            m_playerMovement.CanMove = false;

            m_tween = transform.DOMove(endPosition, timeToCross).SetEase(Ease.Linear).OnComplete(delegate
            {
                m_playerAnimatorController.ForceMove(false);
                GameManager.Instance.TravelledToNextLevel();
            });

            m_calculatedTween = true;
        }
    }
}
