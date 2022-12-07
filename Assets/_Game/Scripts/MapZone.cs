using UnityEngine;
using DG.Tweening;

namespace Aezakmi
{
    public class MapZone : MonoBehaviour
    {
        [SerializeField] private float fillDuration;
        [SerializeField] private Transform spriteMask;

        private Vector3 m_maskOutsidePosition;
        private Tween m_tween;

        private void Start()
        {
            m_maskOutsidePosition = spriteMask.localPosition;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(GameTags.Player)) return;

            m_tween = spriteMask.transform.DOLocalMove(Vector3.zero, fillDuration).SetEase(Ease.Linear).OnComplete(delegate
            {
                MapManager.Instance.EnterMapZone();
            }).Play();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag(GameTags.Player)) return;

            var timePassedNormalized = m_tween.position;
            m_tween.Kill();
            m_tween = spriteMask.transform.DOLocalMove(m_maskOutsidePosition, fillDuration * timePassedNormalized).SetEase(Ease.Linear).Play();
        }
    }
}
