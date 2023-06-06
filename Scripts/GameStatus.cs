using Godot;
using System;

public partial class GameStatus : Node
{
    private float[,] _fixedCameraRotationMatrix;
    public float[,] FixedCameraRotationMatrix => _fixedCameraRotationMatrix;

    public GameStatus()
    {
        _fixedCameraRotationMatrix = new float[2,2];
        SetCameraRotationMatrix(0.0f);
    }
    
    public void SetCameraRotationMatrix(float angle)
    {
        _fixedCameraRotationMatrix[0, 0] =  angle.Cos();
        _fixedCameraRotationMatrix[0, 1] = -(angle.Sin());
        _fixedCameraRotationMatrix[1, 0] = -_fixedCameraRotationMatrix[0, 1];
        _fixedCameraRotationMatrix[1, 1] = _fixedCameraRotationMatrix[0, 0];
    }
    //
    public void SetCameraRotationMatrixDeg(float angle)
    {
        SetCameraRotationMatrix(angle.DegToRad());
    }
    

}