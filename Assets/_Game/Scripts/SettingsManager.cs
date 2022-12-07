using UnityEngine;
using UnityEngine.UI;
using Aezakmi.UI;

namespace Aezakmi
{
    public class SettingsManager : MonoBehaviour
    {
        [SerializeField] private GameObject settingsParent;
        [SerializeField] private Button soundsButton;
        [SerializeField] private Button vibrationsButton;
        [SerializeField] private ToggleUI soundsToggle;
        [SerializeField] private ToggleUI vibrationsToggle;



        private void Start()
        {
            RefreshUI();
            soundsButton.onClick.AddListener(delegate { GameDataManager.Instance.gameData.soundsActive = !GameDataManager.Instance.gameData.soundsActive; RefreshUI(); });
            vibrationsButton.onClick.AddListener(delegate { GameDataManager.Instance.gameData.vibrationsActive = !GameDataManager.Instance.gameData.vibrationsActive; RefreshUI(); });
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
            soundsToggle.ChangeActive(GameDataManager.Instance.gameData.soundsActive);
            vibrationsToggle.ChangeActive(GameDataManager.Instance.gameData.vibrationsActive);
        }
    }
}
