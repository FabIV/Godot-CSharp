public class Limits
{
    public float Min { get; }
    public float Max { get; }

    public Limits(float min, float max)
    {
        Min = min;
        Max = max;
    }

	public float GetValidValue(float input) => input.Max(this.Min).Min(this.Max);

}