using UnityEngine;
using DG.Tweening;

namespace Aezakmi.UI
{
    public class ShopUI : GloballyAccessibleBase<ShopUI>
    {
        [SerializeField] private GameObject maxAllUpgradesText;

        [Header("Tween Settings")]
        [SerializeField] private float moveDuration;
        [SerializeField] private Ease loopEase;

        private RectTransform m_rectTransform;
        private Vector2 m_downPosition;
        private Vector2 m_upPosition;

        private void Start()
        {
            m_rectTransform = GetComponent<RectTransform>();
            m_downPosition = m_rectTransform.anchoredPosition;
            m_upPosition = new Vector2(m_downPosition.x, -m_downPosition.y);
        }

        public void Open()
        {
            m_rectTransform.DOAnchorPos(m_upPosition, moveDuration).SetEase(loopEase).Play();
        }

        public void Close()
        {
            m_rectTransform.DOAnchorPos(m_downPosition, moveDuration).SetEase(loopEase).Play();
        }

        public void ForceUpgradesText(bool active)
        {
            maxAllUpgradesText.SetActive(active);
        }
    }
}
