using UnityEngine;
using TMPro;

namespace Aezakmi.UI
{
    public class FPSCounterUI : MonoBehaviour
    {
        private TextMeshProUGUI m_fpsCounter;
        private float m_refreshRate = .2f;
        private float m_timer = 0f;

        private void Start() => m_fpsCounter = GetComponent<TextMeshProUGUI>();
        private void Update()
        {
            if (m_fpsCounter == null) return;

            m_timer += Time.deltaTime;

            if (m_timer > m_refreshRate)
            {
                m_fpsCounter.text = ((int)(1f / Time.unscaledDeltaTime)).ToString();
                m_timer = 0f;
            }
        }
    }
}
