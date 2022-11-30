using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using Aezakmi.AchievementSystem;
using Aezakmi.UpgradeMechanics;

namespace Aezakmi
{
    public class GameDataManager : SingletonBase<GameDataManager>
    {
        public GameData gameData = new GameData();

        private const string SAVEDATA_FILE_NAME = "/gameData.bin";

        private void Start()
        {
            LoadGameData();

            // Since achievements' predicates (requirements) depend on gameData
            // we first initialize the game data and then the achievement data
            AchievementsManager.Instance.LoadAchievements();
            SceneManager.LoadScene(1);
        }

        private void OnApplicationQuit() => SaveGameData();

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

            for (int i = 0; i < gameData.upgradeLevels.Length; i++)
                gameData.upgradeLevels[i] = UpgradesManager.Instance.upgrades[i].level;

            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Create(Application.persistentDataPath + SAVEDATA_FILE_NAME);
            bf.Serialize(fs, gameData);
            fs.Close();
        }

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
