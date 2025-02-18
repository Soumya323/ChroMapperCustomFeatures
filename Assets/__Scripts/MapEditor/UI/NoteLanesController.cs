using UnityEngine;
using UnityEngine.Serialization;

public class NoteLanesController : MonoBehaviour
{
    [FormerlySerializedAs("noteGrid")] public Transform NoteGrid;
    [SerializeField] private GridChild notePlacementGridChild;
    [SerializeField] private ManageMultipleTracks manageMultipleTracks; // Add this line

    private void Start()
    {
        Settings.NotifyBySettingName("NoteLanes", UpdateNoteLanes);
        UpdateNoteLanes(4);
    }

    private void OnDestroy() => Settings.ClearSettingNotifications("NoteLanes");

    public void UpdateNoteLanes(object value)
    {
        // var noteLanesText = value.ToString();
        // if (int.TryParse(noteLanesText, out var noteLanes))
        // {
        //     if (noteLanes < 4) return;
        //     noteLanes -= noteLanes % 2; //Sticks to even numbers for note lanes.
        //     //notePlacementGridChild.Size = noteLanes / 2;
        //     //NoteGrid.localScale = new Vector3((float)noteLanes / 10, 1, NoteGrid.localScale.z);

        //     // Update tracks
        //     int tracksToSpawn = (noteLanes / 4) - 1; // Calculate number of additional tracks
        //     manageMultipleTracks.UpdateTracks(tracksToSpawn);
        // }

        manageMultipleTracks.UpdateTracks(BeatSaberSongContainer.Instance.DifficultyData.NumberOfTracks - 1);
    }
}
