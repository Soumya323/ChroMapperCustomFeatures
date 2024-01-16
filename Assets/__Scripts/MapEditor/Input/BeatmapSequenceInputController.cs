using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BeatmapSequenceInputController : BeatmapInputController<BeatmapSequenceContainer>, CMInput.IObstacleObjectsActions
{
    [SerializeField] private AudioTimeSyncController atsc;
    [SerializeField] private BPMChangesContainer bpmChangesContainer;
    [SerializeField] private SequenceAppearanceSO sequenceAppearanceSo;

    public void OnChangeWallDuration(InputAction.CallbackContext context)
    {
        if (CustomStandaloneInputModule.IsPointerOverGameObject<GraphicRaycaster>(-1, true)) return;
        RaycastFirstObject(out var seq);
        if (seq != null && !seq.Dragging && context.performed)
        {
            var original = BeatmapObject.GenerateCopy(seq.ObjectData);
            var snapping = 1f / atsc.GridMeasureSnapping;
            snapping *= context.ReadValue<float>() > 0 ? 1 : -1;

            var wallEndTime = seq.SequenceData.Time + seq.SequenceData.Duration;

            var bpmChange = bpmChangesContainer.FindLastBpm(wallEndTime);

            var songBpm = BeatSaberSongContainer.Instance.Song.BeatsPerMinute;
            var bpmRatio = songBpm / (bpmChange?.Bpm ?? songBpm);
            var durationTweak = snapping * bpmRatio;

            var nextBpm = bpmChangesContainer.FindLastBpm(wallEndTime + durationTweak);

            if (nextBpm != bpmChange)
            {
                if (snapping > 0)
                {
                    durationTweak = nextBpm.Time - wallEndTime;
                }
                else
                {
                    // I dont think any solution here will please everyone so i'll just go with my intuition
                    durationTweak = bpmChangesContainer.FindRoundedBpmTime(wallEndTime + durationTweak, snapping * -1) - wallEndTime;
                }
            }

            seq.SequenceData.Duration += durationTweak;
            seq.UpdateGridPosition();
            sequenceAppearanceSo.SetSequenceAppearance(seq);
            BeatmapActionContainer.AddAction(new BeatmapObjectModifiedAction(seq.ObjectData, seq.ObjectData, original));
        }
    }

    public void OnToggleHyperWall(InputAction.CallbackContext context)
    {
        if (CustomStandaloneInputModule.IsPointerOverGameObject<GraphicRaycaster>(-1, true)) return;
        RaycastFirstObject(out var seq);
        if (seq != null && !seq.Dragging && context.performed) ToggleHyperWall(seq);
    }

    public void ToggleHyperWall(BeatmapSequenceContainer seq)
    {
        if (BeatmapObject.GenerateCopy(seq.ObjectData) is BeatmapSequence edited)
        {
            edited.Time += seq.SequenceData.Duration;
            edited.Duration *= -1f;

            BeatmapActionContainer.AddAction(new BeatmapObjectModifiedAction(edited, seq.ObjectData, seq.ObjectData), true);
        }
    }
}
