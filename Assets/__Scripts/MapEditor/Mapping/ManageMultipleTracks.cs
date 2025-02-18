using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ManageMultipleTracks : MonoBehaviour
{
    [SerializeField] private GameObject trackPrefab;
    [SerializeField] private GridChild spectatorGridChild;
    [SerializeField] private GridChild waveformGridChild;
    [SerializeField] private BeatmapObjectCallbackController defaultCallbackController;
    [SerializeField] private BeatmapObjectCallbackController beatSaberCutCallbackController;
    [SerializeField] private NotesContainer container;
    [SerializeField] private AudioTimeSyncController atsc;
    [SerializeField] private GameObject metronomeUI;
    [SerializeField] private AudioUtil audioUtil;

    [SerializeField, Space(10)] private float trackLocalOffset;
    [SerializeField] private float trackLimit;

    private GridChild noteGridChild;

    private Vector3 lastTrackPosition;

    private List<GameObject> lastTracks;
    private List<GameObject> trackInfos;

    private void Awake()
    {
        noteGridChild = GetComponent<GridChild>();

        lastTracks = new List<GameObject>();
        trackInfos = new List<GameObject>();
    }

    private void OnCreateTrackPerformed(InputAction.CallbackContext context)
    {
        if (lastTracks.Count >= trackLimit) return;

        var track = Instantiate(trackPrefab, lastTrackPosition + new Vector3(trackLocalOffset, 0, 0), Quaternion.identity);

        track.transform.parent = transform;

        lastTracks.Add(track);

        track.name = "Track " + (lastTracks.Count + 1);

        track.transform.localPosition = new Vector3(track.transform.localPosition.x, 0, track.transform.localPosition.z);

        lastTrackPosition = track.transform.position;

        track.GetComponentInChildren<DingOnNotePassingGrid>().InitRefrences(defaultCallbackController, beatSaberCutCallbackController, container, atsc, audioUtil);
        track.GetComponentInChildren<VisualFeedback>().InitRefrences(defaultCallbackController, atsc, lastTracks.Count + 1);
        track.GetComponentInChildren<MetronomeHandler>().InitRefrences(atsc, metronomeUI);

        var noteTrackInfo = track.GetComponentsInChildren<NoteTrackInfo>();

        foreach (var info in noteTrackInfo)
        {
            info.TrackNumber = lastTracks.Count + 1;
            trackInfos.Add(info.gameObject);
        }

        UpdateTrackGridOffsets(1);
    }

    private void OnDeleteTrackPerformed(InputAction.CallbackContext context)
    {
        if (lastTracks.Count == 0)
        {
            lastTrackPosition = Vector3.zero;
            return;
        }

        Destroy(lastTracks[lastTracks.Count - 1]);

        lastTracks.RemoveAt(lastTracks.Count - 1);

        trackInfos.RemoveAt(trackInfos.Count - 1);

        if (lastTracks.Count != 0)
            lastTrackPosition = lastTracks[lastTracks.Count - 1].transform.position;
        else
            lastTrackPosition = Vector3.zero;

        UpdateTrackGridOffsets(-1);
    }

    private void UpdateTrackGridOffsets(int offset)
    {
        spectatorGridChild.LocalOffset += new Vector3(trackLocalOffset * offset, 0, 0);
        waveformGridChild.LocalOffset += new Vector3(trackLocalOffset * offset, 0, 0);

        noteGridChild.Size += offset * 5;

        GridOrderController.MarkDirty();
    }

    public int GetTrackInfo(GameObject track)
    {
        if (trackInfos.Count == 0) return 1;

        for (var i = 0; i < trackInfos.Count; i++)
        {
            if (trackInfos[i] == track)
                return trackInfos[i].GetComponent<NoteTrackInfo>().TrackNumber;
        }

        return 1;
    }

    public void UpdateTracks(int targetTrackCount)
    {
        int currentTrackCount = lastTracks.Count;
        
        if (targetTrackCount > currentTrackCount)
        {
            // Spawn new tracks
            for (int i = 0; i < targetTrackCount - currentTrackCount; i++)
            {
                OnCreateTrackPerformed(new InputAction.CallbackContext());
            }
        }
    }
}
