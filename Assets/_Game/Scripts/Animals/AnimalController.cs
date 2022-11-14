using UnityEngine;
using Aezakmi.LeashBehaviours;

namespace Aezakmi.Animals
{
    public enum AnimalStates
    {
        Waiting,
        Wandering,
        FollowingPlayer,
        MovingToShelter
    }

    public class AnimalController : MonoBehaviour
    {
        public LeashType leashType; // The type of leash that should be used on the animal.

        public Transform neckBand; // The end point of the leash.
        public Transform topOfHead; // For displaying heart effects above the animals head.
        public AnimalStates AnimalState;
        public int MoneyWorth;

        private Animator _animator;
        private WanderController _wanderController;
        private FollowPlayerController _followPlayerController;
        private MoveToShelterController _moveToShelterController;
        private static int s_percentageToWanderAsInitialState = 80;

        private bool _isPartOfWave = false;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _wanderController = GetComponent<WanderController>();
            _followPlayerController = GetComponent<FollowPlayerController>();
            _moveToShelterController = GetComponent<MoveToShelterController>();
            SetRandomState();
        }

        private void SetRandomState()
        {
            if (_isPartOfWave)
                return;

            AnimalState = Random.Range(0f, 100f) < s_percentageToWanderAsInitialState ? AnimalStates.Wandering : AnimalStates.Waiting;
            if (AnimalState == AnimalStates.Wandering)
                _wanderController.enabled = true;

            SetRandomRotation();
            SetAnimations();
        }

        public void GetCaught()
        {
            if (AnimalState == AnimalStates.Wandering)
            {
                _wanderController.StopWandering();
                _wanderController.enabled = false;
            }

            _followPlayerController.enabled = true;
            AnimalState = AnimalStates.FollowingPlayer;
            SetAnimations();
        }

        public void StopWandering()
        {
            AnimalState = AnimalStates.Waiting;
            SetAnimations();
        }

        private void SetAnimations()
        {
            _animator.SetBool("IsRunning", AnimalState != AnimalStates.Waiting);
        }

        public void MoveToShelter(Transform endPoint)
        {
            _followPlayerController.StopFollowing();
            _followPlayerController.enabled = false;
            _moveToShelterController.enabled = true;
            _moveToShelterController.SetDestination(endPoint.position);
        }

        private void SetRandomRotation()
        {
            var randomRotation = Random.Range(0f, 360f);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, randomRotation, transform.localEulerAngles.z);
        }

        public void SpawnAsWave(Vector3 pos)
        {
            _isPartOfWave = true;
            var waveBehaviour = gameObject.AddComponent<WaveBehaviour>();
            waveBehaviour.SetDestination(pos);
        }
    }
}
