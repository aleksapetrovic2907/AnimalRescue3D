using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Aezakmi.UI
{
    public class LoadingScreenManager : GloballyAccessibleBase<LoadingScreenManager>
    {
        [SerializeField] private Slider progressSlider;
        [SerializeField] private TextMeshProUGUI progressPercentage;

        public void UpdateUI(float progress)
        {
            progressSlider.value = progress + .1f;
            progressPercentage.text = ((progress + .1f) * 100f).ToString() + "%";
        }
    }
}
