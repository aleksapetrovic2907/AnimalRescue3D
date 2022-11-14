using UnityEngine;

namespace Aezakmi.LeashBehaviours
{
    public enum LeashType
    { Elastic, Chain }

    public abstract class LeashBase : MonoBehaviour
    {
        public LeashType leashType;
        public Transform origin;
        public Transform end;

        private void Start()
        {
            float levelSize = GameManager.Instance.GetLevelSize();
            transform.localScale = new Vector3(levelSize, 1, levelSize);
        }

        protected virtual void Update()
        {
            if (origin == null || end == null)
                return;

            transform.position = origin.position;
        }
    }
}
