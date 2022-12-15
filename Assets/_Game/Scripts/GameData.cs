using Aezakmi.AchievementSystem;
using Aezakmi.Skins;

namespace Aezakmi
{
    [System.Serializable]
    public class GameData
    {
        public int levelReached = 1;
        public int missionID = 0;
        public bool firstTimePlaying = true;
        public bool soundsActive = true;
        public bool vibrationsActive = true;
        public int timesWaveCameraIsForced = 2;
        public bool firstLevelFinished = false;

        // Scenes
        public int[] lastLevelReachedPerRegion = new int[5] { -1, 0, 0, 0, 0 };
        public bool[] regionsVisited = new bool[5] { false, true, false, false, false };
        public int lastRegionPlayed = (int)Region.City;

        // Player
        public float distanceTravelled = 0f;

        // Skins
        public int gems = 0;
        public bool[] skinsBought = new bool[SkinsManager.TOTAL_SKINS];
        public int activeSkinIndex = 0;

        // Achievements
        public bool[] claimedList = new bool[AchievementsManager.ACHIEVEMENTS_COUNT];
        public float timePlayed = 0;

        // Upgrades
        public int money = 500000;
        public int totalMoney = 0; // Amount of money gathered in total.
        public int upgradesBought = 0;
        public int[] upgradeLevels = new int[3]; // Capacity, Range, Speed.
        public int[] relativeUpgradeLevels = new int[3]; // Amount of times upgraded in scene.

        // Drones
        public int droneCooldownLevel = 0;
        public int droneCapacityLevel = 0;
    }
}
