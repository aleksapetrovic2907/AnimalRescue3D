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
        public int totalWaves { get { return animalCountPerWave.Count; } private set { } }
        public int currentWave = 0;

        public int NUM_OF_SMALL
        {
            get
            {
                int sum = 0;
                foreach (var animalData in initialAnimalsSmall)
                    sum += animalData.Value.Value;

                return sum;
            }
            private set { }
        }
        public int NUM_OF_MEDIUM
        {
            get
            {
                int sum = 0;
                foreach (var animals in initialAnimalsMedium)
                    sum += animals.Value.Value;

                foreach (var animals in animalCountPerWave[0])
                    sum += animals.Value.Value;

                return sum;
            }
            private set { }
        }
        public int NUM_OF_LARGE
        {
            get
            {
                int sum = 0;
                foreach (var animals in initialAnimalsLarge)
                    sum += animals.Value.Value;

                foreach (var animals in animalCountPerWave[1])
                    sum += animals.Value.Value;

                return sum;
            }
            private set { }
        }

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
                    if(currentWave == 0)
                    {
                        animal.animalSize = AnimalSize.Medium;
                        animal.transform.localScale = s_mediumScale * Vector3.one;
                        animal.MoneyWorth = medWorth;
                    }
                    else if(currentWave == 1)
                    {
                        animal.animalSize = AnimalSize.Large;
                        animal.transform.localScale = s_largeScale * Vector3.one;
                        animal.MoneyWorth = largeWorth;
                    }
                }
            }

            currentWave++;
        }
    }
}
