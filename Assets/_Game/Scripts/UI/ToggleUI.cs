using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Aezakmi.UI
{
    public class ToggleUI : MonoBehaviour
    {
        public bool isOn = true;

        [SerializeField] private Image image;
        [SerializeField] private Sprite onSprite;
        [SerializeField] private Sprite offSprite;
        [SerializeField] private TextMeshProUGUI activeText;

        public void ChangeActive(bool active)
        {
            isOn = active;
            RefreshUI();
        }

        public void RefreshUI()
        {
            image.sprite = isOn ? onSprite : offSprite;
            activeText.text = isOn ? "ON" : "OFF";
        }
    }
}
