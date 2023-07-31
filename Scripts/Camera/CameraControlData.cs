using Godot;
using System;

public partial class CameraControlData : GodotObject //muss vereerbt werden, da sonst die signals nicht funktionieren
{
    private float _distance;
    public float Distance => _distance;
    private float _offset;
    public float Offset => _offset;
    private float _pan;
    public float Pan => _pan;
    private float _tilt;
    public float Tilt => _tilt;

    public CameraControlData()
    { }
    public CameraControlData(float distance, float offset, float pan, float tilt)
    {
        _distance = distance;
        _offset = offset;
        _pan = pan;
        _tilt = tilt;
    }

	public void SetDistance(float distance) => _distance = distance;

	public void SetOffset(float offset) => _offset = offset;

	public void SetPan(float pan) => _pan = pan;

	public void SetTilt(float tilt) => _tilt = tilt;

}
