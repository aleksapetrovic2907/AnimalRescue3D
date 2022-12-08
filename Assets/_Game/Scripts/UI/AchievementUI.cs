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
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI gemBonus;
        [SerializeField] private TextMeshProUGUI completionText;
        [SerializeField] private Slider completion;
        [SerializeField] private TextMeshProUGUI claimedText;

        private Achievement m_achievement;

        public void SetInfo(Achievement achievement)
        {
            m_achievement = achievement;

            icon.sprite = achievement.icon;
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

            claimButton.interactable = m_achievement.achieved && !m_achievement.claimed;
            claimedText.text = m_achievement.claimed ? "CLAIMED" : "CLAIM";
        }
    }
}
