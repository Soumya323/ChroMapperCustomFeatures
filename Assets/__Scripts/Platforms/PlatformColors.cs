using System;
using UnityEngine;

[Serializable]
public class PlatformColors
{
    public Color RedColor = BeatSaberSong.DefaultLeftColor;
    public Color BlueColor = BeatSaberSong.DefaultRightColor;
    public Color YellowColor = BeatSaberSong.DefaultYellowColor;
    public Color GreenColor = BeatSaberSong.DefaultGreenColor;
    public Color PurpleColor = BeatSaberSong.DefaultPurpleColor;
    public Color PinkColor = BeatSaberSong.DefaultPinkColor;
    public Color GreyColor = BeatSaberSong.DefaultGreyColor;
    public Color BrownColor = BeatSaberSong.DefaultBrownColor;
    public Color RedBoostColor = BeatSaberSong.DefaultLeftColor;
    public Color BlueBoostColor = BeatSaberSong.DefaultRightColor;
    public Color RedNoteColor = BeatSaberSong.DefaultLeftNote;
    public Color BlueNoteColor = BeatSaberSong.DefaultRightNote;
    public Color YellowNoteColor = BeatSaberSong.DefaultYellowNote;
    public Color GreenNoteColor = BeatSaberSong.DefaultGreenNote;
    public Color PurpleNoteColor = BeatSaberSong.DefaultPurpleNote;
    public Color PinkNoteColor = BeatSaberSong.DefaultPinkNote;
    public Color GreyNoteColor = BeatSaberSong.DefaultGreyNote;
    public Color BrownNoteColor = BeatSaberSong.DefaultBrownNote;
    public Color ObstacleColor = BeatSaberSong.DefaultLeftNote;

    public PlatformColors Clone() =>
        new PlatformColors
        {
            RedColor = RedColor,
            BlueColor = BlueColor,
            YellowColor = YellowColor,
            GreenColor = GreenColor,
            PurpleColor = PurpleColor,
            PinkColor = PinkColor,
            GreyColor = GreyColor,
            RedBoostColor = RedBoostColor,
            BlueBoostColor = BlueBoostColor,
            RedNoteColor = RedNoteColor,
            BlueNoteColor = BlueNoteColor,
            YellowNoteColor = YellowNoteColor,
            GreenNoteColor = GreenNoteColor,
            PurpleNoteColor = PurpleNoteColor,
            PinkNoteColor = PinkNoteColor,
            GreyNoteColor = GreyNoteColor,
            ObstacleColor = ObstacleColor
        };
}
