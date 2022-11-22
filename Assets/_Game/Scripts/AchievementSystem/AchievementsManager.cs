using System.Collections.Generic;

namespace Aezakmi.AchievementSystem
{
    public partial class AchievementsManager : SingletonBase<AchievementsManager>
    {
        public List<Achievement> achievements;

        public void UpdateAchievementsStatus()
        {
            foreach (var achievement in achievements)
            {
                if (!achievement.achieved)
                {
                    achievement.UpdateStatus();

                    if (achievement.achieved)
                        AchievementUnlocked(achievement);
                }
            }
        }

        public void AchievementUnlocked(Achievement achievement)
        {
            // gems += achievement.gemBonus;
        }
    }
}
