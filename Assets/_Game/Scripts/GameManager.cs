using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi
{
    public class GameManager : GloballyAccessibleBase<GameManager>
    {
        public int currentLevel;
        public int money;
        public float scaleOfNextLevel; // so maxing capacity will bring player from scale(1,1,1) to scale(scaleOfNextLevel * Vector3.one)
        
        private void Start()
        {
            // Load Game Data
        }
    }
}
