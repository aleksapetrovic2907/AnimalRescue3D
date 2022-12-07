using System.Collections.Generic;
using UnityEngine;
using Aezakmi.Animals;

namespace Aezakmi
{
    public class FeedbackManager : MonoBehaviour
    {
        [SerializeField] private GameObject animalCaptured_VFX;

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
    }
}
