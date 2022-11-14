using UnityEngine;

namespace Aezakmi
{
    public class RandomManager : MonoBehaviour
    {
        public int previousSeed { get; private set; }
        public int seed;

        private void Awake()
        {
#pragma warning disable 618
            var previousSeed = Random.seed;
            Random.InitState(seed);
        }
    }
}
