using UnityEngine;

namespace Aezakmi
{
    public class ArrowIndicator : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private new Renderer renderer;
        [SerializeField] private Color arrowsColor = Color.white;

        private void Start()
        {
            renderer.material.SetColor("_Color", arrowsColor);
        }

        private void LateUpdate()
        {
            // Set size
            var distance = Vector3.Distance(transform.position, target.position) / ReferenceManager.Instance.player.lossyScale.x;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, distance);
            renderer.material.SetFloat("_Length", distance);

            // Rotate
            Vector3 direction = (target.position - transform.position);
            transform.rotation = Quaternion.LookRotation(direction);
            transform.localEulerAngles = new Vector3
            (
                0f,
                transform.localEulerAngles.y,
                0f
            );
        }
    }
}
