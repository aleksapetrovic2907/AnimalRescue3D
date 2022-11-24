using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Aezakmi.AchievementSystem.UI
{
    public class AchievementUI : MonoBehaviour
    {
        public Button claimButton;

        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private TextMeshProUGUI gemBonus;
        [SerializeField] private TextMeshProUGUI completionText;
        [SerializeField] private Slider completion;

        private Achievement m_achievement;

        public void SetInfo(Achievement achievement)
        {
            m_achievement = achievement;

            title.text = m_achievement.title;
            description.text = m_achievement.description;
            gemBonus.text = m_achievement.gemBonus.ToString();
            UpdateInfo();
        }

        public void UpdateInfo()
        {
            var percentage = m_achievement.percentage.Invoke();

            completionText.text = (Mathf.CeilToInt(percentage * 100f)).ToString() + "%";
            completion.value = percentage;

            bool isClaimable = !(m_achievement.claimed || !m_achievement.achieved);
            claimButton.gameObject.SetActive(isClaimable);
        }
    }
}
