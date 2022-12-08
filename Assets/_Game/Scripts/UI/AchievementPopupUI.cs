using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

namespace Aezakmi.AchievementSystem.UI
{
    public class AchievementPopupUI : MonoBehaviour
    {
        [SerializeField] private float waitDuration;
        [SerializeField] private float moveDuration;
        [SerializeField] private Ease ease;

        private RectTransform m_rectTransform;
        private Sequence m_sequence;
        private Vector3 m_upPosition;

        private void Start()
        {
            m_rectTransform = GetComponent<RectTransform>();
            m_upPosition = m_rectTransform.anchoredPosition;
        }

        [Button]
        public void DisplayPopup()
        {
            if (m_sequence != null)
            {
                m_rectTransform.anchoredPosition = m_upPosition;
                m_sequence.Kill();
            }

            m_sequence = DOTween.Sequence();

            var positionDown = m_upPosition;
            positionDown.y *= -1;

            Tween moveDown = m_rectTransform.DOAnchorPos(positionDown, moveDuration).SetEase(ease);
            Tween moveUp = m_rectTransform.DOAnchorPos(m_upPosition, moveDuration).SetEase(ease);

            m_sequence.Append(moveDown).AppendInterval(waitDuration).Append(moveUp).Play();
        }
    }
}
