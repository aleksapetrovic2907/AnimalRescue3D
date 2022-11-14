using UnityEngine;
using NativeSerializableDictionary;
using NaughtyAttributes;
using Pathfinding;

namespace Aezakmi
{
    public partial class SpawnManager : MonoBehaviour
    {
        private enum SpawnerType { Box, Sphere }
        private const int MAX_FAILED_SEARCHES = 10;

        [Header("Initial Spawn Data")]
        [SerializeField] private SerializableDictionary<GameObject, int> initialAnimals;
        [SerializeField] private SpawnerType spawnerType;
        [ShowIf("spawnerType", SpawnerType.Sphere)] public float radius;
        [ShowIf("spawnerType", SpawnerType.Box)] public Vector3 size;
        [SerializeField] private Color gizmosColor = Color.blue;
        [SerializeField] private bool drawGizmos = false;

        private float m_spawnHeight = 0;

        private void Start() => SpawnInitialAnimals();

        public void SpawnInitialAnimals()
        {
            foreach (var animalData in initialAnimals)
            {
                for (int i = 0; i < animalData.Value.Value; i++)
                {
                    Instantiate(animalData.Key, RandomPosition(), Quaternion.identity, ReferenceManager.Instance.animalsParent);
                }
            }
        }

        private Vector3 RandomPosition()
        {
            Vector3 randPosition = Vector3.zero;
            Vector3 targetPosition = Vector3.zero;
            GraphNode nearestNode;

            do
            {
                if (spawnerType == SpawnerType.Sphere)
                    randPosition = Random.insideUnitSphere * radius;
                else if (spawnerType == SpawnerType.Box)
                {
                    var x = Random.Range(-size.x, size.x) / 2f;
                    var z = Random.Range(-size.z, size.z) / 2f;

                    randPosition = new Vector3(x, 0, z);
                }

                randPosition += transform.position;
                nearestNode = AstarPath.active.GetNearest(randPosition).node;
            } while (!nearestNode.Walkable);
            
            targetPosition = (Vector3)nearestNode.position;
            return targetPosition;
        }


#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!drawGizmos) return;

            Gizmos.color = gizmosColor;

            if (spawnerType == SpawnerType.Box)
                Gizmos.DrawWireCube(transform.position, size);
            else if (spawnerType == SpawnerType.Sphere)
                Gizmos.DrawWireSphere(transform.position, radius);
        }
#endif
    }
}