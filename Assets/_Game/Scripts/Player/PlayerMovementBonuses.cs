using UnityEngine;

namespace Aezakmi.Player
{
    public partial class PlayerMovement
    {
        [Header("Speed Bonuses")]
        public float SpeedBonusMultiplierWhenFull;
        public float SpeedBonusMultiplierWhenTransitioning;
        public float LerpSpeed = 8f;

        private void UpdateBonuses()
        {
            var targetTransitioningBonus = GameManager.Instance.isTransitioningLevel && ReferenceManager.Instance.moneyParent.childCount == 0 ? BaseMovementSpeed * SpeedBonusMultiplierWhenTransitioning : 0;
            var targetFullBonus = m_catchController.IsFull() ? BaseMovementSpeed * SpeedBonusMultiplierWhenFull : 0;
            m_isTransitioningSpeedBonus = Mathf.Lerp(m_isTransitioningSpeedBonus, targetTransitioningBonus, LerpSpeed * Time.deltaTime);
            m_isFullSpeedBonus = Mathf.Lerp(m_isFullSpeedBonus, targetFullBonus, LerpSpeed * Time.deltaTime);
        }
    }
}
