using UnityEngine;
using DG.Tweening;

namespace Aezakmi
{
    public class MoneyBehaviour : MonoBehaviour
    {
        public int value;

        [SerializeField] private new Collider collider;
        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private AudioClip audioClip;
        [SerializeField] private AudioSource audioSource;

        private static float s_sequenceDuration = .35f;
        private static Ease s_moveEase = Ease.InSine;
        private static Ease s_scaleEase = Ease.OutSine;

        private static float s_destroyDuration = .25f;

        public void SetValues(int value, Vector3 position)
        {
            this.value = value;

            Sequence sequence = DOTween.Sequence();

            Tween moveToPlatform = transform.DOMove(position, s_sequenceDuration).SetEase(s_moveEase);
            Tween scale = transform.DOScale(Vector3.one, s_sequenceDuration).SetEase(s_scaleEase);

            sequence.Append(moveToPlatform).Join(scale).OnComplete(delegate
            {
                rigidbody.isKinematic = false;
            }).Play();
        }

        public void DestroySelf()
        {
            transform.parent = null;
            collider.enabled = false;
            rigidbody.isKinematic = true;
            transform.DOScale(Vector3.zero, s_destroyDuration).SetEase(Ease.OutSine).OnComplete(delegate { Destroy(gameObject); }).Play();
            FeedbackManager.Instance.CapturedMoney();
        }
    }
}
