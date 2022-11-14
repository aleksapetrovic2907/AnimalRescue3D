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

        private const string LEVEL_PREFIX = "LVL ";

        public void UpdateData(int level, int cost)
        {
            Level.text = LEVEL_PREFIX + level.ToString();
            Cost.text = cost.ToString();
        }

        public virtual void SetAffordability(bool isAffordable) => Button.interactable = isAffordable;
    }
}
