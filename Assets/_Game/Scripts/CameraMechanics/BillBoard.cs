using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi
{
    public class BillBoard : MonoBehaviour
    {
        private void LateUpdate()
        {
            transform.LookAt(Camera.main.transform, Vector3.up);
        }
    }
}
