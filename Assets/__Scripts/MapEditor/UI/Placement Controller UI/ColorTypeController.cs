using UnityEngine;
using UnityEngine.UI;

public class ColorTypeController : MonoBehaviour
{
    [SerializeField] private NotePlacement notePlacement;
    [SerializeField] private LightingModeController lightMode;
    [SerializeField] private CustomColorsUIController customColors;
    [SerializeField] private PlacementModeController placeMode;
    [SerializeField] private Image leftSelected;
    [SerializeField] private Image rightSelected;
    [SerializeField] private Image rightYellowSelected;
    [SerializeField] private Image rightGreenHoldSelected;
    [SerializeField] private Image rightGreenReleaseSelected;
    [SerializeField] private Image rightGreenEndSelected;
    [SerializeField] private Image rightPinkSelected;
    [SerializeField] private Image rightGreySelected;
    [SerializeField] private Image leftNote;
    [SerializeField] private Image leftLight;
    [SerializeField] private Image rightNote;
    [SerializeField] private Image rightLight;
    [SerializeField] private Image rightYellowNote;
    [SerializeField] private Image rightYellowLight;
    [SerializeField] private Image rightGreenNote;
    [SerializeField] private Image rightGreenLight;
    [SerializeField] private Image rightPinkNote;
    [SerializeField] private Image rightPinkLight;
    [SerializeField] private Image rightGreyNote;
    [SerializeField] private Image rightGreyLight;

    //[SerializeField] private TMP_Text steerMode;

    private PlatformDescriptor platform;

    private void Start()
    {
        leftSelected.enabled = true;
        rightSelected.enabled = false;
        LoadInitialMap.PlatformLoadedEvent += SetupColors;
        customColors.CustomColorsUpdatedEvent += UpdateColors;
    }

    private void OnDestroy()
    {
        customColors.CustomColorsUpdatedEvent -= UpdateColors;
        LoadInitialMap.PlatformLoadedEvent -= SetupColors;
    }

    private void SetupColors(PlatformDescriptor descriptor)
    {
        platform = descriptor;
        UpdateColors();
    }

    private void UpdateColors()
    {
        leftNote.color = platform.Colors.RedNoteColor;
        leftLight.color = platform.Colors.RedColor;
        rightNote.color = platform.Colors.BlueNoteColor;
        rightLight.color = platform.Colors.BlueColor;
        rightYellowNote.color = platform.Colors.YellowNoteColor;
        rightYellowLight.color = platform.Colors.YellowColor;
        rightGreenNote.color = platform.Colors.GreenNoteColor;
        rightGreenLight.color = platform.Colors.GreenColor;
        rightPinkNote.color = platform.Colors.PinkNoteColor;
        rightPinkLight.color = platform.Colors.PinkColor;
        rightGreyNote.color = platform.Colors.GreyNoteColor;
        rightGreyLight.color = platform.Colors.GreyColor;
    }

    public void RedNote(bool active)
    {
        if (active) UpdateValue(BeatmapNote.NoteTypeA);
    }

    public void BlueNote(bool active)
    {
        if (active) UpdateValue(BeatmapNote.NoteTypeB);
    }

    public void YellowNote(bool active)
    {
        if (active) UpdateValue(BeatmapNote.NoteTypeC);
    }

    public void GreenSteerHold(bool active)
    {
        if(!active) return;

        UpdateValue(BeatmapNote.NoteTypeSkaterSteerHold);
    }

    public void GreenSteerRelease(bool active)
    {
        if(!active) return;

        UpdateValue(BeatmapNote.NoteTypeSkaterSteerRelease);
    }

    public void GreenSteerEnd(bool active)
    {
        if(!active) return;

        UpdateValue(BeatmapNote.NoteTypeSkaterSteerEnd);
    }

    public void PurpleNote(bool active)
    {
        if (active) UpdateValue(BeatmapNote.NoteTypeSkaterTricks);
    }

    public void PinkNote(bool active)
    {
        if (active) UpdateValue(BeatmapNote.NoteTypePlayerDance);
    }

    public void BrownNote(bool active)
    {
        if (active) UpdateValue(BeatmapNote.NoteTypeCameraPoint);
    }

    public void GreyNote(bool active)
    {
        if (active) UpdateValue(BeatmapNote.NoteTypeEmptyNote);
    }

    public void UpdateValue(int type)
    {
        placeMode.SetMode(PlacementModeController.PlacementMode.Note);
        notePlacement.UpdateType(type);
        lightMode.UpdateValue();
        UpdateUI();
    }

    public void UpdateUI()
    {
        leftSelected.enabled = notePlacement.queuedData.Type == BeatmapNote.NoteTypeA;
        rightSelected.enabled = notePlacement.queuedData.Type == BeatmapNote.NoteTypeB;
        rightYellowSelected.enabled = notePlacement.queuedData.Type == BeatmapNote.NoteTypeC;

        rightGreenHoldSelected.enabled = notePlacement.queuedData.Type == BeatmapNote.NoteTypeSkaterSteerHold;
        rightGreenReleaseSelected.enabled = notePlacement.queuedData.Type == BeatmapNote.NoteTypeSkaterSteerRelease;
        rightGreenEndSelected.enabled = notePlacement.queuedData.Type == BeatmapNote.NoteTypeSkaterSteerEnd;
        
        rightPinkSelected.enabled = notePlacement.queuedData.Type == BeatmapNote.NoteTypePlayerDance;
        rightGreySelected.enabled = notePlacement.queuedData.Type == BeatmapNote.NoteTypeEmptyNote;
    }

    public bool LeftSelectedEnabled() => leftSelected.enabled;
    public bool RightSelectedEnabled() => rightSelected.enabled;
    public bool YellowSelectedEnabled() => rightYellowSelected.enabled;
    public bool PinkSelectedEnabled() => rightPinkSelected.enabled;
    public bool GreySelectedEnabled() => rightGreySelected.enabled;
}
