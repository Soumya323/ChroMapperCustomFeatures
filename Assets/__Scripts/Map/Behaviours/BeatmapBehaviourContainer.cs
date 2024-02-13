using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BeatmapBehaviourContainer : BeatmapObjectContainer
{
    [SerializeField] private GameObject connectingPole;
    public MapBehaviour BehaviourData;
    public BehavioursContainer BehavioursContainer;

    public override BeatmapObject ObjectData { get => BehaviourData; set => BehaviourData = (MapBehaviour)value; }

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

    public static BeatmapBehaviourContainer SpawnBeatmapBehaviour(BehavioursContainer behavioursContainer, MapBehaviour behaviourData,
        ref GameObject behaviourPrefab)
    {
        var container = Instantiate(behaviourPrefab).GetComponent<BeatmapBehaviourContainer>();
        container.BehaviourData = behaviourData;
        container.BehavioursContainer = behavioursContainer;
        return container;
    }

    public void UpdateBehaviour(BehaviourType behaviourType, bool _isInitiating)
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

        if (BehaviourData.LineLayer < 0)
        {
            BehavioursContainer.DeleteObject(BehaviourData);
        }
        else
        {
            Invoke(nameof(UpdateConnectingPole), 0.2f);
        }
    }

    public void UpdateConnectingPole()
    {
        connectingPole.SetActive(BehaviourData.LineLayer > 0);
    }

    public void ChangeLineLayerTo(int _layer)
    {
        BehaviourData.LineLayer = _layer;
        transform.localPosition = new Vector3(transform.localPosition.x, _layer + 0.5f, transform.localPosition.z);
    }
}
