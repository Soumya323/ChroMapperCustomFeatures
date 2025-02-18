using TMPro;
using UnityEngine;

public class DifficultyInfo : MonoBehaviour
{
    [SerializeField] private TMP_InputField bpmField;

    [SerializeField] private TMP_InputField reactionTimeField;

    [SerializeField] private TMP_InputField halfJumpDurationField;
    [SerializeField] private TMP_InputField jumpDistanceField;

    [SerializeField] private TMP_InputField njsField;
    [SerializeField] private TMP_InputField songBeatOffsetField;

    [SerializeField] private TMP_InputField numberOfTracksField;
    private int numberOfTracks = 0;

    public void Start()
    {
        njsField.onValueChanged.AddListener(v => UpdateValues());
        songBeatOffsetField.onValueChanged.AddListener(v => UpdateValues());
        bpmField.onValueChanged.AddListener(v => UpdateValues());
        numberOfTracksField.onValueChanged.AddListener(v => UpdateNumberOfTracks());
    }

    private void Update()
    {
        if(numberOfTracks <= 0)
        {
            numberOfTracks = BeatSaberSongContainer.Instance.DifficultyData.NumberOfTracks;
            numberOfTracksField.text = numberOfTracks.ToString();
        }
    }

    private void UpdateValues()
    {
        float.TryParse(bpmField.text, out var bpm);
        float.TryParse(njsField.text, out var songNoteJumpSpeed);
        float.TryParse(songBeatOffsetField.text, out var songStartBeatOffset);
        var halfJumpDuration = SpawnParameterHelper.CalculateHalfJumpDuration(songNoteJumpSpeed, songStartBeatOffset, bpm);

        var num = 60 / bpm;
        var jumpDistance = songNoteJumpSpeed * num * halfJumpDuration * 2;

        var beatms = 60000 / bpm;
        var reactionTime = beatms * halfJumpDuration;

        halfJumpDurationField.text = halfJumpDuration.ToString();
        jumpDistanceField.text = jumpDistance.ToString("0.00");
        reactionTimeField.text = reactionTime.ToString("N0") + " ms";
    }

    private void UpdateNumberOfTracks()
    {
        int.TryParse(numberOfTracksField.text, out var numberOfTracks);
        if (numberOfTracks < 1) numberOfTracks = 1;
        if (numberOfTracks > 4) numberOfTracks = 4;

        numberOfTracksField.text = numberOfTracks.ToString();

        BeatSaberSongContainer.Instance.DifficultyData.NumberOfTracks = numberOfTracks;
    }
}
