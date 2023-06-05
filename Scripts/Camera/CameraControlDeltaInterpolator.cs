using System;
using Godot;

public partial class CameraControlDeltaInterpolator: CameraControlData
{
    public CameraControlDeltaInterpolator(CameraControlData ccd, CameraControlDataDelta target)
    {
        SetDistance(target.Distance - ccd.Distance);
        SetOffset(target.Offset - ccd.Offset);
        SetPan(target.Pan - ccd.Pan);
        SetTilt(target.Tilt - ccd.Tilt);
    }

    public void InterpolatePositions(float weightRot, float weightTrans)
    {
        //TODO function for interpolation
        SetDistance(Distance.LerpToZero(weightTrans));
        SetOffset(Offset.LerpToZero(weightTrans));
        SetPan(Pan.LerpToZero(weightRot));
        SetTilt(Tilt.LerpToZero(weightRot));
    }
}