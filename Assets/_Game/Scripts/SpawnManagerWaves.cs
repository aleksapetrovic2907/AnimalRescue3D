using UnityEngine;
using System.Collections.Generic;
using NativeSerializableDictionary;
using NaughtyAttributes;
using Aezakmi.Animals;

namespace Aezakmi
{
    public partial class SpawnManager
    {
        [Header("Wave Spawn Data")]
        [SerializeField] private List<SerializableDictionary<GameObject, int>> animalCountPerWave;
        [SerializeField] private List<Transform> waveSpawnpoints;
        [SerializeField] private List<Transform> waveEndpoints;
        public List<int> rescuesNeededPerWave;
        public Transform cameraWaveTransform;
        public int totalWaves { get { return animalCountPerWave.Count; } private set { } }
        public int currentWave { get; private set; } = 0;

        [Button]
        public void SendWave()
        {
            if (currentWave == totalWaves) return;

            foreach (var animals in animalCountPerWave[currentWave])
            {
                for (int i = 0; i < animals.Value.Value; i++)
                {
                    var randomSpawn = waveSpawnpoints[Random.Range(0, waveSpawnpoints.Count)].position;
                    var randomEnd = waveEndpoints[Random.Range(0, waveEndpoints.Count)].position;

                    var animal = Instantiate(animals.Key, randomSpawn, Quaternion.identity, ReferenceManager.Instance.animalsParent).GetComponent<AnimalController>();
                    animal.SpawnAsWave(randomEnd);
                }
            }
        }
    }
}
