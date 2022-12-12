using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using NaughtyAttributes;

using Aezakmi.AchievementSystem;
using Aezakmi.Skins;

namespace Aezakmi
{
    public class GameDataManager : SingletonBase<GameDataManager>
    {
        public GameData gameData = new GameData();

        private const string SAVEDATA_FILE_NAME = "/gameData.bin";

        private void Start()
        {
#if TTP_ANALYTICS || TTP_REWARDED_INTERSTITIALS || TTP_PRIVACY_SETTINGS || TTP_APPSFLYER || TTP_REWARDED_ADS || TTP_PROMOTION || TTP_INTERSTITIALS || TTP_GAMEPROGRESSION || TTP_RATEUS || TTP_BANNERS || TTP_POPUPMGR || TTP_CRASHTOOL || TTP_OPENADS
            Tabtale.TTPlugins.TTPCore.Setup();
#endif

            LoadGameData();

            // Since achievements' predicates (requirements) depend on gameData
            // we first initialize the game data and then the achievement data
            AchievementsManager.Instance.LoadAchievements();
            SceneNavigator.Instance.LoadScenesData();
            SceneNavigator.Instance.LoadLastSavedScene();
        }

        #region IO
        private void LoadGameData()
        {
            if (!File.Exists(Application.persistentDataPath + SAVEDATA_FILE_NAME)) return;

            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + SAVEDATA_FILE_NAME, FileMode.Open);
            gameData = bf.Deserialize(fs) as GameData;
            fs.Close();
        }

        public void SaveGameData()
        {
            for (int i = 0; i < AchievementsManager.Instance.achievements.Count; i++)
                gameData.claimedList[i] = AchievementsManager.Instance.achievements[i].claimed;

            for (int i = 0; i < SkinsManager.Instance.skins.Count; i++)
                gameData.skinsBought[i] = SkinsManager.Instance.skins[i].bought;

            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Create(Application.persistentDataPath + SAVEDATA_FILE_NAME);
            bf.Serialize(fs, gameData);
            fs.Close();
        }
#if UNITY_EDITOR
        private void OnApplicationQuit() => SaveGameData();
#endif

#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        private void OnApplicationFocus(bool hasFocus)
        {
            if(!hasFocus)
                SaveGameData();
        }
#endif

#if UNITY_EDITOR
        [Button]
        private void DeleteData()
        {
            var location = Application.persistentDataPath + SAVEDATA_FILE_NAME;
            if (!File.Exists(location))
            {
                Debug.LogError($"File at {location} cannot be deleted as it does not exist.");
                return;
            }

            File.Delete(location);
        }
#endif
        #endregion
    }
}
