using UnityEngine;
using Lofelt.NiceVibrations;

namespace Aezakmi
{
    public enum VibrationType
    { Soft, Medium, Hard }

    public class VibrationsManager : MonoBehaviour
    {
        private void Vibrate(VibrationType type)
        {
            if (!GameDataManager.Instance.gameData.vibrationsActive) return;

            // Vibrate differently depending on platform
        }
    }
}
