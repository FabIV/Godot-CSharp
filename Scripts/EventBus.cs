using Godot;
using System;

public partial class EventBus : Node
{
    [Signal] 
    public delegate void PlayerMotionDataEventHandler(float motionX, float motionY);
    
    [Signal] 
    public delegate void CameraMotionDeltasEventHandler(CameraControlData ccd);
    // public delegate void CameraMotionDeltasEventHandler(float deltaDistance, float deltaOffset, float deltaPan, float deltaTilt);
    public void EmitPlayerMotionData(float x, float y)
    {
        EmitSignal(nameof(PlayerMotionData), x, y);
    }

    // public void EmitDeltaCameras(float deltaDistance, float deltaOffset, float deltaPan, float deltaTilt)
    public void EmitDeltaCameras(CameraControlData ccd)
    {
        EmitSignal(nameof(CameraMotionDeltas), ccd);
        // EmitSignal(nameof(CameraMotionDeltas), deltaDistance, deltaOffset, deltaPan, deltaTilt);
    }
}
