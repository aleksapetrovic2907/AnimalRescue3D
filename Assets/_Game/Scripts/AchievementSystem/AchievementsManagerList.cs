using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace Aezakmi.AchievementSystem
{
    // Contains all the achievements
    public partial class AchievementsManager
    {
        public const int ACHIEVEMENTS_COUNT = 17;

        [SerializeField] private List<Sprite> achievementSprites;

        public void LoadAchievements()
        {
            achievements = new List<Achievement>();

            achievements.Add(new Achievement("Christmas Gift!", "Merry Christmas!", (object o) => GameDataManager.Instance.gameData.distanceTravelled >= 0, delegate { return 1f; }, 5, achievementSprites[0]));

            // Playtime
            achievements.Add(new Achievement("The Adopter", "Play 5 minutes.", (object o) => GameDataManager.Instance.gameData.timePlayed >= 300f, delegate { return Mathf.Clamp(GameDataManager.Instance.gameData.timePlayed / 300f, 0, 1); }, 2, achievementSprites[1]));
            achievements.Add(new Achievement("The Rescuer", "Play 25 minutes.", (object o) => GameDataManager.Instance.gameData.timePlayed >= 1500f, delegate { return Mathf.Clamp(GameDataManager.Instance.gameData.timePlayed / 1500f, 0, 1); }, 5, achievementSprites[2]));
            achievements.Add(new Achievement("The Savior", "Play 60 minutes.", (object o) => GameDataManager.Instance.gameData.timePlayed >= 3600f, delegate { return Mathf.Clamp(GameDataManager.Instance.gameData.timePlayed / 3600f, 0, 1); }, 10, achievementSprites[3]));

            // Distance travelled
            achievements.Add(new Achievement("Jogger", "Travel a total of 3km.", (object o) => GameDataManager.Instance.gameData.distanceTravelled >= 3f, delegate { return Mathf.Clamp(GameDataManager.Instance.gameData.distanceTravelled / 3f, 0, 1); }, 2, achievementSprites[4]));
            achievements.Add(new Achievement("Sprinter", "Travel a total of 24km.", (object o) => GameDataManager.Instance.gameData.distanceTravelled >= 24f, delegate { return Mathf.Clamp(GameDataManager.Instance.gameData.distanceTravelled / 24f, 0, 1); }, 5, achievementSprites[5]));
            achievements.Add(new Achievement("Marathoner", "Travel a total of 42km.", (object o) => GameDataManager.Instance.gameData.distanceTravelled >= 42f, delegate { return Mathf.Clamp(GameDataManager.Instance.gameData.distanceTravelled / 42f, 0, 1); }, 10, achievementSprites[6]));

            // Money collected
            achievements.Add(new Achievement("Jogger", "Gain 50000 dollars.", (object o) => GameDataManager.Instance.gameData.totalMoney >= 50000, delegate { return Mathf.Clamp(GameDataManager.Instance.gameData.totalMoney / 50000, 0, 1); }, 2, achievementSprites[7]));
            achievements.Add(new Achievement("Sprinter", "Gain 200000 dollars.", (object o) => GameDataManager.Instance.gameData.totalMoney >= 200000, delegate { return Mathf.Clamp(GameDataManager.Instance.gameData.totalMoney / 200000, 0, 1); }, 5, achievementSprites[8]));
            achievements.Add(new Achievement("Millioner", "Gain 1000000 dollars.", (object o) => GameDataManager.Instance.gameData.totalMoney >= 1000000, delegate { return Mathf.Clamp(GameDataManager.Instance.gameData.totalMoney / 1000000, 0, 1); }, 10, achievementSprites[9]));

            // Skins bought
            achievements.Add(new Achievement("Dress-up", "Get a skin.", (object o) => HowManyTrue(GameDataManager.Instance.gameData.skinsBought) >= 2, delegate { return Mathf.Clamp(HowManyTrue(GameDataManager.Instance.gameData.skinsBought) - 1, 0, 1); }, 2, achievementSprites[10]));
            achievements.Add(new Achievement("Cosplayer", "Get 3 skins.", (object o) => HowManyTrue(GameDataManager.Instance.gameData.skinsBought) >= 4, delegate { return Mathf.Clamp((HowManyTrue(GameDataManager.Instance.gameData.skinsBought) - 1) / 3f, 0, 1); }, 5, achievementSprites[11]));
            achievements.Add(new Achievement("Fashionista", "Get 6 skins.", (object o) => HowManyTrue(GameDataManager.Instance.gameData.skinsBought) >= 7, delegate { return Mathf.Clamp((HowManyTrue(GameDataManager.Instance.gameData.skinsBought) - 1) / 6f, 0, 1); }, 10, achievementSprites[12]));

            // Regions unlocked
            achievements.Add(new Achievement("Passenger", "Travel to 2 regions.", (object o) => HowManyTrue(GameDataManager.Instance.gameData.regionsVisited) >= 2, delegate { return Mathf.Clamp(HowManyTrue(GameDataManager.Instance.gameData.regionsVisited) / 2f, 0, 1); }, 2, achievementSprites[13]));
            achievements.Add(new Achievement("Tourist", "Travel to 3 regions.", (object o) => HowManyTrue(GameDataManager.Instance.gameData.regionsVisited) >= 3, delegate { return Mathf.Clamp(HowManyTrue(GameDataManager.Instance.gameData.regionsVisited) / 3f, 0, 1); }, 5, achievementSprites[14]));
            achievements.Add(new Achievement("Globetrotter", "Travel to 4 regions.", (object o) => HowManyTrue(GameDataManager.Instance.gameData.regionsVisited) >= 4, delegate { return Mathf.Clamp(HowManyTrue(GameDataManager.Instance.gameData.regionsVisited) / 4f, 0, 1); }, 10, achievementSprites[15]));

            achievements.Add(new Achievement("King Rescuer", "Get every achievement!", (object o) => achievements.FindAll(a => a.achieved == true).Count == (ACHIEVEMENTS_COUNT - 1), delegate { return Mathf.Clamp(achievements.FindAll(a => a.achieved == true).Count / (float)(ACHIEVEMENTS_COUNT - 1), 0, 1); }, 20, achievementSprites[16]));

            LoadAchievementClaims();
        }

        // Loads whether achievements are claimed or not
        private void LoadAchievementClaims()
        {
            for (int i = 0; i < achievements.Count; i++)
                achievements[i].claimed = GameDataManager.Instance.gameData.claimedList[i];
        }

        [Button]
        public void PrintTotalAchievements()
        {
            Debug.Log(achievements.FindAll(a => a.achieved).Count);
        }

        // Returns how many bool's are true in an array
        public static int HowManyTrue(bool[] array)
        {
            int sum = 0;
            for (int i = 0; i < array.Length; i++)
                if (array[i] == true) sum++;

            return sum;
        }

    }
}