using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi
{
    public class LeashColors : GloballyAccessibleBase<LeashColors>
    {
        public List<Color> leashColors;

        private int m_count = 0;

        public Color GetLeashColor()
        {
            var color = leashColors[m_count];
            m_count = (m_count + 1) % leashColors.Count;

            return color;
        }

    }
}
