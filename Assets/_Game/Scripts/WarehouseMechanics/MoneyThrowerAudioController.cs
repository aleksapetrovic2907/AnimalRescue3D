using UnityEngine;

namespace Aezakmi
{
    public class MoneyThrowerAudioController : MonoBehaviour
    {
        [SerializeField] private MoneyThrower moneyThrower;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private float volume;
        [SerializeField] private float lerpSpeed = 4f;
        private float m_targetVolume = 0;

        private void Update()
        {
            m_targetVolume = GameDataManager.Instance.gameData.soundsActive && moneyThrower.HasMoneyToThrow ? volume : 0f;
            audioSource.volume = Mathf.Lerp(audioSource.volume, m_targetVolume, lerpSpeed * Time.deltaTime);
        }
    }
}
