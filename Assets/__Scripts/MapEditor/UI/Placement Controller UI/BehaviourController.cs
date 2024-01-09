using UnityEngine;
using UnityEngine.UI;

public class BehaviourController : MonoBehaviour
{
    [SerializeField] 
    private NotePlacement notePlacement;

    [SerializeField] 
    private PlacementModeController placeMode;

    [SerializeField]
    private Image positionSelectedImage;

    [SerializeField]
    private Image rotationSelectedImage;

    [SerializeField]
    private Image scaleSelectedImage;

    [SerializeField]
    private Image colorSelectedImage;

    [SerializeField]
    private Image eventSelectedImage;

    [SerializeField]
    private Image changeActiveStatesSelectedImage;

    [SerializeField]
    private Image animationSelectedImage;

    [SerializeField]
    private Image sequenceSelectedImage;

    public void OnPosition()
    {
        UpdateValue(BeatmapNote.NoteTypeTransformPosition);
    }

    public void OnRotation()
    {
        UpdateValue(BeatmapNote.NoteTypeTransformRotation);
    }

    public void OnScale()
    {
        UpdateValue(BeatmapNote.NoteTypeTransformScale);
    }

    public void OnColor()
    {
        UpdateValue(BeatmapNote.NoteTypeColorOnTime);
    }

    public void OnEvent()
    {
        UpdateValue(BeatmapNote.NoteTypeEventOnTime);
    }

    public void OnChangeActiveState()
    {
        UpdateValue(BeatmapNote.NoteTypeChangeActiveState);
    }

    public void OnAnimationTrigger()
    {
        UpdateValue(BeatmapNote.NoteTypeAnimationTrigger);
    }

    public void OnSequenceTrigger()
    {
        UpdateValue(BeatmapNote.NoteTypeSequenceTrigger);
    }

    public void UpdateValue(int type)
    {
        notePlacement.UpdateType(type);
        placeMode.SetMode(PlacementModeController.PlacementMode.Note);

        UpdateBehaviourSelectedUI();
    }

    public void UpdateBehaviourSelectedUI()
    {
        positionSelectedImage.enabled = notePlacement.queuedData.Type == BeatmapNote.NoteTypeTransformPosition;
        rotationSelectedImage.enabled = notePlacement.queuedData.Type == BeatmapNote.NoteTypeTransformRotation; 
        scaleSelectedImage.enabled = notePlacement.queuedData.Type == BeatmapNote.NoteTypeTransformScale;
        colorSelectedImage.enabled = notePlacement.queuedData.Type == BeatmapNote.NoteTypeColorOnTime;
        eventSelectedImage.enabled = notePlacement.queuedData.Type == BeatmapNote.NoteTypeEventOnTime;
        changeActiveStatesSelectedImage.enabled = notePlacement.queuedData.Type == BeatmapNote.NoteTypeChangeActiveState;
        animationSelectedImage.enabled = notePlacement.queuedData.Type == BeatmapNote.NoteTypeAnimationTrigger;
        sequenceSelectedImage.enabled = notePlacement.queuedData.Type == BeatmapNote.NoteTypeSequenceTrigger;
    }
}
