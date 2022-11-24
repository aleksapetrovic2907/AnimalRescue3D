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
        public const int ACHIEVEMENTS_COUNT = 4;

        public void LoadAchievements()
        {
            achievements = new List<Achievement>();
            achievements.Add(new Achievement("Marathoner", "Walk 42km", (object o) => GameDataManager.Instance.gameData.distanceTravelled >= 42f, delegate { return Mathf.Clamp(GameDataManager.Instance.gameData.distanceTravelled / 42f, 0, 1); }, 5));
            achievements.Add(new Achievement("Testicles", "Walk 4km", (object o) => GameDataManager.Instance.gameData.distanceTravelled >= 42f, delegate { return Mathf.Clamp(GameDataManager.Instance.gameData.distanceTravelled / 42f, 0, 1); }, 5));
            achievements.Add(new Achievement("Pussy", "Walk 12km", (object o) => GameDataManager.Instance.gameData.distanceTravelled >= 42f, delegate { return Mathf.Clamp(GameDataManager.Instance.gameData.distanceTravelled / 42f, 0, 1); }, 5));
            achievements.Add(new Achievement("The Nigger", "Walk 30km", (object o) => GameDataManager.Instance.gameData.distanceTravelled >= 42f, delegate { return Mathf.Clamp(GameDataManager.Instance.gameData.distanceTravelled / 42f, 0, 1); }, 5));

            LoadAchievementClaims();
        }

        // Loads whether achievements are claimed or not
        private void LoadAchievementClaims()
        {
            for (int i = 0; i < achievements.Count; i++)
                achievements[i].claimed = GameDataManager.Instance.gameData.claimedList[i];
        }
    }
}