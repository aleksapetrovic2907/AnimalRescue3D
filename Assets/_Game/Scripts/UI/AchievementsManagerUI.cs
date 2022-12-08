using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Aezakmi.Tweens;

namespace Aezakmi.AchievementSystem.UI
{
    public class AchievementsManagerUI : GloballyAccessibleBase<AchievementsManagerUI>
    {
        [SerializeField] private GameObject achievementsMenu;
        [SerializeField] private GameObject achievementPrefab;
        [SerializeField] private Transform achievementsParent;
        [SerializeField] private AchievementPopupUI popup;
        [SerializeField] private GameObject achievedNotification; // Little icon that is on the side of the achievements button.

        private List<AchievementUI> achievementUIs = new List<AchievementUI>();

        private void Start()
        {
            LoadMenu();
        }

        public void OpenAchievementsMenu()
        {
            UpdateMenu();
            achievementsMenu.SetActive(true);
            achievedNotification.SetActive(false);
        }

        public void CloseAchievementsMenu() => achievementsMenu.SetActive(false);

        private void LoadMenu()
        {
            for (int i = 0; i < AchievementsManager.Instance.achievements.Count; i++)
            {
                var ach = Instantiate(achievementPrefab, achievementsParent).GetComponent<AchievementUI>();
                ach.SetInfo(AchievementsManager.Instance.achievements[i]);
                var index = i;
                ach.claimButton.onClick.AddListener(delegate { AchievementsManager.Instance.AchievementClaimed(AchievementsManager.Instance.achievements[index]); UpdateMenu(); });
                achievementUIs.Add(ach);
            }
        }

        private void UpdateMenu()
        {
            foreach (var ach in achievementUIs)
                ach.UpdateInfo();
        }

        public void AchievementAchieved()
        {
            // todo: popup achievement unlocked
            popup.DisplayPopup();
            achievedNotification.SetActive(true);
        }
    }
}
