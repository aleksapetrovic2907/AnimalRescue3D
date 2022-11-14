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
            // ! var targetTransitioningBonus = GameManager.Instance.IsPlayerTransitioning && MoneyParent.childCount == 0 ? BaseMovementSpeed * SpeedBonusMultiplierWhenTransitioning : 0;
            // ! var targetFullBonus = _playerController.IsFull ? BaseMovementSpeed * SpeedBonusMultiplierWhenFull : 0;
            // ! _isTransitioningSpeedBonus = Mathf.Lerp(_isTransitioningSpeedBonus, targetTransitioningBonus, LerpSpeed * Time.deltaTime);
            // ! _isFullSpeedBonus = Mathf.Lerp(_isFullSpeedBonus, targetFullBonus, LerpSpeed * Time.deltaTime);
        }
    }
}
