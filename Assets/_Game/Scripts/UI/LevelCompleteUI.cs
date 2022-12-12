using UnityEngine;

namespace Aezakmi
{
    public class LevelCompleteUI : GloballyAccessibleBase<LevelCompleteUI>
    {
        [SerializeField] private GameObject parentUI;
        
        public void OpenUI() => parentUI.SetActive(true);
        public void Continue() => GameManager.Instance.ContinueToNextLevel();
    }
}
