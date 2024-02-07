using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BehaviourController : MonoBehaviour, CMInput.IChoreographyWorkflowsActions
{
    [SerializeField] private BehaviourPlacement behaviourPlacement;

    [SerializeField] private Image positionSelectedImage;

    [SerializeField] private Image rotationSelectedImage;

    [SerializeField] private Image scaleSelectedImage;

    [SerializeField] private Image colorSelectedImage;

    [SerializeField] private Image eventSelectedImage;

    [SerializeField] private Image changeActiveStatesSelectedImage;

    [SerializeField] private Image animationSelectedImage;

    [SerializeField] private Image sequenceSelectedImage;

    private void Start() => StartCoroutine(DelayStart());

    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(1);
        UpdateValue(BehaviourType.MoveBehaviour);
    }

    public void OnMove()
    {
        UpdateValue(BehaviourType.MoveBehaviour);
    }

    public void OnRotation()
    {
        UpdateValue(BehaviourType.RotateBehaviour);
    }

    public void OnScale()
    {
        UpdateValue(BehaviourType.ScaleBehaviour);
    }

    public void OnColor()
    {
        UpdateValue(BehaviourType.ColorBehaviour);
    }

    public void OnEvent()
    {
        UpdateValue(BehaviourType.EventBehaviour);
    }

    public void OnChangeActiveState()
    {
        UpdateValue(BehaviourType.ChangeActiveStateBehaviour);
    }

    public void OnAnimationTrigger()
    {
        UpdateValue(BehaviourType.AnimationBehaviour);
    }

    public void OnSequenceTrigger()
    {
        // UpdateValue(BehaviourType.SequenceTriggerBehaviour);
    }

    public void UpdateValue(BehaviourType type)
    {
        behaviourPlacement.UpdateType(type);

        UpdateBehaviourSelectedUI();
    }

    public void UpdateBehaviourSelectedUI()
    {
        positionSelectedImage.enabled = behaviourPlacement.queuedData.Type == BehaviourType.MoveBehaviour;
        rotationSelectedImage.enabled = behaviourPlacement.queuedData.Type == BehaviourType.RotateBehaviour;
        scaleSelectedImage.enabled = behaviourPlacement.queuedData.Type == BehaviourType.ScaleBehaviour;
        colorSelectedImage.enabled = behaviourPlacement.queuedData.Type == BehaviourType.ColorBehaviour;
        eventSelectedImage.enabled = behaviourPlacement.queuedData.Type == BehaviourType.EventBehaviour;
        changeActiveStatesSelectedImage.enabled = behaviourPlacement.queuedData.Type == BehaviourType.ChangeActiveStateBehaviour;
        animationSelectedImage.enabled = behaviourPlacement.queuedData.Type == BehaviourType.AnimationBehaviour;
        sequenceSelectedImage.enabled = behaviourPlacement.queuedData.Type == BehaviourType.SequenceTriggerBehaviour;
    }

    public void OnFunc1(InputAction.CallbackContext context)
    {
        if (context.performed) OnMove();
    }

    public void OnFunc2(InputAction.CallbackContext context)
    {
        if (context.performed) OnRotation();
    }

    public void OnFunc3(InputAction.CallbackContext context)
    {
        if (context.performed) OnScale();
    }

    public void OnFunc4(InputAction.CallbackContext context)
    {
        if (context.performed) OnColor();
    }

    public void OnFunc5(InputAction.CallbackContext context)
    {
        if (context.performed) OnEvent();
    }

    public void OnFunc6(InputAction.CallbackContext context)
    {
        if (context.performed) OnChangeActiveState();
    }

    public void OnFunc7(InputAction.CallbackContext context)
    {
        if (context.performed) OnAnimationTrigger();
    }

    public void OnFunc8(InputAction.CallbackContext context)
    {
        if (context.performed) OnSequenceTrigger();
    }
}
