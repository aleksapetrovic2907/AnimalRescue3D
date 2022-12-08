using UnityEngine;

namespace Aezakmi.CameraMechanics
{
    public class CameraController : GloballyAccessibleBase<CameraController>
    {
        [SerializeField] private CameraFollowPlayer cameraFollowPlayer;
        [SerializeField] private CameraWaveBehaviour cameraWaveBehaviour;
        [SerializeField] private int maxTimesWaveCameraCanBeForced; // How many times will the camera be forced to move to show new animals.

        public void StartWaveBehaviour()
        {
            if (GameDataManager.Instance != null)
                if (GameDataManager.Instance.gameData.timesWaveCameraIsForced > maxTimesWaveCameraCanBeForced) return;

            cameraFollowPlayer.enabled = false;
            cameraWaveBehaviour.StartBehaviour();
        }

        public void WaveBehaviourFinished()
        {
            cameraFollowPlayer.enabled = true;
        }
    }
}
