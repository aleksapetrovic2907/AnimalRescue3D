using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Aezakmi.Player
{
    public class MusclesController : GloballyAccessibleBase<MusclesController>
    {
        [SerializeField] private List<SkinnedMeshRenderer> _skinnedMeshRenderers;
        [SerializeField] private float WeightIncreasePerLevel;

        [Header("Tween Settings")]
        [SerializeField] private float ScaleIncreasePerLevel;
        [SerializeField] private float ScaleDuration;
        [SerializeField] private Ease ScaleEase;

        private const float MAX_WEIGHT = 180f;

        private Tweener _tweener = null;

        public void IncreaseMuscles(int levels)
        {
            _tweener = transform.DOScale(transform.localScale + levels * ScaleIncreasePerLevel * Vector3.one, ScaleDuration).SetEase(ScaleEase).Play();

            foreach (var _skinnedMeshRenderer in _skinnedMeshRenderers)
            {
                var currentWeight = _skinnedMeshRenderer.GetBlendShapeWeight(0);
                var targetWeight = currentWeight + WeightIncreasePerLevel * levels;

                if (currentWeight + WeightIncreasePerLevel * levels >= MAX_WEIGHT)
                    targetWeight = MAX_WEIGHT;

                _skinnedMeshRenderer.SetBlendShapeWeight(0, targetWeight);
            }
        }
    }
}
