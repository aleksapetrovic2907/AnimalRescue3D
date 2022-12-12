using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Aezakmi.UI;
using NativeSerializableDictionary;
using NaughtyAttributes;

namespace Aezakmi
{
    public class SceneNavigator : SingletonBase<SceneNavigator>
    {
        [SerializeField] private SerializableDictionary<Region, int> regionStartIndex = new SerializableDictionary<Region, int>();
        [SerializeField] private SerializableDictionary<Region, int> levelsPerRegion = new SerializableDictionary<Region, int>();

        private Region m_currentRegion = Region.City;
        private int m_currentRegionStartIndex = 0;


        public void LoadScenesData()
        {
            m_currentRegion = (Region)GameDataManager.Instance.gameData.lastRegionPlayed;
            m_currentRegionStartIndex = regionStartIndex[m_currentRegion].Value;

            var lastSavedScene = m_currentRegionStartIndex + GameDataManager.Instance.gameData.lastLevelReachedPerRegion[(int)m_currentRegion];
            StartCoroutine(BeginLoad(lastSavedScene));
        }

        public void GoToRegion(Region region)
        {
            if (m_currentRegion == region) return;

            m_currentRegion = region;
            GameDataManager.Instance.gameData.lastRegionPlayed = (int)m_currentRegion;
            GameDataManager.Instance.gameData.regionsVisited[(int)m_currentRegion] = true;
            m_currentRegionStartIndex = regionStartIndex[m_currentRegion].Value;

            SceneManager.LoadScene(m_currentRegionStartIndex + GameDataManager.Instance.gameData.lastLevelReachedPerRegion[(int)m_currentRegion]);
        }

        [Button]
        public void GoToNextLevel()
        {
            var lastLevelReached = GameDataManager.Instance.gameData.lastLevelReachedPerRegion[(int)m_currentRegion];
            lastLevelReached = (lastLevelReached + 1) % (levelsPerRegion[m_currentRegion].Value);
            GameDataManager.Instance.gameData.lastLevelReachedPerRegion[(int)m_currentRegion] = lastLevelReached;
            SceneManager.LoadScene(m_currentRegionStartIndex + lastLevelReached);
        }

        #region LOADING_SCREEN
        private AsyncOperation m_operation;

        public void LoadLastSavedScene()
        {
        }


        private IEnumerator BeginLoad(int sceneIndex)
        {
            m_operation = SceneManager.LoadSceneAsync(sceneIndex);

            while (!m_operation.isDone)
            {
                LoadingScreenManager.Instance.UpdateUI(m_operation.progress);
                yield return null;
            }

            LoadingScreenManager.Instance.UpdateUI(m_operation.progress);
            m_operation = null;
        }
        #endregion
    }
}
