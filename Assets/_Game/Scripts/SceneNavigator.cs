using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Aezakmi.UI;

namespace Aezakmi
{
    public class SceneNavigator : SingletonBase<SceneNavigator>
    {
        #region LOADING_SCREEN
        private AsyncOperation m_operation;

        public void LoadLastSavedScene()
        {
            var lastSavedScene = GameDataManager.Instance.gameData.levelReached;
            StartCoroutine(BeginLoad(lastSavedScene));
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
