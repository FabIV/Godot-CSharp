using Godot;

public static class ClassExtentions
{
    public static float Abs(this float val)
    {
        return Mathf.Abs(val);
    }
    
    public static float Sign(this float val)
    {
        return Mathf.Sign(val);
    }
    public static float Min(this float val, float min)
    {
        return Mathf.Min(val, min);
    }
    
    public static float Max(this float val, float min)
    {
        return Mathf.Max(val, min);
    }
    
    public static float Sqrt(this float val)
    {
        return Mathf.Sqrt(val);
    }
    
    public static float Square(this float val)
    {
        return Mathf.Pow(val, 2);
    }
    public static float Cube(this float val)
    {
        return Mathf.Pow(val, 3);
    }

    public static float DegToRad(this float val)
    {
        return Mathf.DegToRad(val);
    }

    public static float RadToDeg(this float val)
    {
        return Mathf.RadToDeg(val);
    }

    public static float LerpToZero(this float val)
    {
        return Mathf.Lerp(val, 0.0f, 0.5f);
    }
    public static float LerpToZero(this float val, float weight)
    {
        return Mathf.Lerp(val, 0.0f, weight);
    }
}