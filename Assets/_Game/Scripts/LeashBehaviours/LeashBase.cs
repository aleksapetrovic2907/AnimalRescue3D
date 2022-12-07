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

        protected virtual void Update()
        {
            if (origin == null || end == null)
                return;

            transform.position = origin.position;
        }
    }
}
