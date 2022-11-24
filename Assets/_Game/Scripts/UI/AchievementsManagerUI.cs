using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace Aezakmi.AchievementSystem.UI
{
    public class AchievementsManagerUI : MonoBehaviour
    {
        [SerializeField] private GameObject achievementsMenu;
        [SerializeField] private GameObject achievementPrefab;
        [SerializeField] private Transform achievementsParent;

        private List<AchievementUI> achievementUIs = new List<AchievementUI>();

        private void Start()
        {
            LoadMenu();
        }

        public void OpenAchievementsMenu()
        {
            UpdateMenu();
            achievementsMenu.SetActive(true);
        }

        public void CloseAchievementsMenu() => achievementsMenu.SetActive(false);

        private void LoadMenu()
        {
            Debug.Log("Achievements count: " + AchievementsManager.Instance.achievements.Count);
            for (int i = 0; i < AchievementsManager.Instance.achievements.Count; i++)
            {
                var ach = Instantiate(achievementPrefab, achievementsParent).GetComponent<AchievementUI>();
                ach.SetInfo(AchievementsManager.Instance.achievements[i]);
                var index = i;
                ach.claimButton.onClick.AddListener(delegate { AchievementsManager.Instance.AchievementClaimed(AchievementsManager.Instance.achievements[index]); UpdateMenu(); });
                achievementUIs.Add(ach);
            }
            Debug.Log("Added achievements sum: " + achievementUIs.Count.ToString());
        }

        [Button]
        private void UpdateMenu()
        {
            foreach (var ach in achievementUIs)
                ach.UpdateInfo();
        }
    }
}
