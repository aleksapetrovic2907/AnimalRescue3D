using UnityEngine;
using NaughtyAttributes;

namespace Aezakmi
{
    [RequireComponent(typeof(Renderer))]
    public class PivotCenterer : MonoBehaviour
    {
        private enum Pivot
        { Top = -1, Bottom = 1, Middle = 0 }

        [SerializeField] private Pivot meshPivot = Pivot.Bottom;

        [Button]
        private void SetPivot()
        {
            var renderer = GetComponent<Renderer>();

            transform.localPosition = new Vector3
            (
                0f,
                (renderer.bounds.size.y * (int)meshPivot) / 2f,
                0f
            );
        }
    }
}
