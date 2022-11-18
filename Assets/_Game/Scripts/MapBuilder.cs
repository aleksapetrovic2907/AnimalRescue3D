using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using NativeSerializableDictionary;
using System.Linq;


namespace Aezakmi
{
    public class MapBuilder : MonoBehaviour
    {
        private enum BuilderType
        { Box, Sphere }

        public Color gizmosColor = Color.red;
        public bool drawGizmos = false;
        [SerializeField] private BuilderType builderType;

        [Tooltip("Deletes all previously generated objects when generating new.")]
        public bool deleteOld;

        [ShowIf("builderType", BuilderType.Sphere)] public float radius;
        [ShowIf("builderType", BuilderType.Box)] public Vector3 size;

        [SerializeField] private SerializableDictionary<string, List<SpawnObject>> ObjectsToSpawn;
        private List<GameObject> m_previouslySpawnedObjects = new List<GameObject>();

        private float m_spawnHeight = 0;

        [Button]
        private void GenerateNewMap()
        {
            if (deleteOld)
                DeleteOldBuilds();

            foreach (var item in ObjectsToSpawn)
            {
                var parent = new GameObject();
                parent.name = item.Key;
                parent.transform.parent = transform;
                m_previouslySpawnedObjects.Add(parent);

                foreach (var spawnObject in item.Value.Value)
                {
                    for (int i = 0; i < spawnObject.amount; i++)
                    {
                        var rotation = spawnObject.rotateRandomly ? RandomRotation() : Vector3.zero;
                        var newPrefab = Instantiate(spawnObject.prefab, transform.position + RandomPosition(), Quaternion.identity, parent.transform);
                        newPrefab.transform.localEulerAngles = rotation;
                    }

                }
            }
        }

        private Vector3 RandomPosition()
        {
            var randPosition = Vector3.zero;

            if (builderType == BuilderType.Sphere)
                randPosition = Random.insideUnitSphere * radius;
            else if (builderType == BuilderType.Box)
            {
                var x = Random.Range(-size.x, size.x) / 2f;
                var z = Random.Range(-size.z, size.z) / 2f;

                randPosition = new Vector3(x, 0, z);
            }

            randPosition.y = m_spawnHeight;
            return randPosition;
        }

        private Vector3 RandomRotation()
        {
            return new Vector3(0f, Random.Range(0f, 360f), 0f);
        }

        private void DeleteOldBuilds()
        {
            if (m_previouslySpawnedObjects.Count == 0) return;

            foreach (var obj in m_previouslySpawnedObjects)
                DestroyImmediate(obj.gameObject);
        }


#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!drawGizmos) return;

            Gizmos.color = gizmosColor;

            if (builderType == BuilderType.Box)
                Gizmos.DrawWireCube(transform.position, size);
            else if (builderType == BuilderType.Sphere)
                Gizmos.DrawWireSphere(transform.position, radius);
        }
#endif
    }

    [System.Serializable]
    public class SpawnObject
    {
        public GameObject prefab;
        public int amount;
        public bool rotateRandomly = true;
    }
}
