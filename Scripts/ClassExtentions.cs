using System;
using Godot;

public static class ClassExtentionsFloat
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
    public static float Sin(this float val)
    {
        return Mathf.Sin(val);
    }
    
    public static float Cos(this float val)
    {
        return Mathf.Cos(val);
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

public static class ClassExtentionsVector2
{
    public static void RotateBy(this ref Vector2 val, float angle)
    {
        float sinB = Mathf.Sin(angle);
        float cosB = Mathf.Cos(angle);
        float x = cosB * val.X - sinB * val.Y;
        val.Y = sinB * val.X + cosB * val.Y;
        val.X = x;
    }

    public static void RotateByMatrix(this ref Vector2 val, float[,] matrix)
    {
        float x = matrix[0, 0] * val.X - matrix[0, 1] * val.Y;
        val.Y = -matrix[1, 0] * val.X + matrix[1, 1] * val.Y;
        val.X = x;
    }

    public static void RotateByMatrix(this ref Vector2 val, float[] matrix)
    {
        float x = matrix[0] * val.X - matrix[1] * val.Y;
        val.Y = -matrix[1] * val.X + matrix[0] * val.Y;
        val.X = x;
    }
    
}