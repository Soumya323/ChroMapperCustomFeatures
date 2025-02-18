﻿using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "ObstacleAppearanceSO", menuName = "Map/Appearance/Obstacle Appearance SO")]
public class ObstacleAppearanceSO : ScriptableObject
{
    [FormerlySerializedAs("defaultObstacleColor")] public Color DefaultObstacleColor = BeatSaberSong.DefaultLeftColor;
    [SerializeField] private Color negativeWidthColor = Color.green;
    [SerializeField] private Color negativeDurationColor = Color.yellow;

    public void SetObstacleAppearance(BeatmapObstacleContainer obj, PlatformDescriptor platform = null)
    {
        if (platform != null) DefaultObstacleColor = platform.Colors.ObstacleColor;

        if (obj.SequenceData.Duration < 0 && Settings.Instance.ColorFakeWalls)
        {
            obj.SetColor(negativeDurationColor);
        }
        else
        {
            if (obj.SequenceData.CustomData != null)
            {
                var wallSize = obj.SequenceData.CustomData["_scale"]?.ReadVector2() ?? Vector2.one;
                if (wallSize.x < 0 || (wallSize.y < 0 && Settings.Instance.ColorFakeWalls))
                    obj.SetColor(negativeWidthColor);
                else
                    obj.SetColor(DefaultObstacleColor);
                if (obj.SequenceData.CustomData.HasKey("_color"))
                    obj.SetColor(obj.SequenceData.CustomData["_color"].ReadColor(DefaultObstacleColor));
            }
            else if (obj.SequenceData.Width < 0 && Settings.Instance.ColorFakeWalls)
            {
                obj.SetColor(negativeWidthColor);
            }
            else
            {
                obj.SetColor(DefaultObstacleColor);
            }
        }
    }
}
