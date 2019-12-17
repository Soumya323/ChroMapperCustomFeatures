﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    [SerializeField] private Transform objectParentTransform;

    private List<BeatmapObjectContainer> Containers = new List<BeatmapObjectContainer>();

    public int RotationValue = 0;
    public int RawRotation { get; private set; } = 0;
    private Vector3 rotationPoint = LoadInitialMap.PlatformOffset;

    public void AssignRotationValue(int rotation, bool rotate = true)
    {
        RotationValue = rotation;
        if (rotate)
        {
            transform.RotateAround(rotationPoint, Vector3.up, RotationValue);
            //transform.localPosition = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
        }
    }

    public void UpdatePosition(float position)
    {
        objectParentTransform.localPosition = new Vector3(objectParentTransform.localPosition.x,
            objectParentTransform.localPosition.y, position);
    }

    public void AttachContainer(BeatmapObjectContainer obj, int rawRotation)
    {
        obj.transform.SetParent(objectParentTransform, true);
        obj.AssignTrack(this);
        Containers.Add(obj);
        RawRotation = rawRotation;
    }
}