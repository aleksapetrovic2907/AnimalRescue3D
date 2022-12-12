using System.Collections.Generic;
using UnityEngine;
using Aezakmi.Animals;

namespace Aezakmi
{
    public class FeedbackManager : GloballyAccessibleBase<FeedbackManager>
    {
        [SerializeField] private GameObject animalCaptured_VFX;

        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip money_SFX;
        [SerializeField] private AudioClip upgrade_SFX;
        [SerializeField] private AudioClip capture_SFX;
        [SerializeField] private AudioClip achievement_SFX;

        private void OnEnable()
        {
            EventManager.StartListening(GameEvents.AnimalCaptured, AnimalCaptured);
        }

        private void OnDisable()
        {
            EventManager.StopListening(GameEvents.AnimalCaptured, AnimalCaptured);
        }

        private void AnimalCaptured(Dictionary<string, object> message)
        {
            var animal = (Transform)message["animal"];
            var topOfHead = animal.GetComponent<AnimalController>().topOfHead;
            Instantiate(animalCaptured_VFX, topOfHead.position, Quaternion.identity, topOfHead);
            if (GameDataManager.Instance.gameData.soundsActive)
                audioSource.PlayOneShot(capture_SFX);
            VibrationsManager.Instance.Vibrate(VibrationType.Soft);
        }

        public void CapturedMoney()
        {
            if (GameDataManager.Instance.gameData.soundsActive)
                audioSource.PlayOneShot(money_SFX);
            VibrationsManager.Instance.Vibrate(VibrationType.Soft);
        }

        public void Upgraded()
        {
            if (GameDataManager.Instance.gameData.soundsActive)
                audioSource.PlayOneShot(upgrade_SFX);
            VibrationsManager.Instance.Vibrate(VibrationType.Soft);
        }

        public void AchievementAchieved()
        {
            if (GameDataManager.Instance.gameData.soundsActive)
                audioSource.PlayOneShot(achievement_SFX);
            VibrationsManager.Instance.Vibrate(VibrationType.Soft);
        }
    }
}
