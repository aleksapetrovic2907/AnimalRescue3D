using UnityEngine;
using NaughtyAttributes;

namespace Aezakmi
{
    public class GameData : SingletonBase<GameData>
    {
        public int levelReached;
        public bool firstTimePlaying;

        protected override void Awake()
        {
            base.Awake();
            LoadGameData();
        }

        private void LoadGameData()
        {
            levelReached = PlayerPrefs.GetInt("levelReached", 1);
            firstTimePlaying = PlayerPrefsExtra.GetBool("firstTimePlaying", true);
        }

        private void SaveGameData()
        {
            PlayerPrefs.SetInt("levelReached", levelReached);
        }

        [Button]
        private void DeleteAllData() => PlayerPrefs.DeleteAll();
    }
}
