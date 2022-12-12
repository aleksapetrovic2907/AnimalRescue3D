using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Aezakmi.UpgradeMechanics
{
    [RequireComponent(typeof(Button))]
    public class UpgradeButton : MonoBehaviour
    {
        public Button Button;
        public TextMeshProUGUI Level;
        public TextMeshProUGUI Cost;
        public GameObject MaxedOutText;
        public Image Background;

        private static Color s_greyedOutColor = new Color(0.3490566f, 0.3490566f, 0.3490566f);

        private const string LEVEL_PREFIX = "LVL ";

        public void UpdateData(int level, int cost)
        {
            Level.text = LEVEL_PREFIX + (level + 1).ToString();
            Cost.text = StringsManager.ShortNotation(cost);
        }

        public void SetMaxedOut()
        {
            Button.interactable = false;
            Cost.gameObject.SetActive(false);
            MaxedOutText.SetActive(true);
            Background.color = s_greyedOutColor;
        }

        public void SetUnmaxed()
        {
            Button.interactable = true;
            Cost.gameObject.SetActive(true);
            MaxedOutText.SetActive(false);
        }

        public virtual void SetAffordability(bool isAffordable)
        {
            Button.interactable = isAffordable;
            Background.color = isAffordable ? Color.white : s_greyedOutColor;
        }
    }
}
