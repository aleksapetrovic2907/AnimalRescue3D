using UnityEngine;

namespace Aezakmi.LeashBehaviours
{
    public class LeashElastic : LeashBase
    {
        private const float ROTATION_OFFSET = 90f;

        private Renderer m_renderer;

        private void Start()
        {
            m_renderer = GetComponent<Renderer>();
            m_renderer.material.SetColor("_BaseColor", LeashColors.Instance.GetLeashColor());
            m_renderer.enabled = false;
        }

        protected override void Update()
        {
            base.Update();

            ScaleLeashToNeckband();
            RotateTowardsNeckband();
        }

        private void ScaleLeashToNeckband()
        {
            var targetScale = (end.position - transform.position).magnitude / 2f;
            Vector3 newScale = new Vector3
            (
                transform.localScale.x,
                targetScale,
                transform.localScale.z
            );
            transform.localScale = newScale;
        }

        private void RotateTowardsNeckband()
        {
            transform.localEulerAngles = Vector3.zero;
            transform.LookAt(end, Vector3.up);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x + ROTATION_OFFSET, transform.localEulerAngles.y, transform.localEulerAngles.z);

            if (!m_renderer.enabled)
                m_renderer.enabled = true;
        }
    }
}
