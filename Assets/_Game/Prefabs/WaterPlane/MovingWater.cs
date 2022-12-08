using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi
{
    public class MovingWater : MonoBehaviour
    {
        [SerializeField] private new Renderer renderer;
        [SerializeField] private Vector2 moveSpeed;

        private void Update()
        {
            renderer.material.SetTextureOffset("_BaseMap", Time.time * moveSpeed);
        }
    }
}
