using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi
{
    public class GameManager : GloballyAccessibleBase<GameManager>
    {
        public int currentLevel;
        public int money;

        [SerializeField] private List<Transform> m_transformsThatScaleWithLevel;

        /// <summary>Using a formula returns a current level size where everything scales with it.</summary>
        public float GetLevelSize() => Mathf.Pow(2, currentLevel);

        private void Start()
        {
            // Load Game Data
        }
    }
}
