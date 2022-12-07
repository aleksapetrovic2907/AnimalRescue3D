using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace Aezakmi.Player.Vehicles
{
    // Used when we have one single mesh with multiple materials
    public class VehicleMaterialColorer : VehicleColorControllerBase
    {
        [SerializeField] private new Renderer renderer;
        [SerializeField] private List<int> materialIndexes;

        [Button]
        public override void UpdateColor()
        {
            if (materialIndexes.Count <= 0) return;

            var color = colors[m_count];

            foreach (var index in materialIndexes)
                renderer.materials[index].SetColor("_BaseColor", color);

            m_count = (m_count + 1) % colors.Count;
        }
    }
}
