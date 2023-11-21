using UnityEngine;

[CreateAssetMenu(fileName = "NoteAppearanceSO", menuName = "Map/Appearance/Note Appearance SO")]
public class NoteAppearanceSO : ScriptableObject
{
    [SerializeField] private GameObject notePrefab;

    [Space(10)][SerializeField] private Sprite unknownSprite;

    [SerializeField] private Sprite arrowSprite;
    [SerializeField] private Sprite dotSprite;

    [Space(10)][SerializeField] private Material unknownNoteMaterial;

    [Space(10)][SerializeField] private Material blueNoteSharedMaterial;

    [SerializeField] private Material redNoteSharedMaterial;

    [Space(20)]
    [Header("ChromaToggle Notes")]
    [SerializeField]
    private Sprite deflectSprite;

    [Space(10)][SerializeField] private Material greenNoteSharedMaterial;

    [SerializeField] private Material magentaNoteSharedMaterial;

    [Space(10)][SerializeField] private Material monochromeSharedNoteMaterial;

    [SerializeField] private Material duochromeSharedNoteMaterial;

    [Space(10)][SerializeField] private Material superNoteSharedMaterial;

    public Color RedColor { get; private set; } = BeatSaberSong.DefaultLeftNote;
    public Color BlueColor { get; private set; } = BeatSaberSong.DefaultRightNote;
    public Color YellowColor { get; private set; } = BeatSaberSong.DefaultYellowNote;
    public Color GreenColor { get; private set; } = BeatSaberSong.DefaultGreenNote;
    public Color PurpleColor { get; private set; } = BeatSaberSong.DefaultPurpleNote;
    public Color PinkColor { get; private set; } = BeatSaberSong.DefaultPinkNote;
    public Color GreyColor { get; private set; } = BeatSaberSong.DefaultGreyNote;
    public Color BrownColor { get; private set; } = BeatSaberSong.DefaultBrownNote;

    public void UpdateColor(Color red, Color blue)
    {
        RedColor = red;
        BlueColor = blue;
    }

    public void SetNoteAppearance(BeatmapNoteContainer note)
    {
        if (note.MapNoteData.Type != BeatmapNote.NoteTypeBomb)
        {
            if (note.MapNoteData.CutDirection != BeatmapNote.NoteCutDirectionAny)
            {
                note.SetArrowVisible(true);
                note.SetDotVisible(false);
            }
            else
            {
                note.SetArrowVisible(false);
                note.SetDotVisible(true);
            }

            //Since sometimes the user can hover over the note grid before all the notes are loading,
            //we create material instances here to prevent NullReferenceExceptions.
            switch (note.MapNoteData.Type)
            {
                case BeatmapNote.NoteTypeA:
                    note.SetColor(RedColor);
                    break;
                case BeatmapNote.NoteTypeB:
                    note.SetColor(BlueColor);
                    break;
                case BeatmapNote.NoteTypeC:
                    note.SetColor(YellowColor);
                    break;
                /*case BeatmapNote.NoteTypeSkaterSteer:
                    note.SetColor(GreenColor);
                    break;*/
                case BeatmapNote.NoteTypeSkaterSteerHold:
                    note.SetColor(GreenColor);
                    break;
                case BeatmapNote.NoteTypeSkaterSteerRelease:
                    note.SetColor(GreenColor);
                    break;
                case BeatmapNote.NoteTypeSkaterSteerEnd:
                    note.SetColor(GreenColor);
                    break;
                case BeatmapNote.NoteTypeSkaterTricks:
                    note.SetColor(PurpleColor);
                    break;
                case BeatmapNote.NoteTypePlayerDance:
                    note.SetColor(PinkColor);
                    break;
                case BeatmapNote.NoteTypeCameraPoint:
                    note.SetColor(BrownColor);
                    break;
                case BeatmapNote.NoteTypeEmptyNote:
                    note.SetColor(GreyColor);
                    break;
                default:
                    note.SetColor(null);
                    break;
            }
        }
        else
        {
            note.SetArrowVisible(false);
            note.SetDotVisible(false);
            note.SetColor(null);
        }

        if (note.MapNoteData.CustomData?.HasKey("_color") ?? false)
            note.SetColor(note.MapNoteData.CustomData["_color"]);
    }
}
