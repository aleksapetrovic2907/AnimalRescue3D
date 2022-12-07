using UnityEngine;

namespace Aezakmi
{
    public class GameManager : GloballyAccessibleBase<GameManager>
    {
        public int currentLevel;
        public float scaleOfNextLevel; // so maxing capacity will bring player from scale(1,1,1) to scale(scaleOfNextLevel * Vector3.one)

        public int totalAnimals = 160;
        public float rescuesPercentageToPassLevel = .95f;


        public void AddMoney(int value)
        {
            if (GameDataManager.Instance == null) return;
            GameDataManager.Instance.gameData.money += value;
            GameDataManager.Instance.gameData.totalMoney += value;
        }

        public void LoseMoney(int value)
        {
            GameDataManager.Instance.gameData.money -= value;
        }

        private void Update()
        {
            CountTimePlayed();
        }

        private void CountTimePlayed()
        {
            if (GameDataManager.Instance == null) return;
            GameDataManager.Instance.gameData.timePlayed += Time.unscaledDeltaTime;
        }
    }
}
