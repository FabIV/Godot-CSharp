using Godot;
public partial class CameraControlDataDelta: CameraControlData
{
    private Limits _distanceBound;
    private Limits _tiltBound;
    private Limits _offsetBound;

    private Vector2 _floorOffset;
    public Vector2 FloorOffset => _floorOffset;
    private Vector3 _worldPos;
    public Vector3 WorldPosition => _worldPos;
    
    public CameraControlDataDelta(float minDist, float maxDist, float minOffset, float maxOffset, float minTilt, float maxTilt)
    {
        _distanceBound = new Limits(minDist, maxDist);
        _offsetBound = new Limits(minOffset, maxOffset);
        _tiltBound = new Limits(minTilt, maxTilt);
        _floorOffset = new Vector2(0.0f, 0.0f);
        _worldPos = new Vector3(0.0f, 0.0f, 0.0f);
    }

    public void SetPositioning(float distance, float offset, float pan, float tilt, Vector2 floorOffset, Vector3 worldPos)
    {
        this.SetDistance(distance);
        this.SetOffset(offset);
        this.SetPan(pan);
        this.SetTilt(tilt);
        SetFloorOffset(floorOffset);
        SetWorldPos(worldPos);
    }

    public void SetWorldPos(Vector3 pos)
    {
        _worldPos.X = pos.X;
        _worldPos.Y = pos.Y;
        _worldPos.Z = pos.Z;
    }

    public void SetFloorOffset(Vector2 offset)
    {
        _floorOffset.X = offset.X;
        _floorOffset.Y = offset.Y;
    }
    
    public void SetFloorOffset(float x, float y)
    {
        _floorOffset.X = x;
        _floorOffset.Y = y;
    }

    public void RotateFloorOffsetDeg(float rad)
    {
        _floorOffset.RotateBy(rad);
    }
    
    public void RotateFloorOffset(float[,] matrix)
    {
        _floorOffset.RotateByMatrix(matrix);
    }

    public void SetTargetFloorOffset(float x, float y)
    {
        SetFloorOffset(x , y);
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