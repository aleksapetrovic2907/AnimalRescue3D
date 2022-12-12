using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Aezakmi.Animals;
using Aezakmi.LeashBehaviours;

namespace Aezakmi.Drone
{
    public enum DroneState
    { Idle, MovingToRescue, MovingToStation }

    public class DroneController : GloballyAccessibleBase<DroneController>
    {
        public DroneState droneState;
        public int currentCapacity = 0;
        public int maxCapacity;
        public float cooldownDuration = 10f;
        [HideInInspector] public List<Transform> leashOrigins;

        [Header("Tween Settings")]
        [SerializeField] private GameObject droneMesh;
        [SerializeField] private float moveAmount;
        [SerializeField] private float moveDuration;
        [SerializeField] private Ease moveEase;

        private Dictionary<AnimalController, LeashBase> m_animalsAndLeashes;
        private DroneMovement m_droneMovement;
        private DroneSkinController m_droneSkinController;
        private Tween m_upAndDown = null;

        private void Start()
        {
            m_droneMovement = GetComponent<DroneMovement>();
            m_droneSkinController = GetComponent<DroneSkinController>();
        }

        private void Update()
        {
            if (m_droneMovement.target == null)
            {
                if (droneState == DroneState.MovingToRescue && currentCapacity == 0)
                {
                    m_upAndDown.Kill();
                    m_upAndDown = null;
                    droneState = DroneState.MovingToStation;
                    m_droneMovement.MoveToStation();
                }

                if (droneState == DroneState.MovingToStation)
                {
                    m_upAndDown.Kill();
                    m_upAndDown = null;
                    droneState = DroneState.Idle;
                    m_droneMovement.ResetRotation();
                }

                if (droneState == DroneState.Idle && m_upAndDown == null) MoveDroneUpAndDown();
            }
        }

        public void GetAnimals(Dictionary<AnimalController, LeashBase> animalsAndLeashes)
        {
            m_animalsAndLeashes = animalsAndLeashes;
            Debug.LogWarning(m_animalsAndLeashes.Count);
            foreach (var animalAndLeash in m_animalsAndLeashes)
            {
                var fpc = animalAndLeash.Key.GetComponent<FollowPlayerController>();
                fpc.targetToFollow = transform;
                fpc.followingPlayer = false;
                currentCapacity++;
            }

            UpdateLeashesOrigin();
            droneState = DroneState.MovingToRescue;
            m_droneMovement.MoveToShelter();
        }

        public void AnimalRescued(AnimalController ac)
        {
            foreach (var animal in m_animalsAndLeashes)
            {
                if (animal.Key == ac)
                    Destroy(animal.Value.gameObject);
            }

            currentCapacity--;
        }

        private void UpdateLeashesOrigin()
        {
            foreach (var animalAndLeash in m_animalsAndLeashes)
            {
                var randomOrigin = leashOrigins[Random.Range(0, leashOrigins.Count)];
                animalAndLeash.Value.origin = randomOrigin;
            }
        }

        private void MoveDroneUpAndDown()
        {
            m_upAndDown = droneMesh.transform.DOLocalMoveY(moveAmount, moveDuration).SetLoops(-1, LoopType.Yoyo).SetEase(moveEase).SetRelative(true).Play();
        }
    }
}
