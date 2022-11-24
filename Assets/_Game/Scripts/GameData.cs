using Aezakmi.AchievementSystem;

namespace Aezakmi
{
    [System.Serializable]
    public class GameData
    {
        public int levelReached = 1;
        public bool firstTimePlaying = true;

        // Player related
        public float distanceTravelled = 0f;
        public int upgradesBought = 0;

        // Achievements
        public bool[] claimedList = new bool[AchievementsManager.ACHIEVEMENTS_COUNT];
    }
}
