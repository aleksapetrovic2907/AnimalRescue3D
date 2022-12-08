using UnityEngine;

namespace Aezakmi
{
    public class MoneyEjector : MonoBehaviour
    {
        [SerializeField] private MoneyThrower moneyThrower;
        [SerializeField] private new ParticleSystem particleSystem;
        [SerializeField] private float shakeAmount;
        [SerializeField] private Vector3 shakingScale;
        [SerializeField] private float scaleSpeed;

        private Vector3 m_originalPos;
        private Vector3 m_originalScale;
        private Vector3 m_targetScale;

        private void OnEnable()
        {
            m_originalPos = transform.localPosition;
            m_originalScale = transform.localScale;
        }

        private void Update()
        {
            if (!moneyThrower.HasMoneyToThrow)
            {
                particleSystem.Stop();
                transform.localPosition = m_originalPos;
                m_targetScale = m_originalScale;
            }
            else
            {
                particleSystem.Play();
                transform.localPosition = m_originalPos + Random.insideUnitSphere * shakeAmount;
                m_targetScale = shakingScale;
            }

            transform.localScale = Vector3.Lerp(transform.localScale, m_targetScale, scaleSpeed * Time.deltaTime);
        }
    }
}
