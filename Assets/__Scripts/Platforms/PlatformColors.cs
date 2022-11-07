using System;
using UnityEngine;

[Serializable]
public class PlatformColors
{
    public Color RedColor = BeatSaberSong.DefaultLeftColor;
    public Color BlueColor = BeatSaberSong.DefaultRightColor;
    public Color YellowColor = BeatSaberSong.DefaultYellowColor;
    public Color RedBoostColor = BeatSaberSong.DefaultLeftColor;
    public Color BlueBoostColor = BeatSaberSong.DefaultRightColor;
    public Color RedNoteColor = BeatSaberSong.DefaultLeftNote;
    public Color BlueNoteColor = BeatSaberSong.DefaultRightNote;
    public Color YellowNoteColor = BeatSaberSong.DefaultYellowNote;
    public Color ObstacleColor = BeatSaberSong.DefaultLeftNote;

    public PlatformColors Clone() =>
        new PlatformColors
        {
            RedColor = RedColor,
            BlueColor = BlueColor,
            YellowColor = YellowColor,
            RedBoostColor = RedBoostColor,
            BlueBoostColor = BlueBoostColor,
            RedNoteColor = RedNoteColor,
            BlueNoteColor = BlueNoteColor,
            YellowNoteColor = YellowNoteColor,
            ObstacleColor = ObstacleColor
        };
}
