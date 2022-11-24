using UnityEngine;

namespace Aezakmi
{
    public class ReferenceManager : GloballyAccessibleBase<ReferenceManager>
    {
        public Transform animalsParent;
        public Transform leashesParent;
        public Transform moneyParent;
        public Transform waveCameraTransform; // the transform that camera looks at when wave starts
    }
}
