using UnityEngine;
using DG.Tweening;

namespace Aezakmi
{
    public class MoneyBehaviour : MonoBehaviour
    {
        public int value;

        [SerializeField] private Rigidbody Rigidbody;

        private static float s_sequenceDuration = .15f;
        private static Ease s_moveEase = Ease.InSine;
        private static Ease s_scaleEase = Ease.OutSine;

        public void SetValues(int value, Vector3 position)
        {
            this.value = value;

            Sequence sequence = DOTween.Sequence();

            Tween moveToPlatform = transform.DOMove(position, s_sequenceDuration).SetEase(s_moveEase);
            Tween scale = transform.DOScale(Vector3.one, s_sequenceDuration).SetEase(s_scaleEase);

            sequence.Append(moveToPlatform).Join(scale).OnComplete(delegate
            {
                Rigidbody.isKinematic = false;
            }).Play();
        }
    }
}
