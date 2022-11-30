using Aezakmi.AchievementSystem;

namespace Aezakmi
{
    [System.Serializable]
    public class GameData
    {
        public int levelReached = 1;
        public bool firstTimePlaying = true;
        public bool soundsActive = true;
        public bool vibrationsActive = true;

        // Player
        public float distanceTravelled = 0f;

        // Achievements
        public bool[] claimedList = new bool[AchievementsManager.ACHIEVEMENTS_COUNT];

        // Upgrades
        public int gems = 0;
        public int money = 0;
        public int upgradesBought = 0;
        public int[] upgradeLevels = new int[3]; // capacity, range, speed
    }
}
