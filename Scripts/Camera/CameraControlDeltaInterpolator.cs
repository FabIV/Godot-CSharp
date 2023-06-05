using System;
using Godot;

public partial class CameraControlDeltaInterpolator: CameraControlData
{
    private Vector2 _floorOffset;
    public Vector2 FloorOffset => _floorOffset;
    public CameraControlDeltaInterpolator(CameraControlData ccd, Vector2 currentFloorOffset, CameraControlDataDelta target)
    {
        SetDistance(target.Distance - ccd.Distance);
        SetOffset(target.Offset - ccd.Offset);
        SetPan(target.Pan - ccd.Pan);
        SetTilt(target.Tilt - ccd.Tilt);
        SetFloorOffset(target.FloorOffset.X - currentFloorOffset.X, target.FloorOffset.Y - currentFloorOffset.Y);
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
    }
}