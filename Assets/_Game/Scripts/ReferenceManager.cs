using UnityEngine;

namespace Aezakmi
{
    public class ReferenceManager : GloballyAccessibleBase<ReferenceManager>
    {
        public Transform player;
        public Transform animalsParent;
        public Transform leashesParent;
        public Transform moneyParent;
        public Transform shelterEndpoint;
        public Transform levelEndpoint;
        public Transform newLevelStartpoint;
    }
}
