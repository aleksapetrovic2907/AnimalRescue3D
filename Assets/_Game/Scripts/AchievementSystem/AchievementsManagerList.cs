using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Aezakmi.AchievementSystem
{
    // Contains all the achievements
    public partial class AchievementsManager
    {
        public void LoadAchievements()
        {
            achievements = new List<Achievement>();
            achievements.Add(new Achievement("Marathoner", "Walk 42km", (object o) => GameDataManager.Instance.gameData.distanceTravelled >= 42f, delegate { return Mathf.Clamp(GameDataManager.Instance.gameData.distanceTravelled / 42f, 0, 1); }, 5));
        }
    }


}