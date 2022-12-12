using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Aezakmi.CameraMechanics
{
    public class CameraWaveBehaviour : MonoBehaviour
    {
        // We use a list in case we want to incorporate different positions/rotations for each waves.
        [SerializeField] private List<Transform> waveTransforms;

        [SerializeField] private float moveDuration;
        [SerializeField] private float waitDuration;

        [SerializeField] private Ease moveEase;
        [SerializeField] private Ease rotateEase;

        private Vector3 m_previousPosition;
        private Vector3 m_previousRotation;
        private int m_count = 0;

        private Sequence m_waveSequence;

        private void Update()
        {
            // if can skip && touched screen && m_sequence.IsPlaying()
            //  BehaviourFinished()
        }

        public void StartBehaviour()
        {
            if (m_waveSequence.IsActive()) return;

            m_previousPosition = transform.localPosition;
            m_previousRotation = transform.localEulerAngles;

            Tweener moveToPosition = transform.DOLocalMove(waveTransforms[m_count].position, moveDuration).SetEase(moveEase);
            Tweener rotateTowardsWave = transform.DOLocalRotate(waveTransforms[m_count].localEulerAngles, moveDuration).SetEase(rotateEase);
            Tweener moveBack = transform.DOLocalMove(m_previousPosition, moveDuration).SetEase(moveEase);
            Tweener rotateBack = transform.DOLocalRotate(m_previousRotation, moveDuration).SetEase(rotateEase);

            m_waveSequence = DOTween.Sequence();
            m_waveSequence.Append(moveToPosition).Join(rotateTowardsWave).AppendInterval(waitDuration).Append(moveBack).Join(rotateBack)
                .OnComplete(BehaviourFinished)
                .Play();

            m_count = (m_count + 1) % waveTransforms.Count;
        }

        public void BehaviourFinished()
        {
            m_waveSequence.Kill();
            transform.position = m_previousPosition;
            transform.localEulerAngles = m_previousRotation;

            CameraController.Instance.WaveBehaviourFinished();
        }
    }
}
