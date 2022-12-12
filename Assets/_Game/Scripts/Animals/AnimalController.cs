using UnityEngine;
using Aezakmi.LeashBehaviours;
using NaughtyAttributes;

namespace Aezakmi.Animals
{
    public enum AnimalStates
    {
        Waiting,
        Wandering,
        FollowingPlayer,
        MovingToShelter
    }

    public enum AnimalSize
    { Small, Medium, Large }

    public class AnimalController : MonoBehaviour
    {
        public LeashType leashType; // The type of leash that should be used on the animal.

        public Transform neckBand; // The end point of the leash.
        public Transform topOfHead; // For displaying heart effects above the animals head.
        public AnimalStates AnimalState;
        public int MoneyWorth;
        public AnimalSize animalSize = AnimalSize.Small;

        private Animator m_animator;
        private WanderController m_wanderController;
        private FollowPlayerController m_followPlayerController;
        private MoveToShelterController m_moveToShelterController;
        private WaveBehaviour m_waveBehaviour;
        private static int s_percentageToWanderAsInitialState = 80;

        private bool m_isPartOfWave = false;

        private void Start()
        {
            m_animator = GetComponent<Animator>();
            m_wanderController = GetComponent<WanderController>();
            m_followPlayerController = GetComponent<FollowPlayerController>();
            m_moveToShelterController = GetComponent<MoveToShelterController>();
            SetRandomState();
        }

        private void SetRandomState()
        {
            if (m_isPartOfWave)
            {
                m_waveBehaviour = GetComponent<WaveBehaviour>();
                return;
            }

            AnimalState = Random.Range(0f, 100f) < s_percentageToWanderAsInitialState ? AnimalStates.Wandering : AnimalStates.Waiting;
            if (AnimalState == AnimalStates.Wandering)
                m_wanderController.enabled = true;

            SetRandomRotation();
            SetAnimations();
        }

        public void GetCaught()
        {
            if (m_isPartOfWave)
                m_waveBehaviour.StopWaveBehaviour();
            else if (AnimalState == AnimalStates.Wandering)
            {
                m_wanderController.StopWandering();
                m_wanderController.enabled = false;
            }

            m_followPlayerController.enabled = true;
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
            m_animator.SetBool("IsRunning", AnimalState != AnimalStates.Waiting);
        }

        public void MoveToShelter(Transform endPoint)
        {
            m_followPlayerController.StopFollowing();
            m_followPlayerController.enabled = false;
            m_moveToShelterController.enabled = true;
            m_moveToShelterController.SetDestination(endPoint.position);
        }

        private void SetRandomRotation()
        {
            var randomRotation = Random.Range(0f, 360f);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, randomRotation, transform.localEulerAngles.z);
        }

        public void SpawnAsWave(Vector3 pos)
        {
            m_isPartOfWave = true;
            var waveBehaviour = gameObject.AddComponent<WaveBehaviour>();
            waveBehaviour.SetDestination(pos);
        }

        [Button]
        private void SetTransforms()
        {
            neckBand = GameObject.Find("NeckbandAttachment").transform;
            topOfHead = GameObject.Find("TopOfHead").transform;
        }
    }
}
