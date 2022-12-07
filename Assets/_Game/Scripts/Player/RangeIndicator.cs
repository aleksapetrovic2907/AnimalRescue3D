using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

namespace Aezakmi.Player
{
    public class RangeIndicator : MonoBehaviour
    {
        [SerializeField] private CatchController catchController;
        [SerializeField] private SpriteRenderer rangeSpriteRenderer;
        [SerializeField] private float disappearDuration;
        [SerializeField] private float disappearDelay;
        private Tween m_tween;

        private void LateUpdate()
        {
            // Range indicator scale must be independent from root scale
            transform.localScale = (catchController.raycastRadius * Vector3.one) / ReferenceManager.Instance.player.transform.localScale.x;
        }

        [Button]
        public void Display()
        {
            if (m_tween != null) m_tween.Kill();

            var color = rangeSpriteRenderer.color;
            color.a = 1f;
            rangeSpriteRenderer.color = color;
        }

        [Button]
        public void Disappear()
        {
            var transparentColor = rangeSpriteRenderer.color;
            transparentColor.a = 0f;

            m_tween = rangeSpriteRenderer.DOColor(transparentColor, disappearDuration).SetDelay(disappearDelay).Play();
        }
    }
}
