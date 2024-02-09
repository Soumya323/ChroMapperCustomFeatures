using System.Collections.Generic;
using UnityEngine;

public class BehaviourPlacement : PlacementController<MapBehaviour, BeatmapBehaviourContainer, BehavioursContainer>
{
    [SerializeField] private BehaviourAppearanceSO behaviourAppearanceSo;
    [SerializeField] private CreateBehaviourLanesLabels labels;

    internal BehaviourType behaviourType = BehaviourType.None;

    public override MapBehaviour GenerateOriginalData() => new MapBehaviour(0, 0, 0, BehaviourType.None);

    public override BeatmapAction GenerateAction(BeatmapObject spawned, IEnumerable<BeatmapObject> conflicting) =>
        new BeatmapObjectPlacementAction(spawned, conflicting, "Placed an Behaviour.");

    public override void OnPhysicsRaycast(Intersections.IntersectionHit hit, Vector3 transformedPoint)
    {
        var trns = instantiatedContainer.transform;

        if (trns.localPosition.y < 0.5f)
            trns.localPosition = new Vector3(trns.localPosition.x, 0.5f, trns.localPosition.z);

        queuedData.LineIndex = Mathf.RoundToInt(trns.localPosition.x + 1.5f);
         queuedData.LineLayer = Mathf.RoundToInt(trns.localPosition.y - 0.5f);
    }

    public override void TransferQueuedToDraggedObject(ref MapBehaviour dragged, MapBehaviour queued)
    {
        dragged.Time = queued.Time;
        dragged.Type = queued.Type;
        dragged.LineIndex = queued.LineIndex;
        dragged.LineLayer = queued.LineLayer;
        dragged.ID = queued.ID;
    }

    public void UpdateType(BehaviourType type)
    {
        queuedData.Type = type;
        behaviourType = type;
    }
}
