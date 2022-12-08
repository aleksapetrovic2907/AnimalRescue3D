using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace Aezakmi
{
    public class MoneyThrower : MonoBehaviour
    {
        public bool HasMoneyToThrow { get { return m_moneyToThrow.Count > 0; } }

        [SerializeField] private GameObject moneyPrefab;
        [SerializeField] private Vector3 moneyPrefabSize;
        [SerializeField] private int stacksPerAnimal;

        [Space(10)]
        [SerializeField] private float delayBetweenThrows;
        [SerializeField] private Transform spawnPosition;
        [SerializeField] private Transform firstLandPosition;
        [SerializeField] private Vector2Int platformDimensions; // how many stacks fit in row, how many in column

        private float m_timeSinceLastThrow = 0f;
        private int m_moneyOnPlatform = 0;
        private Queue<int> m_moneyToThrow = new Queue<int>();

        private const float OFFSET_X = .15f;
        private const float OFFSET_Z = .15f;
        private const float OFFSET_Y = 0.3f;

        private void Update()
        {
            if (!HasMoneyToThrow) return;

            m_timeSinceLastThrow += Time.deltaTime;

            if (m_timeSinceLastThrow >= delayBetweenThrows)
            {
                m_timeSinceLastThrow = 0f;

                var stack = Instantiate(moneyPrefab, spawnPosition.position, Quaternion.identity, ReferenceManager.Instance.moneyParent).GetComponent<MoneyBehaviour>();
                stack.SetValues(m_moneyToThrow.Dequeue(), GetTargetPosition());

                m_moneyOnPlatform++;
            }
        }

        [Button]
        public void ThrowMoney(int amount = 0)
        {
            if (ReferenceManager.Instance.moneyParent.childCount == 0)
                m_moneyOnPlatform = 0;

            for (int i = 0; i < stacksPerAnimal; i++)
                m_moneyToThrow.Enqueue(amount / stacksPerAnimal);
        }

        private Vector3 GetTargetPosition()
        {
            var posZ = (m_moneyOnPlatform / platformDimensions.x) % platformDimensions.y;
            var posX = m_moneyOnPlatform % platformDimensions.x;
            var posY = m_moneyOnPlatform / (platformDimensions.x * platformDimensions.y);


            var offsetX = posX == 0 ? 0f : OFFSET_X;
            var offsetZ = posZ == 0 ? 0f : OFFSET_Z;

            var targetPosition = firstLandPosition.position + new Vector3
            (
                posX * (moneyPrefabSize.x + offsetX),
                posY * moneyPrefabSize.y + OFFSET_Y,
                -posZ * (moneyPrefabSize.z + offsetZ)
            );

            return targetPosition;
        }
    }
}
