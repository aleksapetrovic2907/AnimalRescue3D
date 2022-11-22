using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Aezakmi.AchievementSystem;

namespace Aezakmi
{
    public class GameDataManager : SingletonBase<GameDataManager>
    {
        public GameData gameData = new GameData();

        private const string SAVEDATA_FILE_NAME = "/gameData.bin";

        private void Start()
        {
            Debug.Log(Application.persistentDataPath + SAVEDATA_FILE_NAME);
            LoadGameData();

            // Since achievements' predicates (requirements) depend on gameData
            // we first initialize the game data and then the achievement data
            AchievementsManager.Instance.LoadAchievements();
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
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Create(Application.persistentDataPath + SAVEDATA_FILE_NAME);
            bf.Serialize(fs, gameData);
            fs.Close();
        }
        #endregion
    }
}
