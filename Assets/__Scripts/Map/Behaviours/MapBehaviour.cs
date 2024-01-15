using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Serialization;

public enum BehaviourType : int
{
    None = 0,
    SequenceTriggerBehaviour = 1,
    MoveBehaviour = 2,
    RotateBehaviour = 3,
    ScaleBehaviour = 4,
    ColorBehaviour = 5,
    AnimationBehaviour = 6,
    EventBehaviour = 7,
    ChangeActiveStateBehaviour = 8,
}

[Serializable]
public class MapBehaviour : BeatmapObject
{
    

    //============// VALUES //============//

    public int LineIndex;
    public int LineLayer;
    [FormerlySerializedAs("Types")] public BehaviourType Type;
    public uint ID;

    public MapBehaviour(JSONNode node)
    {
        Time = RetrieveRequiredNode(node, "_time").AsFloat;
        LineIndex = RetrieveRequiredNode(node, "_lineIndex").AsInt;
        LineLayer = RetrieveRequiredNode(node, "_lineLayer").AsInt;
        Type = (BehaviourType)RetrieveRequiredNode(node, "_types").AsInt;
        CustomData = node["_customData"];
    }

    public MapBehaviour(float time, int lineIndex, int lineLayer, BehaviourType type, JSONNode customData = null)
    {
        Time = time;
        LineIndex = lineIndex;
        LineLayer = lineLayer;
        Type = type;
        CustomData = customData;
    }

    public override ObjectType BeatmapType { get; set; } = ObjectType.Behaviour;

    public override JSONNode ConvertToJson()
    {
        JSONNode node = new JSONObject();

        node["_time"] = Math.Round(Time, DecimalPrecision);
        node["_lineIndex"] = LineIndex;
        node["_lineLayer"] = LineLayer;
        node["_types"] = (int)Type;

        return node;
    }

    protected override bool IsConflictingWithObjectAtSameTime(BeatmapObject other, bool deletion = false)
    {
        if (other is MapBehaviour note)
            // Only down to 1/4 spacing
            return Vector2.Distance(note.GetPosition(), GetPosition()) < 0.1;
        return false;
    }

    public Vector2 GetPosition()
    {
        if (CustomData?.HasKey("_position") ?? false)
            return CustomData["_position"].ReadVector2() + new Vector2(0.5f, 0);

        var position = LineIndex - 1.5f;
        float layer = LineLayer;

        if (LineIndex >= 1000)
            position = (LineIndex / 1000f) - 2.5f;
        else if (LineIndex <= -1000) position = (LineIndex / 1000f) - 0.5f;

        if (LineLayer >= 1000 || LineLayer <= -1000) layer = (LineLayer / 1000f) - 1f;

        return new Vector2(position, layer);
    }

    public Vector3 GetScale()
    {
        if (CustomData?.HasKey("_scale") ?? false) return CustomData["_scale"].ReadVector3();
        return Vector3.one;
    }
}
