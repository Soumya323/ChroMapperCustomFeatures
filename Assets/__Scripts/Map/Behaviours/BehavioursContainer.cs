using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class BehavioursContainer : BeatmapObjectContainerCollection, CMInput.IBehaviourGridActions
{
    [SerializeField] private GameObject behaviourPrefab;

    [SerializeField] private BehaviourAppearanceSO behaviourAppearanceSo;
    [SerializeField] private TracksManagerRight tracksManagerRight;
    [SerializeField] private CreateBehaviourLanesLabels labels;
    [SerializeField] private CountersPlusController countersPlus;

    public List<MapEvent> AllRotationEvents = new List<MapEvent>();

    private int currentPage = 0;
    private const int maxPage = 10;

    public override BeatmapObject.ObjectType ContainerType => BeatmapObject.ObjectType.Behaviour;


    public override BeatmapObjectContainer CreateContainer()
    {
        BeatmapObjectContainer con = BeatmapBehaviourContainer.SpawnBeatmapBehaviour(null, ref behaviourPrefab);
        return con;
    }

    internal override void SubscribeToCallbacks()
    {
        SpawnCallbackController.NotePassedThreshold += SpawnCallback;
        SpawnCallbackController.RecursiveNoteCheckFinished += RecursiveCheckFinished;
        DespawnCallbackController.NotePassedThreshold += DespawnCallback;
        AudioTimeSyncController.PlayToggle += OnPlayToggle;
    }

    internal override void UnsubscribeToCallbacks()
    {
        SpawnCallbackController.NotePassedThreshold -= SpawnCallback;
        SpawnCallbackController.RecursiveNoteCheckFinished += RecursiveCheckFinished;
        DespawnCallbackController.NotePassedThreshold -= DespawnCallback;
        AudioTimeSyncController.PlayToggle -= OnPlayToggle;
    }

    private void SpawnCallback(bool initial, int index, BeatmapObject objectData)
    {
        if (!LoadedContainers.ContainsKey(objectData)) CreateContainerFromPool(objectData);
    }

    //We don't need to check index as that's already done further up the chain
    private void DespawnCallback(bool initial, int index, BeatmapObject objectData)
    {
        if (LoadedContainers.ContainsKey(objectData)) RecycleContainer(objectData);
    }

    private void RecursiveCheckFinished(bool natural, int lastPassedIndex) => RefreshPool();

    private void OnPlayToggle(bool isPlaying)
    {
        if (!isPlaying) RefreshPool();
    }

    public void OnCyclePageUp(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        currentPage++;
        if (!(currentPage < maxPage)) currentPage = 0;

        labels.UpdateLabels(currentPage);
    }

    public void OnCyclePageDown(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        currentPage--;
        if (currentPage < 0) currentPage = (maxPage - 1);

        labels.UpdateLabels(currentPage);
    }

    protected override void UpdateContainerData(BeatmapObjectContainer con, BeatmapObject obj)
    {
        var behaviour = con as BeatmapBehaviourContainer;
        var behaviourData = obj as MapBehaviour;

        if (behaviour != null && behaviourData != null)
            behaviour.UpdateBehaviour(behaviourData.Type);
    }

    protected override void OnObjectSpawned(BeatmapObject obj)
    {
        if (obj is MapEvent e)
        {
            if (e.IsRotationEvent)
                AllRotationEvents.Add(e);
        }
        
        countersPlus.UpdateStatistic(CountersPlusStatistic.Behaviours);
    }

    protected override void OnObjectDelete(BeatmapObject obj)
    {
        if (obj is MapEvent e)
        {
            if (e.IsRotationEvent)
            {
                AllRotationEvents.Remove(e);
                tracksManagerRight.RefreshTracks();
            }
        }
        countersPlus.UpdateStatistic(CountersPlusStatistic.Behaviours);
    }

    protected override void OnContainerSpawn(BeatmapObjectContainer container, BeatmapObject obj)
    {
    }

    protected override void OnContainerDespawn(BeatmapObjectContainer container, BeatmapObject obj)
    {
    }
}
