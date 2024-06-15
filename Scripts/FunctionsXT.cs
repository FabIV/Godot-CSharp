namespace Pixelator;
using Godot;
using System;


public static class FunctionsFloat
{
    public static float Rad(this float val)
    {
        return val * Mathf.Pi / 180.0f;
    }
}

public static class FunctionsVector2
    {

        public static Vector2 RotateBy(this ref Vector2 val, float angle)
        {
            float[,] rM = val.RotationMatrix(angle);
            Vector2 newValues = new Vector2(rM[0,0] * val.X + rM[0,1] * val.Y, rM[1,0] * val.X + rM[1,1] * val.Y);

            val.X = newValues.X;
            val.Y = newValues.Y;
            
            return newValues;
        }
        
        public static Vector2 RotateByDeg(this ref Vector2 val, float angle)
        {
            Vector2 newValues = val.RotateBy(angle / 180.0f * Mathf.Pi);

            return newValues;
        }
        
        public static float[,] RotationMatrix(this Vector2 inp, float angle)
        {
            float[,] result = new float[2, 2];
            result[0, 0] = MathF.Cos(angle);
            result[0, 1] = -MathF.Sin(angle);
            result[1, 0] = -result[0, 1];
            result[1, 1] = result[0, 0];
            return result;
        }

        public static float Abs(this float val)
        {
            return Mathf.Abs(val);
        }
        
        public static float Sign(this float val)
        {
            return Mathf.Sign(val);
        }
        public static float Max(this float val, float val2)
        {
            return Mathf.Max(val, val2);
        }
        
        public static float Min(this float val, float val2)
        {
            return Mathf.Min(val, val2);
        }
    }