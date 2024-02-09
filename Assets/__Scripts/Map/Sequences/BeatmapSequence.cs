using System;
using SimpleJSON;
using UnityEngine;

[Serializable]
public class BeatmapSequence : BeatmapObject, IBeatmapObjectBounds
{
    public const int ValueFullBarrier = 0;
    public const int ValueHighBarrier = 1;

    public static readonly float MappingextensionsStartHeightMultiplier = 1.35f;
    public static readonly float MappingextensionsUnitsToFullHeightWall = 1000 / 8.5f;

    public int LineIndex;

    // public int Type;
    public float Duration;
    // public int Width;

    /*
     * Sequence Logic
     */

    public BeatmapSequence(JSONNode node)
    {
        Time = RetrieveRequiredNode(node, "_time").AsFloat;
        LineIndex = RetrieveRequiredNode(node, "_lineIndex").AsInt + 1;
        // Type = RetrieveRequiredNode(node, "_type").AsInt;
        Duration = RetrieveRequiredNode(node, "_duration").AsFloat;
        // Width = RetrieveRequiredNode(node, "_width").AsInt;
        CustomData = node["_customData"];
    }

    public BeatmapSequence(float time, int lineIndex, float duration, JSONNode customData = null)
    {
        Time = time;
        LineIndex = lineIndex;
        // Type = type;
        Duration = duration;
        // Width = width;
        CustomData = customData;
    }

    public bool IsNoodleExtensionsWall => CustomData != null &&
                                          (CustomData.HasKey("_position") || CustomData.HasKey("_scale")
                                                                          || CustomData.HasKey("_localRotation") ||
                                                                          CustomData.HasKey("_rotation"));

    public override ObjectType BeatmapType { get; set; } = ObjectType.Sequence;

    public Vector2 GetCenter()
    {
        var bounds = GetShape();

        return new Vector2(bounds.Position + (bounds.Width / 2) + 4, bounds.StartHeight + (bounds.Height / 2));
    }

    public override JSONNode ConvertToJson()
    {
        JSONNode node = new JSONObject();
        node["_time"] = Math.Round(Time, DecimalPrecision);
        node["_lineIndex"] = LineIndex - 1;
        // node["_type"] = Type;
        node["_duration"] = Math.Round(Duration, DecimalPrecision); //Get rid of float precision errors
        // node["_width"] = Width;
        if (CustomData != null) node["_customData"] = CustomData;
        /*if (Settings.Instance.AdvancedShit) //This will be left commented unless its 100%, absolutely, positively required.
        {
            //By request of Spooky Ghost to determine BeatWalls VS CM walls
            if (!node["_customData"].HasKey("_editor"))
            {
                node["_customData"]["_editor"] = BeatSaberSongContainer.Instance.song.editor;
            }
        }*/
        return node;
    }

    protected override bool IsConflictingWithObjectAtSameTime(BeatmapObject other, bool deletion)
    {
        if (other is BeatmapSequence sequence)
        {
            if (IsNoodleExtensionsWall || sequence.IsNoodleExtensionsWall)
                return ConvertToJson().ToString() == other.ConvertToJson().ToString();
            // return LineIndex == sequence.LineIndex && Type == sequence.Type;
        }

        return false;
    }

    public override void Apply(BeatmapObject originalData)
    {
        base.Apply(originalData);

        if (originalData is BeatmapSequence seq)
        {
            // Type = seq.Type;
            // Width = seq.Width;
            LineIndex = seq.LineIndex;
            Duration = seq.Duration;
        }
    }

    public SequenceBounds GetShape()
    {
        var position = LineIndex - 2f; //Line index
        var startHeight = 0.0f;
        var height = 0.0f;
        // float width = Width;

        // ME

        // if (Width >= 1000) width = ((float)Width - 1000) / 1000;
        if (LineIndex >= 1000)
            position = (((float)LineIndex - 1000) / 1000f) - 2f;
        else if (LineIndex <= -1000)
            position = ((float)LineIndex - 1000) / 1000f;

        // if (Type > 1 && Type < 1000)
        // {
        //     startHeight = Type / (750 / 3.5f); //start height 750 == standard wall height
        //     height = 3.5f;
        // }
        // else if (Type >= 1000 && Type <= 4000)
        // {
        //     startHeight = 0; //start height = floor
        //     height = ((float)Type - 1000) /
        //              MappingextensionsUnitsToFullHeightWall; //1000 = no height, 2000 = full height
        // }
        // else if (Type > 4000)
        // {
        //     float modifiedType = Type - 4001;
        //     startHeight = modifiedType % 1000 / MappingextensionsUnitsToFullHeightWall *
        //                   MappingextensionsStartHeightMultiplier;
        //     height = modifiedType / 1000 / MappingextensionsUnitsToFullHeightWall;
        // }

        // NE

        //Just look at the difference in code complexity for Mapping Extensions support and Noodle Extensions support.
        //Hot damn.
        if (CustomData != null)
        {
            if (CustomData.HasKey("_position"))
            {
                var wallPos = CustomData["_position"]?.ReadVector2() ?? Vector2.zero;
                position = wallPos.x;
                // startHeight = wallPos.y;
            }

            // if (CustomData.HasKey("_scale"))
            // {
            //     var wallSize = CustomData["_scale"]?.ReadVector2() ?? Vector2.one;
            //     // width = wallSize.x;
            //     // height = wallSize.y;
            // }
        }

        return new SequenceBounds(position);
    }
}
