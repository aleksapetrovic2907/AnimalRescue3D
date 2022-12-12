using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Aezakmi
{
    public class DroneButtonUI : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image background;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI cost;
        [SerializeField] private GameObject maxedOutText;

        private static Color s_greyedOutColor = new Color(0.3490566f, 0.3490566f, 0.3490566f);

        public void UpdateInfo(int level, int cost, bool isAffordable)
        {
            levelText.text = "LVL " + (level + 1).ToString();
            this.cost.text = cost.ToString();
            button.interactable = isAffordable;
            background.color = isAffordable ? Color.white : s_greyedOutColor;
        }


        public void MaxedOut(int level)
        {
            levelText.text = "LVL " + (level + 1).ToString();
            button.interactable = false;
            cost.gameObject.SetActive(false);
            maxedOutText.SetActive(true);
            background.color = s_greyedOutColor;
        }
    }
}
