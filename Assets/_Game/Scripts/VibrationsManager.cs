using UnityEngine;
using Lofelt.NiceVibrations;

namespace Aezakmi
{
    public enum VibrationType
    { Soft, Medium, Hard }

    public class VibrationsManager : SingletonBase<VibrationsManager>
    {
        public void Vibrate(VibrationType type)
        {
            if (!GameDataManager.Instance.gameData.vibrationsActive) return;

            if (type == VibrationType.Soft)
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.SoftImpact);
            else if (type == VibrationType.Medium)
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.MediumImpact);
            else if (type == VibrationType.Hard)
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.HeavyImpact);
        }
    }
}
