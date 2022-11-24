using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Aezakmi
{
    public class CameraWaveBehaviour : MonoBehaviour
    {
        // We use a list in case we want to incorporate different positions/rotations for each waves.
        [SerializeField] private List<Vector3> movePositions;
        [SerializeField] private List<Vector3> moveRotations;

        [SerializeField] private float moveDuration;
        [SerializeField] private float waitDuration;

        [SerializeField] private Ease moveEase;
        [SerializeField] private Ease rotateEase;

        private Vector3 m_previousPosition;
        private Vector3 m_previousRotation;
        private int m_count = 0;

        private Sequence m_waveSequence;

        private void OnEnable()
        {
            m_previousPosition = transform.localPosition;
            m_previousRotation = transform.localEulerAngles;

            int index = m_count % movePositions.Count;

            Tweener moveToPosition = transform.DOLocalMove(movePositions[index], moveDuration).SetEase(moveEase);
            Tweener rotateTowardsWave = transform.DOLocalRotate(moveRotations[index], moveDuration).SetEase(rotateEase);
            Tweener moveBack = transform.DOLocalMove(m_previousPosition, moveDuration).SetEase(moveEase);
            Tweener rotateBack = transform.DOLocalRotate(m_previousRotation, moveDuration).SetEase(rotateEase);

            m_waveSequence = DOTween.Sequence();
            m_waveSequence.Append(moveToPosition).Join(rotateTowardsWave).AppendInterval(waitDuration).Append(moveBack).Join(rotateBack);

            m_count++;
        }

        public void StopBehaviour()
        {
            m_waveSequence.Kill();
            transform.position = m_previousPosition;
            transform.localEulerAngles = m_previousRotation;
        }
    }
}
