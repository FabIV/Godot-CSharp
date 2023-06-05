using Godot;
public partial class CameraControlDataDelta: CameraControlData
{
    private Limits _distanceBound;
    private Limits _tiltBound;
    private Limits _offsetBound;

    public CameraControlDataDelta(float minDist, float maxDist, float minOffset, float maxOffset, float minTilt, float maxTilt)
    {
        _distanceBound = new Limits(minDist, maxDist);
        _offsetBound = new Limits(minOffset, maxOffset);
        _tiltBound = new Limits(minTilt, maxTilt);
    }

    public void SetPositioning(float distance, float offset, float pan, float tilt)
    {
        this.SetDistance(distance);
        this.SetOffset(offset);
        this.SetPan(pan);
        this.SetTilt(tilt);
    }

    public void AddTargetDistance(float deltaDist)
    {
        this.SetDistance(_distanceBound.GetValidValue(Distance+deltaDist));
    }
    
    public void AddTargetOffset(float deltaOffset)
    {
        this.SetOffset(_offsetBound.GetValidValue(Offset+deltaOffset));
    }
    public void AddTargetTilt(float deltaTilt)
    {
        this.SetTilt(_tiltBound.GetValidValue(Tilt+deltaTilt));
    }

    public void AddTargetPan(float deltaPan)
    {
        this.SetPan(Pan + deltaPan);
    }
    
}