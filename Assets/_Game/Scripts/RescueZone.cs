using UnityEngine;
using Aezakmi.Animals;
using Aezakmi.Player;

namespace Aezakmi
{
    public class RescueZone : MonoBehaviour
    {
        [SerializeField] private MoneyThrower moneyThrower;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(GameTags.AnimalCollider)) return;

            other.transform.parent.tag = GameTags.RescuedAnimal;

            var ac = other.transform.parent.GetComponent<AnimalController>();
            ReferenceManager.Instance.player.GetComponent<CatchController>().AnimalRescued(ac);
            moneyThrower.ThrowMoney(ac.MoneyWorth);

            ac.MoveToShelter(ReferenceManager.Instance.shelterEndpoint);
        }
    }
}
