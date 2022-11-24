using System.Collections.Generic;
using UnityEngine;

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
                }
            }
        }

        public void AchievementClaimed(Achievement achievement)
        {
            achievement.claimed = true;

            foreach (var test in achievements)
            {
                if(test.claimed == true)
                    Debug.LogWarning("Works.");
            }
        }
    }
}
