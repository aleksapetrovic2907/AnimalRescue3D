using System.Collections.Generic;
using UnityEngine;
using Aezakmi.Animals;
using Aezakmi.LeashBehaviours;

namespace Aezakmi.Drone
{
    public enum DroneState
    { Idle, MovingToRescue, MovingToStation }

    public class DroneController : GloballyAccessibleBase<DroneController>
    {
        public DroneState droneState;
        public int currentCapacity { get { return m_animalsAndLeashes.Count; } }
        public int maxCapacity;

        private List<Transform> m_leashOrigins;
        private Dictionary<AnimalController, LeashBase> m_animalsAndLeashes;
        private DroneMovement m_droneMovement;

        private void Update()
        {
            if (droneState == DroneState.MovingToStation && currentCapacity == 0)
            {
                // finished rescuing animals
                // return to station
            }
        }

        public void GetAnimals(Dictionary<AnimalController, LeashBase> animalsAndLeashes)
        {
            m_animalsAndLeashes = animalsAndLeashes;

            foreach (var animalAndLeash in m_animalsAndLeashes)
            {
                // give animals new transform to follow
                animalAndLeash.Key.GetComponent<FollowPlayerController>().targetToFollow = transform;

                // change leashes origin to drones' origin
                var randomOrigin = m_leashOrigins[Random.Range(0, m_leashOrigins.Count)];
                animalAndLeash.Value.origin = randomOrigin;
            }
        }

    }
}
