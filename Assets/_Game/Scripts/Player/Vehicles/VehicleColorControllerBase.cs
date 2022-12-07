using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi.Player.Vehicles
{
    public abstract class VehicleColorControllerBase : MonoBehaviour
    {
        [SerializeField] protected List<Color> colors;
        protected int m_count = 0;

        public abstract void UpdateColor();
    }
}
