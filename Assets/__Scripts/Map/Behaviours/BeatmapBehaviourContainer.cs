using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BeatmapBehaviourContainer : BeatmapObjectContainer
{
    public MapBehaviour BehaviourData;
    public BehavioursContainer BehavioursContainer;

    public override BeatmapObject ObjectData { get => BehaviourData; set => BehaviourData = (MapBehaviour)value; }

    // [SerializeField] private GameObject cube;
    // [SerializeField] private Material material;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private TextMeshPro label;


    [Space(10)] [SerializeField] private Color moveColor;
    [SerializeField] private Color rotateColor;
    [SerializeField] private Color scaleColor;
    [SerializeField] private Color colorColor;
    [SerializeField] private Color eventColor;
    [SerializeField] private Color changeActiveStateColor;
    [SerializeField] private Color animationColor;
    [SerializeField] private Color sequenceColor;


    private void Start()
    {
        // meshRenderer = cube.GetComponent<MeshRenderer>();
    }

    public override void UpdateGridPosition()
    {
        transform.localPosition = (Vector3)BehaviourData.GetPosition() +
                                  new Vector3(0, 0.5f, BehaviourData.Time * EditorScaleController.EditorScale);
        transform.localScale = BehaviourData.GetScale() + new Vector3(0.5f, 0.5f, 0.5f);

        UpdateCollisionGroups();

        MaterialPropertyBlock.SetFloat("_ObjectTime", BehaviourData.Time);
        SetRotation(AssignedTrack != null ? AssignedTrack.RotationValue.y : 0);
        UpdateMaterials();
    }

    public static BeatmapBehaviourContainer SpawnBeatmapBehaviour(MapBehaviour behaviourData, ref GameObject behaviourPrefab)
    {
        var container = Instantiate(behaviourPrefab).GetComponent<BeatmapBehaviourContainer>();
        container.BehaviourData = behaviourData;
        return container;
    }

    public void UpdateBehaviour(BehaviourType behaviourType)
    {
        switch (behaviourType)
        {
            case BehaviourType.SequenceTriggerBehaviour:
                label.text = "Sequence";
                meshRenderer.materials[0].SetColor("_Color", sequenceColor);
                break;
            case BehaviourType.MoveBehaviour:
                label.text = "Move";
                meshRenderer.materials[0].SetColor("_Color", moveColor);
                break;
            case BehaviourType.RotateBehaviour:
                label.text = "Rotate";
                meshRenderer.materials[0].SetColor("_Color", rotateColor);
                break;
            case BehaviourType.ScaleBehaviour:
                label.text = "Scale";
                meshRenderer.materials[0].SetColor("_Color", scaleColor);
                break;
            case BehaviourType.ColorBehaviour:
                label.text = "Color";
                meshRenderer.materials[0].SetColor("_Color", colorColor);
                break;
            case BehaviourType.AnimationBehaviour:
                label.text = "Anim";
                meshRenderer.materials[0].SetColor("_Color", animationColor);
                break;
            case BehaviourType.EventBehaviour:
                label.text = "Event";
                meshRenderer.materials[0].SetColor("_Color", eventColor);
                break;
            case BehaviourType.ChangeActiveStateBehaviour:
                label.text = "Active<br>State";
                meshRenderer.materials[0].SetColor("_Color", changeActiveStateColor);
                break;
            default:
                break;
        }
    }
}
