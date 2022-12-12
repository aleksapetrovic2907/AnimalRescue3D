using Aezakmi.Player;
using UnityEngine;

namespace Aezakmi
{
    public class LevelEndpointController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(GameTags.Player)) return;
            PlayerController.Instance.TransitionToNextLevel();
        }
    }
}
