using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Aezakmi.Skins
{
    public partial class SkinsManager
    {
        [Header("UI")]
        [SerializeField] private GameObject skinsMenuParent;
        [SerializeField] private GameObject skinUiPrefab;
        [SerializeField] private Transform skinsContent;

        private List<SkinUI> m_skinUIs = new List<SkinUI>();

        public void AddSkinsToMenu()
        {
            for (int i = 0; i < skins.Count; i++)
            {
                var skinUi = Instantiate(skinUiPrefab, Vector3.zero, Quaternion.identity, skinsContent).GetComponent<SkinUI>();
                skinUi.SetInfo(skins[i], i);
                var index = i;
                skinUi.GetComponent<Button>().onClick.AddListener(delegate { BuySkin(index); });
                m_skinUIs.Add(skinUi);
            }
        }

        public void RefreshUI()
        {
            foreach (var skinUI in m_skinUIs)
                skinUI.UpdateUI();
        }

        public void OpenSkinsMenu()
        {
            RefreshUI();
            skinsMenuParent.SetActive(true);
        }
        public void CloseSkinsMenu() => skinsMenuParent.SetActive(false);
    }
}
