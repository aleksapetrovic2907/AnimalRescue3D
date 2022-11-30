using UnityEngine;
using UnityEngine.UI;

namespace Aezakmi
{
    public class SettingsManager : MonoBehaviour
    {
        [SerializeField] private GameObject settingsParent;
        [SerializeField] private Toggle soundsToggle;
        [SerializeField] private Toggle vibrationsToggle;

        private void Start()
        {
            RefreshUI();
            soundsToggle.onValueChanged.AddListener(delegate { GameDataManager.Instance.gameData.soundsActive = !GameDataManager.Instance.gameData.soundsActive; });
            vibrationsToggle.onValueChanged.AddListener(delegate { GameDataManager.Instance.gameData.vibrationsActive = !GameDataManager.Instance.gameData.vibrationsActive; });
        }

        public void OpenSettingsMenu()
        {
            RefreshUI();
            settingsParent.SetActive(true);
        }

        public void CloseSettingsMenu()
        {
            settingsParent.SetActive(false);
        }

        private void RefreshUI()
        {
            soundsToggle.isOn = GameDataManager.Instance.gameData.soundsActive;
            vibrationsToggle.isOn = GameDataManager.Instance.gameData.vibrationsActive;
        }
    }
}
