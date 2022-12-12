using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Aezakmi.CameraMechanics;
using Aezakmi.UI;
using Aezakmi.Player;

namespace Aezakmi
{
    public class GameManager : GloballyAccessibleBase<GameManager>
    {
        public float scaleOfNextLevel; // so maxing capacity will bring player from scale(1,1,1) to scale(scaleOfNextLevel * Vector3.one)

        public int animalsRescued = 0;
        public int totalAnimals = 160;
        public bool isTransitioningLevel = false;

        private List<int> m_animalRescuesNeededToSpawnWave = new List<int>() { 57, 117 };
        private bool m_proceedFlag = false;

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

        public void AnimalRescued()
        {
            animalsRescued++;

            if (m_animalRescuesNeededToSpawnWave.Contains(animalsRescued))
                StartWave();

            if (animalsRescued >= totalAnimals && !m_proceedFlag)
            {
                ProceedToNextLevel();
            }
        }

        [Button]
        private void ProceedToNextLevel()
        {
            m_proceedFlag = true;
            PlayerController.Instance.ToggleNewLevelIndicator();
            ReferenceManager.Instance.levelEndpoint.gameObject.SetActive(true);
            isTransitioningLevel = true;
        }

        public void TravelledToNextLevel()
        {
            LevelCompleteUI.Instance.OpenUI();
        }

        public void ContinueToNextLevel()
        {
            // todo: vidi sta ces sa viskom novca
        }

        private void CountTimePlayed()
        {
            if (GameDataManager.Instance == null) return;
            GameDataManager.Instance.gameData.timePlayed += Time.unscaledDeltaTime;
        }

        [Button]
        public void StartWave()
        {
            SpawnManager.Instance.SendWave();
            CameraController.Instance.StartWaveBehaviour();
            WaveMessageUI.Instance.PlayMessage();
        }
    }
}
