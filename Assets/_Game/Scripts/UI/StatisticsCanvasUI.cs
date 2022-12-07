using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Aezakmi
{
    public class StatisticsCanvasUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private TextMeshProUGUI gemText;

        private int m_lastMoneyValue = -1;
        private int m_lastGemValue = -1;

        private void Update()
        {
            if (GameDataManager.Instance == null) return;

            if (m_lastMoneyValue != GameDataManager.Instance.gameData.money)
            {
                m_lastMoneyValue = GameDataManager.Instance.gameData.money;
                moneyText.text = StringsManager.ShortNotation(m_lastMoneyValue);
            }

            if (m_lastGemValue != GameDataManager.Instance.gameData.gems)
            {
                m_lastGemValue = GameDataManager.Instance.gameData.gems;
                gemText.text = m_lastGemValue.ToString();
            }
        }
    }
}
