using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi
{
    public class AnimalDestroyer : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag(GameTags.AnimalCollider))
                Destroy(other.transform.parent.gameObject);
        }
    }
}
