using UnityEngine;

public class SequenceContainer : BeatmapObjectContainerCollection
{
    [SerializeField] private GameObject sequencePrefab;
    [SerializeField] private SequenceAppearanceSO sequenceAppearanceSo;
    [SerializeField] private TracksManagerRight tracksManagerRight;
    [SerializeField] private CountersPlusController countersPlus;

    public override BeatmapObject.ObjectType ContainerType => BeatmapObject.ObjectType.Sequence;

    internal override void SubscribeToCallbacks()
    {
        Shader.SetGlobalFloat("_OutsideAlpha", 0.25f);
        AudioTimeSyncController.PlayToggle += OnPlayToggle;
    }

    internal override void UnsubscribeToCallbacks() => AudioTimeSyncController.PlayToggle -= OnPlayToggle;

    private void OnPlayToggle(bool playing) => Shader.SetGlobalFloat("_OutsideAlpha", playing ? 0 : 0.25f);

    public void UpdateColor(Color sequence) => sequenceAppearanceSo.DefaultSequenceColor = sequence;


    protected override void OnObjectSpawned(BeatmapObject _) =>
        countersPlus.UpdateStatistic(CountersPlusStatistic.Sequence);

    protected override void OnObjectDelete(BeatmapObject _) =>
        countersPlus.UpdateStatistic(CountersPlusStatistic.Sequence);

    public override BeatmapObjectContainer CreateContainer() =>
        BeatmapSequenceContainer.SpawnSequence(null, tracksManagerRight, ref sequencePrefab);

    protected override void UpdateContainerData(BeatmapObjectContainer con, BeatmapObject obj, bool isPasted = false)
    {
        var sequence = con as BeatmapSequenceContainer;
        if (!sequence.IsRotatedByNoodleExtensions)
        {
            var track = tracksManagerRight.GetTrackAtTime(obj.Time);
            track.AttachContainer(con);
        }

        sequenceAppearanceSo.SetSequenceAppearance(sequence);
    }
}
