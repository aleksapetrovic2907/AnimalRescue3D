using System.Collections.Generic;
using UnityEngine;
using Aezakmi.UpgradeMechanics;
using NaughtyAttributes;
using Aezakmi.Player.Vehicles;

namespace Aezakmi.Player
{
    public enum VehicleType
    {
        Feet,
        Skate,
        Scooter,
        Bicycle,
        OneWheeler,
        Hoverboard,
        Segway,
        Vespa,
        FlyingSkate,
        FlyingMotorcycle,
    }

    public class PlayerVehicleManager : GloballyAccessibleBase<PlayerVehicleManager>
    {
        public Vehicle currentVehicle = new Vehicle();
        public List<Vehicle> vehicles;

        [SerializeField] private List<Transform> skins;

        private void Start() => UpdateVehicle();

        [Button]
        public void UpdateVehicle()
        {
            var speedLevel = UpgradesManager.Instance.upgrades[2].level;

            for (int i = 0; i < vehicles.Count; i++)
            {
                if (!BetweenTwoNumbers(speedLevel, vehicles[i].levelRange)) continue;

                currentVehicle.vehicleObject.SetActive(false);

                currentVehicle = vehicles[i];
                currentVehicle.vehicleObject.SetActive(true);

                foreach (var skin in skins)
                    skin.localPosition = currentVehicle.meshPosition;

                PlayerAnimatorController.Instance.UpdateAnimations(currentVehicle);
            }

            currentVehicle.vehicleObject.GetComponent<VehicleColorControllerBase>().UpdateColor();
        }

        public static bool BetweenTwoNumbers(int num, Vector2 range)
        {
            return num >= range.x && num <= range.y;
        }
    }

    [System.Serializable]
    public class Vehicle
    {
        public VehicleType vehicleType;
        public GameObject vehicleObject;
        public Vector2 levelRange; // Indicates which levels of speed will use this vehicle.
        public Vector3 meshPosition; // e.g: mesh position will have y > 0 when standing on a segway.

        public AnimatorOverrideController animatorOverrideController;
        public float idleSpeed = 1f;
        public float moveSpeed = 1f;
    }
}
