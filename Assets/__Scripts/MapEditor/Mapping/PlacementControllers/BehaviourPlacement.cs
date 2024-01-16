using System.Collections.Generic;
using UnityEngine;

public class BehaviourPlacement : PlacementController<MapBehaviour, BeatmapBehaviourContainer, BehavioursContainer>
{
    [SerializeField] private BehaviourAppearanceSO behaviourAppearanceSo;
    [SerializeField] private CreateBehaviourLanesLabels labels;

    internal BehaviourType behaviourType = BehaviourType.None;
    
    public override MapBehaviour GenerateOriginalData() => new MapBehaviour(0, 0, 0,BehaviourType.None);

    public override BeatmapAction GenerateAction(BeatmapObject spawned, IEnumerable<BeatmapObject> conflicting) =>
        new BeatmapObjectPlacementAction(spawned, conflicting, "Placed an Behaviour.");

    public override void OnPhysicsRaycast(Intersections.IntersectionHit hit, Vector3 transformedPoint)
    {
        queuedData.LineIndex = Mathf.RoundToInt(instantiatedContainer.transform.localPosition.x + 1.5f);
        queuedData.LineLayer = Mathf.RoundToInt(instantiatedContainer.transform.localPosition.y - 0.5f);
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
