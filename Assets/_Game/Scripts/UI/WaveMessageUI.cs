using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aezakmi.Tweens;

namespace Aezakmi.UI
{
    public class WaveMessageUI : GloballyAccessibleBase<WaveMessageUI>
    {
        [SerializeField] private Scale messageScale;
        public void PlayMessage() => messageScale.PlayTween();
    }
}
