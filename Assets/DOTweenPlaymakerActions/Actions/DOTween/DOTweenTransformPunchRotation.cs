// Copyright (c) 2015 - 2019 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using DG.Tweening;
using Doozy.PlayMaker;
using Doozy.PlayMaker.Actions;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("DOTween")]
    [Tooltip("Punches a Transform's localRotation towards the given size and then back to the starting one as if it was connected to the starting rotation via an elastic. ")]
    [HelpUrl("http://dotween.demigiant.com/documentation.php")]
    public class DOTweenTransformPunchRotation : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(Transform))]
        public FsmOwnerDefault gameObject;

        [RequiredField]
        [UIHint(UIHint.FsmVector3)]
        [Tooltip("The direction and strength of the punch (added to the Transform's current rotation)")]
        public FsmVector3 punch;

        [UIHint(UIHint.FsmInt)]
        [Tooltip("Indicates how much will the punch vibrate")]
        public FsmInt vibrato;

        [UIHint(UIHint.FsmFloat)]
        [Tooltip("Represents how much (0 to 1) the vector will go beyond the starting position when bouncing backwards. 1 creates a full oscillation between the punch direction and the opposite direction, while 0 oscillates only between the punch and the start position")]
        public FsmFloat elasticity;

        [UIHint(UIHint.FsmBool)]
        [Tooltip("If TRUE the tween will smoothly snap all values to integers")]
        public FsmBool snapping;

        [RequiredField]
        [UIHint(UIHint.FsmFloat)]
        [Tooltip("The duration of the tween")]
        public FsmFloat duration;

        [UIHint(UIHint.FsmBool)]
        [Tooltip("If isSpeedBased is TRUE sets the tween as speed based (the duration will represent the number of units/degrees the tween moves x second). NOTE: if you want your speed to be constant, also set the ease to Ease.Linear.")]
        public FsmBool setSpeedBased;

        [UIHint(UIHint.FsmFloat)]
        [Tooltip("Set a delayed startup for the tween")]
        public FsmFloat startDelay;

        [ActionSection("Events")]
        [UIHint(UIHint.FsmEvent)]
        public FsmEvent startEvent;
        [UIHint(UIHint.FsmEvent)]
        public FsmEvent finishEvent;
        [UIHint(UIHint.FsmBool)]
        [Tooltip("If TRUE this action will finish immediately, if FALSE it will finish when the tween is complete.")]
        public FsmBool finishImmediately;

        [ActionSection("Tween ID")]

        [UIHint(UIHint.Description)]
        public string tweenIdDescription = "Set an ID for the tween, which can then be used as a filter with DOTween's Control Methods";

        [Tooltip("Select the source for the tween ID")]
        public Doozy.PlayMaker.Actions.TweenId tweenIdType;

        [UIHint(UIHint.FsmString)]
        [Tooltip("Use a String as the tween ID")]
        public FsmString stringAsId;

        [UIHint(UIHint.Tag)]
        [Tooltip("Use a Tag as the tween ID")]
        public FsmString tagAsId;

        [ActionSection("Ease Settings")]

        public Doozy.PlayMaker.Actions.SelectedEase selectedEase;
        [Tooltip("Sets the ease of the tween. If applied to a Sequence instead of a Tweener, the ease will be applied to the whole Sequence as if it was a single animated timeline.Sequences always have Ease.Linear by default, independently of the global default ease settings.")]
        public Ease easeType;
        public FsmAnimationCurve animationCurve;

        [ActionSection("Loop Settings")]

        [UIHint(UIHint.Description)]
        public string loopsDescriptionArea = "Setting loops to -1 will make the tween loop infinitely.";
        [UIHint(UIHint.FsmInt)]
        [Tooltip("Setting loops to -1 will make the tween loop infinitely.")]
        public FsmInt loops;

        [Tooltip("Sets the looping options (Restart, Yoyo, Incremental) for the tween. LoopType.Restart: When a loop ends it will restart from the beginning. LoopType.Yoyo: When a loop ends it will play backwards until it completes another loop, then forward again, then backwards again, and so on and on and on. LoopType.Incremental: Each time a loop ends the difference between its endValue and its startValue will be added to the endValue, thus creating tweens that increase their values with each loop cycle. Has no effect if the tween has already started.Also, infinite loops will not be applied if the tween is inside a Sequence.")]
        public DG.Tweening.LoopType loopType = DG.Tweening.LoopType.Restart;

        [ActionSection("Special Settings")]

        [UIHint(UIHint.FsmBool)]
        [Tooltip("If autoKillOnCompletion is set to TRUE the tween will be killed as soon as it completes, otherwise it will stay in memory and you'll be able to reuse it.")]
        public FsmBool autoKillOnCompletion;

        [UIHint(UIHint.FsmBool)]
        [Tooltip("Sets the recycling behaviour for the tween. If you don't set it then the default value (set either via DOTween.Init or DOTween.defaultRecyclable) will be used.")]
        public FsmBool recyclable;

        [Tooltip("Sets the type of update (Normal, Late or Fixed) for the tween and eventually tells it to ignore Unity's timeScale. UpdateType.Normal: Updates every frame during Update calls. UpdateType.Late: Updates every frame during LateUpdate calls. UpdateType.Fixed: Updates using FixedUpdate calls. ")]
        public UpdateType updateType = UpdateType.Normal;

        [UIHint(UIHint.FsmBool)]
        [Tooltip(" If TRUE the tween will ignore Unity's Time.timeScale. NOTE: independentUpdate works also with UpdateType.Fixed but is not recommended in that case (because at timeScale 0 FixedUpdate won't run).")]
        public FsmBool isIndependentUpdate;

        [ActionSection("Debug Options")]
        [UIHint(UIHint.FsmBool)]
        public FsmBool debugThis;

        private Tweener tween;

        public override void Reset()
        {
            base.Reset();
            gameObject = null;
            punch = new FsmVector3 { UseVariable = false };
            vibrato = new FsmInt { UseVariable = false, Value = 10 };
            elasticity = new FsmFloat { UseVariable = false, Value = 1 };
            snapping = new FsmBool { UseVariable = false, Value = false };
            duration = new FsmFloat { UseVariable = false };
            setSpeedBased = new FsmBool { UseVariable = false, Value = false };
            startDelay = new FsmFloat { Value = 0 };
            startEvent = null;
            finishEvent = null;
            finishImmediately = new FsmBool { UseVariable = false, Value = false };
            stringAsId = new FsmString { UseVariable = false };
            tagAsId = new FsmString { UseVariable = false };
            selectedEase = Doozy.PlayMaker.Actions.SelectedEase.EaseType;
            easeType = Ease.Linear;
            loops = new FsmInt { Value = 0 };
            loopType = DG.Tweening.LoopType.Restart;
            autoKillOnCompletion = new FsmBool { Value = true };
            recyclable = new FsmBool { Value = false };
            updateType = UpdateType.Normal;
            isIndependentUpdate = new FsmBool { Value = false };
            debugThis = new FsmBool { Value = false };
        }

        public override void OnEnter()
        {
            tween = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<Transform>().DOPunchRotation(punch.Value, duration.Value, vibrato.Value, elasticity.Value);
            if (setSpeedBased.Value) tween.SetSpeedBased();
            tween.SetTweenId(tweenIdType, stringAsId, tagAsId, Fsm.GetOwnerDefaultTarget(gameObject));
            tween.SetDelay(startDelay.Value);
            tween.SetSelectedEase(selectedEase, easeType, animationCurve);
            tween.SetLoops(loops.Value, loopType);
            tween.SetAutoKill(autoKillOnCompletion.Value);
            tween.SetRecyclable(recyclable.Value);
            tween.SetUpdate(updateType, isIndependentUpdate.Value);
            if (startEvent != null) tween.OnStart(() => { Fsm.Event(startEvent); });
            if (finishImmediately.Value == false) // This allows Action Sequences of this action.
            {
                if (finishEvent != null)
                    tween.OnComplete(() => { Fsm.Event(finishEvent); });
                else
                    tween.OnComplete(Finish);
            }
            tween.Play();
            if (debugThis.Value) State.Debug("DOTween Transform Punch Rotation");
            if (finishImmediately.Value) Finish();
        }
    }
}
