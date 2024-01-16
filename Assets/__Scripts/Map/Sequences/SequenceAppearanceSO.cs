
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SequenceAppearanceSO", menuName = "Map/Appearance/Sequence Appearance SO")]
public class SequenceAppearanceSO : ScriptableObject
{
    public Color DefaultSequenceColor = Color.cyan;
    [SerializeField] private Color negativeWidthColor = Color.green;
    [SerializeField] private Color negativeDurationColor = Color.yellow;

    public void SetSequenceAppearance(BeatmapSequenceContainer obj, PlatformDescriptor platform = null)
    {
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
                    obj.SetColor(DefaultSequenceColor);
                if (obj.SequenceData.CustomData.HasKey("_color"))
                    obj.SetColor(obj.SequenceData.CustomData["_color"].ReadColor(DefaultSequenceColor));
            }
            // else if (obj.SequenceData.Width < 0 && Settings.Instance.ColorFakeWalls)
            // {
            //     obj.SetColor(negativeWidthColor);
            // }
            else
            {
                obj.SetColor(DefaultSequenceColor);
            }
        }
    }
}
