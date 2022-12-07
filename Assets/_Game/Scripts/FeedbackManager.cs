using System.Collections.Generic;
using UnityEngine;
using Aezakmi.Animals;

namespace Aezakmi
{
    public class FeedbackManager : GloballyAccessibleBase<FeedbackManager>
    {
        [SerializeField] private GameObject animalCaptured_VFX;

        [SerializeField] private AudioClip upgrade_SFX;
        [SerializeField] private AudioSource audioSource;

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
        }

        public void Upgraded()
        {
            audioSource.PlayOneShot(upgrade_SFX);
        }
    }
}
