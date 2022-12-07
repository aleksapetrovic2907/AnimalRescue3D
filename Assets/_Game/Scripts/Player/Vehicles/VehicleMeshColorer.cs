using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi.Player.Vehicles
{
    public class VehicleMeshColorer : VehicleColorControllerBase
    {
        [SerializeField] private List<Renderer> meshesToColor;

        public override void UpdateColor()
        {
            if (meshesToColor.Count <= 0) return;

            var color = colors[m_count];

            foreach (var mesh in meshesToColor)
                mesh.material.SetColor("_BaseColor", color);

            m_count = (m_count + 1) % meshesToColor.Count;
        }
    }
}
