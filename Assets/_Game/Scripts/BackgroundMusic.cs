using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi
{
    public class BackgroundMusic : SingletonBase<BackgroundMusic>
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private List<AudioClip> musics;

        private int m_songCount = 0;

        private void Start()
        {
            audioSource.clip = musics[m_songCount];
        }

        private void Update()
        {
            if (GameDataManager.Instance == null) return;
            audioSource.mute = !GameDataManager.Instance.gameData.soundsActive;

            if (!audioSource.isPlaying)
            {
                m_songCount = (m_songCount + 1) % musics.Count;
                audioSource.clip = musics[m_songCount];
                audioSource.Play();
            }
        }
    }
}
