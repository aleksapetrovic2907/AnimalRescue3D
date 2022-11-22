using System;
using UnityEngine.Events;

namespace Aezakmi.AchievementSystem
{
    [System.Serializable]
    public class Achievement
    {
        public string title;
        public string description;
        public bool achieved = false;
        public int gemBonus; // Amount of gems earned by achieving achievement
        public Predicate<object> requirements;
        public Func<float> percentage;

        public Achievement(string t, string d, Predicate<object> reqs, Func<float> perc, int gems)
        {
            title = t;
            description = d;
            requirements = reqs;
            percentage = perc;
            gemBonus = gems;
            achieved = RequirementsMet();
        }

        public void UpdateStatus()
        {
            if (achieved) return;
            achieved = RequirementsMet();
        }

        public bool RequirementsMet()
        {
            return requirements.Invoke(null);
        }
    }
}
