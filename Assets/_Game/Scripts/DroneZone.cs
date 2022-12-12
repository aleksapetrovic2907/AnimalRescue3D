using UnityEngine;
using DG.Tweening;
using Aezakmi.Player;

namespace Aezakmi.Drone
{
    public class DroneZone : MonoBehaviour
    {
        [SerializeField] private float maskFillDuration;
        [SerializeField] private Transform spriteMask;
        [SerializeField] private SpriteRenderer circleRenderer;

        private float m_timer = 0f;
        private bool m_isOnCooldown;

        private Vector3 m_maskOutsidePosition;
        private Tween m_tween;

        private void Start()
        {
            m_maskOutsidePosition = spriteMask.localPosition;
        }

        private void Update()
        {
            if (!m_isOnCooldown) return;

            m_timer += Time.deltaTime;
            var cooldownDuration = DroneController.Instance.cooldownDuration;
            var cooldownNormalized = m_timer / cooldownDuration;
            circleRenderer.material.SetFloat("_Arc1", 360f - cooldownNormalized * 360f);

            if (m_timer >= cooldownDuration)
            {
                m_timer = 0f;
                m_isOnCooldown = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(GameTags.Player)) return;
            if (m_isOnCooldown) return;
            if (CatchController.Instance.animalAndLeash.Count == 0) return;

            m_tween = spriteMask.transform.DOLocalMove(Vector3.zero, maskFillDuration).SetEase(Ease.Linear).OnComplete(delegate
            {
                CatchController.Instance.EnteredDroneZone();
                m_tween.Kill();
                spriteMask.transform.localPosition = m_maskOutsidePosition;
                StartCooldown();
            }).Play();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag(GameTags.Player)) return;

            var timePassedNormalized = m_tween.position;
            m_tween.Kill();
            m_tween = spriteMask.transform.DOLocalMove(m_maskOutsidePosition, maskFillDuration * timePassedNormalized).SetEase(Ease.Linear).Play();
        }

        private void StartCooldown()
        {
            m_isOnCooldown = true;
            circleRenderer.material.SetFloat("_Arc1", 360f);
        }
    }
}
