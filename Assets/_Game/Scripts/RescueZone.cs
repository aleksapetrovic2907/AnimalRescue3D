using System.Collections.Generic;
using UnityEngine;
using Aezakmi.Animals;
using Aezakmi.Player;
using Aezakmi.Drone;
using TMPro;

namespace Aezakmi
{
    public class RescueZone : MonoBehaviour
    {
        [SerializeField] private TextMeshPro groundText;
        [SerializeField] private MoneyThrower moneyThrower;

        private void Start() => groundText.text = GameManager.Instance.animalsRescued + "/" + GameManager.Instance.totalAnimals;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(GameTags.AnimalCollider)) return;

            other.transform.parent.tag = GameTags.RescuedAnimal;

            var ac = other.transform.parent.GetComponent<AnimalController>();
            var followingPlayer = ac.GetComponent<FollowPlayerController>().followingPlayer;
            if (followingPlayer)
                ReferenceManager.Instance.player.GetComponent<CatchController>().AnimalRescued(ac);
            else
                DroneController.Instance.AnimalRescued(ac);

            moneyThrower.ThrowMoney(ac.MoneyWorth);
            ac.MoveToShelter(ReferenceManager.Instance.shelterEndpoint);
            GameManager.Instance.AnimalRescued();
            groundText.text = GameManager.Instance.animalsRescued + "/" + GameManager.Instance.totalAnimals;
        }
    }
}
