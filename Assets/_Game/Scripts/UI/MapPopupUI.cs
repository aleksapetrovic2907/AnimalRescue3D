using UnityEngine;
using TMPro;

namespace Aezakmi
{
    public class MapPopupUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI goToText;

        public void SetRegion(Region region)
        {
            titleText.text = region.ToString();
            goToText.text = region.ToString() + "?";
        }

        public void Go() => MapManager.Instance.ConfirmRegion();
        public void No() => gameObject.SetActive(false);
    }
}
