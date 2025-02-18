﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using TMPro;

public class BeatmapNoteContainer : BeatmapObjectContainer
{
    private static readonly Color unassignedColor = new Color(0.1544118f, 0.1544118f, 0.1544118f);

    [FormerlySerializedAs("mapNoteData")] public BeatmapNote MapNoteData;

    [SerializeField] private GameObject simpleBlock;
    [SerializeField] private GameObject complexBlock;

    [SerializeField] private List<MeshRenderer> noteRenderer;
    [SerializeField] private MeshRenderer bombRenderer;
    [SerializeField] private MeshRenderer dotRenderer;
    [SerializeField] private MeshRenderer arrowRenderer;
    [SerializeField] private SpriteRenderer swingArcRenderer;
    [SerializeField] private GameObject redNoteLabel;
    [SerializeField] private GameObject blueNoteLabel;
    [SerializeField] private GameObject yellowNoteLabel;
    [SerializeField] private GameObject greenNoteSteerHoldLabel;
    [SerializeField] private GameObject greenNoteSteerReleaseLabel;
    [SerializeField] private GameObject greenNoteSteerEndLabel;
    [SerializeField] private GameObject purpleNoteLabel;
    [SerializeField] private GameObject pinkNoteLabel;
    [SerializeField] private GameObject greyNoteLabel;
    [SerializeField] private GameObject brownNoteLabel;
    [SerializeField] private bool debugShowCustomMesh = false;
    private static Vector3 dir;

    public override BeatmapObject ObjectData { get => MapNoteData; set => MapNoteData = (BeatmapNote)value; }

    public override void Setup()
    {
        base.Setup();

        if (simpleBlock != null)
        {
            simpleBlock.SetActive(Settings.Instance.SimpleBlocks);
            complexBlock.SetActive(!Settings.Instance.SimpleBlocks);

            MaterialPropertyBlock.SetFloat("_Lit", Settings.Instance.SimpleBlocks ? 0 : 1);
            MaterialPropertyBlock.SetFloat("_TranslucentAlpha", Settings.Instance.PastNoteModelAlpha);

            UpdateMaterials();
        }

        SetArcVisible(NotesContainer.ShowArcVisualizer);
    }

    internal static Vector3 Directionalize(BeatmapNote mapNoteData)
    {
        return Vector3.zero;

        if (mapNoteData is null) return Vector3.zero;
        var directionEuler = Vector3.zero;
        var cutDirection = mapNoteData.CutDirection;
        switch (cutDirection)
        {
            case BeatmapNote.NoteCutDirectionUp:
                directionEuler += new Vector3(0, 0, 180);
                break;
            case BeatmapNote.NoteCutDirectionDown:
                directionEuler += new Vector3(0, 0, 0);
                break;
            case BeatmapNote.NoteCutDirectionLeft:
                directionEuler += new Vector3(0, 0, -90);
                break;
            case BeatmapNote.NoteCutDirectionRight:
                directionEuler += new Vector3(0, 0, 90);
                break;
            case BeatmapNote.NoteCutDirectionUpRight:
                directionEuler += new Vector3(0, 0, 135);
                break;
            case BeatmapNote.NoteCutDirectionUpLeft:
                directionEuler += new Vector3(0, 0, -135);
                break;
            case BeatmapNote.NoteCutDirectionDownLeft:
                directionEuler += new Vector3(0, 0, -45);
                break;
            case BeatmapNote.NoteCutDirectionDownRight:
                directionEuler += new Vector3(0, 0, 45);
                break;
        }

        if (mapNoteData.CustomData?.HasKey("_cutDirection") ?? false)
        {
            directionEuler = new Vector3(0, 0, mapNoteData.CustomData["_cutDirection"]?.AsFloat ?? 0);
        }
        else
        {
            if (cutDirection >= 1000) directionEuler += new Vector3(0, 0, 360 - (cutDirection - 1000));
        }

        dir = directionEuler;

        return directionEuler;
    }

