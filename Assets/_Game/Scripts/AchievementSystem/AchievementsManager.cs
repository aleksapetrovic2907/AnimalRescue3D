using System.Collections.Generic;
using UnityEngine;
using Aezakmi.AchievementSystem.UI;

namespace Aezakmi.AchievementSystem
{
    public partial class AchievementsManager : SingletonBase<AchievementsManager>
    {
        public List<Achievement> achievements;

        private void Update()
        {
            UpdateAchievementsStatus();
        }

        public void UpdateAchievementsStatus()
        {
            foreach (var achievement in achievements)
            {
                if (!achievement.achieved)
                {
                    achievement.UpdateStatus();

                    if (achievement.achieved && AchievementsManagerUI.Instance != null)
                    {
                        AchievementsManagerUI.Instance.AchievementAchieved();
                    }
                }
            }
        }

        public void AchievementClaimed(Achievement achievement)
        {
            achievement.claimed = true;
            GameDataManager.Instance.gameData.gems += achievement.gemBonus;
        }
    }
}
