using UnityEngine;

public class NoteTrackInfo : MonoBehaviour
{
    [SerializeField] private int trackNumber;
    public int TrackNumber
    {
        get => trackNumber;
        set
        {
            trackNumber = value;
        }
    }
}