    public void SetDotVisible(bool b) => dotRenderer.enabled = b;

    public void SetArrowVisible(bool b) => arrowRenderer.enabled = b;

    public void SetBomb(bool b)
    {
        simpleBlock.SetActive(!b && Settings.Instance.SimpleBlocks);
        complexBlock.SetActive(!b && !Settings.Instance.SimpleBlocks);

        bombRenderer.gameObject.SetActive(b);
        bombRenderer.enabled = b;
    }

    public void SetRedNote(bool b)
    {
        redNoteLabel.SetActive(b);
        if (b && debugShowCustomMesh)
        {
            TextMeshPro textMeshProComp = this.GetComponentInChildren<TextMeshPro>();
            if (textMeshProComp != null) textMeshProComp.enabled = false;
            simpleBlock.SetActive(false);
            complexBlock.SetActive(false);
            arrowRenderer.enabled = false;
        }
    }

    public void SetBlueNote(bool b)
    {
        blueNoteLabel.SetActive(b);
        if (b && debugShowCustomMesh)
        {
            TextMeshPro textMeshProComp = this.GetComponentInChildren<TextMeshPro>();
            if (textMeshProComp != null) textMeshProComp.enabled = false;
            simpleBlock.SetActive(false);
            complexBlock.SetActive(false);
            arrowRenderer.enabled = false;
        }
    }

    public void SetYellowNote(bool b)
    {
        yellowNoteLabel.SetActive(b);
        if (b && debugShowCustomMesh)
        {
            TextMeshPro textMeshProComp = this.GetComponentInChildren<TextMeshPro>();
            if (textMeshProComp != null) textMeshProComp.enabled = false;
            simpleBlock.SetActive(false);
            complexBlock.SetActive(false);
            arrowRenderer.enabled = false;
        }
    }
    
    public void SetGreenSteerHoldNote(bool b)
    {
        greenNoteSteerHoldLabel.SetActive(b);

        MeshRenderer mesh = greenNoteSteerHoldLabel.GetComponentInChildren<MeshRenderer>();

        float angleY;


        switch (MapNoteData.LineIndex % 5)
        {
            case 0:  // for indices 0, 5, 10, 15
                mesh.materials[1].color = Color.blue;
                mesh.materials[2].color = Color.blue;
                mesh.materials[3].color = Color.blue;
                mesh.materials[4].color = Color.black;
                angleY = 180;
                break;
            case 1:  // for indices 1, 6, 11, 16
                mesh.materials[1].color = Color.red;
                mesh.materials[2].color = Color.red;
                mesh.materials[3].color = Color.red;
                mesh.materials[4].color = Color.black;
                angleY = 0;
                break;
            case 2:  // for indices 2, 7, 12, 17
                mesh.materials[1].color = Color.red;
                mesh.materials[2].color = Color.blue;
                mesh.materials[3].color = Color.red;
                mesh.materials[4].color = Color.blue;
                angleY = 0;
                break;
            default:  // for all other indices (3,4, 8,9, 13,14, 18,19)
                mesh.materials[1].color = Color.red;
                mesh.materials[2].color = Color.blue;
                mesh.materials[3].color = Color.red;
                mesh.materials[4].color = Color.blue;
                angleY = 0;
                break;
        }

        if (b && debugShowCustomMesh)
        {
            TextMeshPro textMeshProComp = this.GetComponentInChildren<TextMeshPro>();
            if (textMeshProComp != null) textMeshProComp.enabled = false;
            simpleBlock.SetActive(false);
            complexBlock.SetActive(false);
            arrowRenderer.enabled = false;
        }

        greenNoteSteerHoldLabel.transform.localEulerAngles = new Vector3(0, angleY, -dir.z);
    }
    
