using System;
using Godot;

public partial class CameraControlDeltaInterpolator: CameraControlData
{
    private Vector2 _floorOffset;
    public Vector2 FloorOffset => _floorOffset;
    private Vector3 _worldPosition;
    public Vector3 WorldPosition => _worldPosition;
    public CameraControlDeltaInterpolator(CameraControlData ccd, 
                                            Vector2 currentFloorOffset, 
                                            Vector3 currentWorldPos,
                                            CameraControlDataDelta target)
    {
        SetDistance(target.Distance - ccd.Distance);
        SetOffset(target.Offset - ccd.Offset);
        SetPan(target.Pan - ccd.Pan);
        SetTilt(target.Tilt - ccd.Tilt);
        SetFloorOffset(target.FloorOffset.X - currentFloorOffset.X, target.FloorOffset.Y - currentFloorOffset.Y);
        SetWorldPosition(target.WorldPosition.X - currentWorldPos.X, 
                            target.WorldPosition.Y - currentWorldPos.Y,
                            target.WorldPosition.Z - currentWorldPos.Z);
    }

    public void SetWorldPosition(float x, float y, float z)
    {
        _worldPosition.X = x;
        _worldPosition.Y = y;
        _worldPosition.Z = z;
    }

    public void SetFloorOffset(float x, float y)
    {
        _floorOffset.X = x;
        _floorOffset.Y = y;
    }

    public void InterpolatePositions(float weightRot, float weightTrans, float weightFloor)
    {
        //TODO function for interpolation
        SetDistance(Distance.LerpToZero(weightTrans));
        SetOffset(Offset.LerpToZero(weightTrans));
        SetPan(Pan.LerpToZero(weightRot));
        SetTilt(Tilt.LerpToZero(weightRot));
        SetFloorOffset(_floorOffset.X.LerpToZero(weightFloor), _floorOffset.Y.LerpToZero(weightFloor));
        SetWorldPosition(_worldPosition.X.LerpToZero(weightTrans), _worldPosition.Y.LerpToZero(weightTrans),
                            _worldPosition.Z.LerpToZero(weightTrans));
    }
}