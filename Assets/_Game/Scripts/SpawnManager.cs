using UnityEngine;
using NativeSerializableDictionary;
using NaughtyAttributes;
using Pathfinding;
using Aezakmi.Animals;

namespace Aezakmi
{
    public partial class SpawnManager : GloballyAccessibleBase<SpawnManager>
    {
        private enum SpawnerType { Box, Sphere }
        private const int MAX_FAILED_SEARCHES = 10;

        [Header("Initial Spawn Data")]
        [SerializeField] private SerializableDictionary<GameObject, int> initialAnimalsSmall;
        [SerializeField] private SerializableDictionary<GameObject, int> initialAnimalsMedium;
        [SerializeField] private SerializableDictionary<GameObject, int> initialAnimalsLarge;
        [SerializeField] private SpawnerType spawnerType;
        [ShowIf("spawnerType", SpawnerType.Sphere)] public float radius;
        [ShowIf("spawnerType", SpawnerType.Box)] public Vector3 size;
        [SerializeField] private Color gizmosColor = Color.blue;
        [SerializeField] private bool drawGizmos = false;

        [Header("Size Dependent Values")]
        [SerializeField] private int smallWorth;
        [SerializeField] private int medWorth;
        [SerializeField] private int largeWorth;

        private float m_spawnHeight = 0;

        private static float s_smallScale = 1;
        private static float s_mediumScale = 1.25f;
        private static float s_largeScale = 1.5f;

        private void Start() => SpawnInitialAnimals();

        public void SpawnInitialAnimals()
        {
            foreach (var animalData in initialAnimalsSmall)
                for (int i = 0; i < animalData.Value.Value; i++)
                {
                    var animal = Instantiate(animalData.Key, RandomPosition(), Quaternion.identity, ReferenceManager.Instance.animalsParent);
                    animal.transform.localScale = s_smallScale * Vector3.one;
                    var ac = animal.GetComponent<AnimalController>();
                    ac.animalSize = AnimalSize.Small;
                    ac.MoneyWorth = smallWorth;                    
                }

            foreach (var animalData in initialAnimalsMedium)
                for (int i = 0; i < animalData.Value.Value; i++)
                {
                    var animal = Instantiate(animalData.Key, RandomPosition(), Quaternion.identity, ReferenceManager.Instance.animalsParent);
                    animal.transform.localScale = s_mediumScale * Vector3.one;
                    var ac = animal.GetComponent<AnimalController>();
                    ac.animalSize = AnimalSize.Medium;
                    ac.MoneyWorth = medWorth;
                }

            foreach (var animalData in initialAnimalsLarge)
                for (int i = 0; i < animalData.Value.Value; i++)
                {
                    var animal = Instantiate(animalData.Key, RandomPosition(), Quaternion.identity, ReferenceManager.Instance.animalsParent);
                    animal.transform.localScale = s_largeScale * Vector3.one;
                    var ac = animal.GetComponent<AnimalController>();
                    ac.animalSize = AnimalSize.Large;
                    ac.MoneyWorth = largeWorth;
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