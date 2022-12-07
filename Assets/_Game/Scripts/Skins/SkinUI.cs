using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Aezakmi.Skins
{
    public class SkinUI : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI cost;
        [SerializeField] private Image image;
        [SerializeField] private GameObject buyButton;
        [SerializeField] private Image activeBorder;
        [SerializeField] private Color unactiveColor;
        [SerializeField] private Color activeColor;

        private Skin m_skin;
        private int m_index;

        public void SetInfo(Skin skin, int index)
        {
            m_skin = skin;
            m_index = index;
            cost.text = skin.cost.ToString();

            image.sprite = m_skin.sprite;
        }

        public void UpdateUI()
        {
            activeBorder.color = m_index == GameDataManager.Instance.gameData.activeSkinIndex ? activeColor : unactiveColor;
            buyButton.SetActive(!GameDataManager.Instance.gameData.skinsBought[m_index]);
            button.interactable = GameDataManager.Instance.gameData.gems >= m_skin.cost || m_skin.bought;
        }
    }
}