    public void SetGreenSteerReleaseNote(bool b)
    {
        greenNoteSteerReleaseLabel.SetActive(b);

        MeshRenderer mesh = greenNoteSteerReleaseLabel.GetComponentInChildren<MeshRenderer>();

        switch (MapNoteData.LineIndex % 5)
        {
            case 0:
                mesh.materials[1].color = Color.blue;
                mesh.materials[2].color = Color.blue;
                break;
            case 1:
                mesh.materials[1].color = Color.red;
                mesh.materials[2].color = Color.red;
                break;
            case 2:
                mesh.materials[1].color = Color.red;
                mesh.materials[2].color = Color.blue;
                break;
            default:
                mesh.materials[1].color = Color.red;
                mesh.materials[2].color = Color.blue;
                break;
        }

        if (b && debugShowCustomMesh)
        {
            TextMeshPro textMeshProComp = this.GetComponentInChildren<TextMeshPro>();
            if (textMeshProComp != null) textMeshProComp.enabled = false;
            simpleBlock.SetActive(false);
            complexBlock.SetActive(false);
            arrowRenderer.enabled = false;
        }

        greenNoteSteerReleaseLabel.transform.localEulerAngles = new Vector3(0, 0, -dir.z);
    }
    
    public void SetGreenSteerEndNote(bool b)
    {
        greenNoteSteerEndLabel.SetActive(b);
        if (b && debugShowCustomMesh)
        {
            TextMeshPro textMeshProComp = this.GetComponentInChildren<TextMeshPro>();
            if (textMeshProComp != null) textMeshProComp.enabled = false;
            simpleBlock.SetActive(false);
            complexBlock.SetActive(false);
            arrowRenderer.enabled = false;
        }
    }

    public void SetPurpleNote(bool b)
    {
        purpleNoteLabel.SetActive(b);
    }

    public void SetPinkNote(bool b)
    {
        pinkNoteLabel.SetActive(b);
        if (b)
            Debug.Log("BeatmapNoteContainer : " + "pink note line index : " + MapNoteData.LineIndex + "    layer : " + MapNoteData.LineLayer);
    }

    public void SetGreyNote(bool b)
    {
        greyNoteLabel.SetActive(b);
    }

    public void SetBrownNote(bool b)
    {
        brownNoteLabel.SetActive(b);
    }

    public void SetArcVisible(bool showArcVisualizer)
    {
        if (swingArcRenderer != null) swingArcRenderer.enabled = showArcVisualizer;
    }

    public static BeatmapNoteContainer SpawnBeatmapNote(BeatmapNote noteData, ref GameObject notePrefab)
    {
        var container = Instantiate(notePrefab).GetComponent<BeatmapNoteContainer>();
        container.MapNoteData = noteData;
        container.transform.localEulerAngles = Directionalize(noteData);

        return container;
    }

    public override void UpdateGridPosition()
    {
        transform.localPosition = (Vector3)MapNoteData.GetPosition() +
                                  new Vector3(0, 0.5f, MapNoteData.Time * EditorScaleController.EditorScale);
        transform.localScale = MapNoteData.GetScale() + new Vector3(0.5f, 0.5f, 0.5f);

        UpdateCollisionGroups();

        MaterialPropertyBlock.SetFloat("_ObjectTime", MapNoteData.Time);
        SetRotation(AssignedTrack != null ? AssignedTrack.RotationValue.y : 0);

        UpdateMaterials();
    }

    public void SetColor(Color? color)
    {
        MaterialPropertyBlock.SetColor(BeatmapObjectContainer.color, color ?? unassignedColor);
        UpdateMaterials();
    }

    internal override void UpdateMaterials()
    {
        foreach (var renderer in noteRenderer) renderer.SetPropertyBlock(MaterialPropertyBlock);
        foreach (var renderer in SelectionRenderers) renderer.SetPropertyBlock(MaterialPropertyBlock);
        bombRenderer.SetPropertyBlock(MaterialPropertyBlock);
    }
}
