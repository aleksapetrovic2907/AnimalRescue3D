using System.Collections.Generic;
using UnityEngine;
using NativeSerializableDictionary;
using Aezakmi.UpgradeMechanics;
using Aezakmi.LeashBehaviours;
using Aezakmi.Animals;

namespace Aezakmi.Player
{
    public class CatchController : MonoBehaviour
    {
        [Header("Catch Settings")]
        [SerializeField] private LayerMask raycastLayers;
        public float raycastRadius;
        [SerializeField] private bool drawGizmos = false;

        [Header("Leash Settings")]
        [SerializeField] private Transform leftHand;
        [SerializeField] private Transform rightHand;
        [SerializeField] private List<GameObject> leashPrefabs;

        [Header("Capacity Settings")]
        public List<int> currentCapacity = new List<int>(3);
        public List<int> maxCapacity = new List<int>(3);
        public SerializableDictionary<int, SerializableDictionary<AnimalSize, int>> maxCapacityPerCapacityLevel;

        private int m_capacityLevel = 0;

        private Collider[] m_hitAnimals;
        private PlayerController m_playerController;
        private Dictionary<AnimalController, LeashBase> m_animalAndLeash = new Dictionary<AnimalController, LeashBase> { };
        public float raycastRadiusModifier = 7;

        private void Start()
        {
            for (int i = 0; i < maxCapacity.Count; i++)
            {
                maxCapacity[i] = maxCapacityPerCapacityLevel[m_capacityLevel].Value.GetValue((AnimalSize)i);
            }

            m_playerController = GetComponent<PlayerController>();
        }

        private void Update()
        {
            CheckForAnimals();
        }

        private void CheckForAnimals()
        {
            m_hitAnimals = Physics.OverlapSphere(transform.position, raycastRadius * raycastRadiusModifier, raycastLayers);

            if (m_hitAnimals.Length <= 0)
                return;

            foreach (var animal in m_hitAnimals)
            {
                if (animal.transform.parent.CompareTag(GameTags.FreeAnimal))
                {
                    var ac = animal.transform.parent.GetComponent<AnimalController>();
                    if (!CanCatch(ac)) continue;

                    currentCapacity[(int)ac.animalSize]++;
                    m_playerController.ToggleFullIndicator();
                    PutAnimalOnLeash(animal.transform.parent);
                    EventManager.TriggerEvent(GameEvents.AnimalCaptured, new Dictionary<string, object> { { "animal", animal.transform.parent } });
                }
            }
        }

        private bool CanCatch(AnimalController ac)
        {
            var index = (int)ac.animalSize;
            return currentCapacity[index] != maxCapacity[index];
        }

        private void PutAnimalOnLeash(Transform animal)
        {
            animal.tag = GameTags.CaughtAnimal;

            AnimalController animalController = animal.GetComponent<AnimalController>();
            LeashBase leash = Instantiate(GetSuitableLeash(animalController), rightHand.position, Quaternion.identity, ReferenceManager.Instance.leashesParent).GetComponent<LeashBase>();

            var targetHand = IsRightHandCloser(animal) ? rightHand : leftHand;
            leash.origin = targetHand;
            leash.end = animalController.neckBand;

            animalController.GetCaught();
            m_animalAndLeash.Add(animalController, leash);
        }

        private bool IsRightHandCloser(Transform animal)
        {
            var animalDirection = (animal.position - transform.position).normalized;
            var crossProduct = Vector3.Cross(animalDirection, transform.forward);
            float dot = Vector3.Dot(crossProduct, Vector3.up);
            return dot < 0f;
        }

        private GameObject GetSuitableLeash(AnimalController animalController)
        {
            foreach (var leash in leashPrefabs)
            {
                if (leash.GetComponent<LeashBase>().leashType == animalController.leashType)
                    return leash;
            }
            return leashPrefabs[0];
        }

        public void AnimalRescued(AnimalController animalController)
        {
            currentCapacity[(int)animalController.animalSize]--;

            foreach (var animal in m_animalAndLeash)
            {
                if (animal.Key == animalController)
                    Destroy(animal.Value.gameObject);
            }

            m_playerController.ToggleFullIndicator();
        }

        public void SetNewHands(MeshHandsController meshHandsController)
        {
            leftHand = meshHandsController.leftHand;
            rightHand = meshHandsController.rightHand;

            foreach (var m_animalAndLeash in m_animalAndLeash)
            {
                if (m_animalAndLeash.Key != null)
                    m_animalAndLeash.Value.origin = IsRightHandCloser(m_animalAndLeash.Key.transform) ? rightHand : leftHand;
            }
        }

        public void UpdateCapacity()
        {
            m_capacityLevel = UpgradesManager.Instance.upgrades[0].relativeLevel;

            for (int i = 0; i < maxCapacity.Count; i++)
            {
                maxCapacity[i] = maxCapacityPerCapacityLevel[m_capacityLevel].Value.GetValue((AnimalSize)i);
            }
        }

        // todo: replace magic numbers
        public bool IsFull()
        {
            int animalSizeToCheck;
            if (m_capacityLevel < 4) animalSizeToCheck = 0;
            else if (m_capacityLevel < 7) animalSizeToCheck = 1;
            else animalSizeToCheck = 2;

            return currentCapacity[animalSizeToCheck] == maxCapacity[animalSizeToCheck];
        }

        #region UNITYEDITOR
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!drawGizmos)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, raycastRadius * raycastRadiusModifier);
        }
#endif
        #endregion
    }
}
