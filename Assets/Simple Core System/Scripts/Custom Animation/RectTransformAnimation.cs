using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrunoMikoski.AnimationSequencer;
using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine.UI;

namespace Custom.Animation
{
    public class RectTransformAnimation : DOTweenActionBase
    {
        public override Type TargetComponentType => typeof(RectTransform);

        public override string DisplayName => "Rect Transform Animation";

        private RectTransform rectTransform;
        private Vector2 previousAnchorPosition;

        protected override Tweener GenerateTween_Internal(GameObject target, float duration)
        {
            return null;
        }

        public override void ResetToInitialState()
        {

        }
    }
}