public class SequenceBounds
{
    public SequenceBounds(float position)
    {
        Width = 1;
        Height = 0.0f;
        Position = position;
        StartHeight = 0;
    }

    public float Width { get; }
    public float Height { get; }
    public float Position { get; }
    public float StartHeight { get; }
}
