using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi.Player
{
    public class PlayerSkinController : GloballyAccessibleBase<PlayerSkinController>
    {
        [SerializeField] private List<GameObject> skins;

        private int m_activeSkin = 0;
        private CatchController m_catchController;

        private void Start()
        {
            m_catchController = GetComponent<CatchController>();
            LoadSkinData();
        }

        private void LoadSkinData()
        {
            if (GameDataManager.Instance == null) return;

            skins[0].SetActive(false);
            m_activeSkin = GameDataManager.Instance.gameData.activeSkinIndex;
            skins[m_activeSkin].SetActive(true);
        }

        public void SkinChanged()
        {
            skins[m_activeSkin].SetActive(false);
            m_activeSkin = GameDataManager.Instance.gameData.activeSkinIndex;
            skins[m_activeSkin].SetActive(true);

            m_catchController.SetNewHands(skins[m_activeSkin].GetComponent<MeshHandsController>());
            PlayerAnimatorController.Instance.UpdateVehicle();
        }


    }
}
