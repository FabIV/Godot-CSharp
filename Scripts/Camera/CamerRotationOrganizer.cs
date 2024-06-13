using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using Godot;


namespace Pixelator;

public class CamerRotationOrganizer
{
    public float TargetRotation { get; private set; }
    public float Rotation { get; private set; }
    private float _previousRotation;
    public Vector2 Origin { get; private set; }

    private Vector2 _xPart;
    private Vector2 _yPart;

    public Vector2 XPart => _xPart;
    public Vector2 YPart => _yPart;

    private float _pixelFactorY;

    private const float _circle = Mathf.Pi * 2.0f;
    private const float _twoCircle = Mathf.Pi * 4.0f;

    public CamerRotationOrganizer()
    {
        _previousRotation = -1.0f;
        Origin = new Vector2(0.0f, 0.0f);
        UpdateRotationData(0.0f, new Vector2(0.0f,0.0f));
        _pixelFactorY = 2.0f;
    }

    public void HardOverwriteOrigin(Vector2 val)
    {
        Origin = val;
    }

    public void AddToTargetRotation(float val)
    {
        TargetRotation += val;
        CompensateTooBigAnglesDifferences();
    }

    private void CompensateTooBigAnglesDifferences()
    {
        if (TargetRotation > Rotation + _circle)
            TargetRotation -= _circle;
        else if (TargetRotation <= Rotation - _circle)
            TargetRotation += _circle;
    }

    public float GetNewCameraAngle( float deltaTime, float speed)
    {
        var deltaAngles = (TargetRotation - Rotation);
        var finalDelta = (speed * deltaTime).Min(deltaAngles.Abs()) * deltaAngles.Sign();
        var newAngle = finalDelta + Rotation;
        
        return newAngle;
        
    }
    
    public void SetPixelFactorY(float val)
    {
        _pixelFactorY = val;
    }

    public void UpdateRotationData(float angle, Vector2 relCameraWorldPos)
    {
        var initRelCameraWorldPos = new Vector2(relCameraWorldPos.X, relCameraWorldPos.Y);
        var absWorldRotationPoint = initRelCameraWorldPos.RotateBy(Rotation) + Origin;

        relCameraWorldPos.RotateBy(angle);

        Origin = absWorldRotationPoint - relCameraWorldPos;
        
        var currentRotationPoint = new Vector2(0.0f, 0.0f);
        Rotation = angle;
        
        UpdateInternalValues(relCameraWorldPos);
        _previousRotation = Rotation;
    }

    public void CompensateTooBigAngles()
    {
        if (Rotation > _twoCircle)
        {
            Rotation -= _circle;
            TargetRotation -= _circle;
        }
        else if (Rotation < _twoCircle)
        {
            Rotation += _circle;
            TargetRotation += _circle;
        }
        
    }
    
    private void UpdateInternalValues(Vector2 rotationPoint)
    {
        _xPart = new Vector2(1.0f, 0.0f);
        _xPart.RotateBy(Rotation);

        _yPart = new Vector2(0.0f, 1.0f);
        _yPart.RotateBy(Rotation);
    }
    
    
}