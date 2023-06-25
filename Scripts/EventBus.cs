using Godot;
using System;

public partial class EventBus : Node
{
    [Signal] public delegate void PlayerMotionDataEventHandler(float motionX, float motionY);
    public void EmitPlayerMotionData(float x, float y)
    {
        EmitSignal(nameof(PlayerMotionData), x, y);
    }
    
    [Signal] public delegate void CameraMotionDeltasEventHandler(CameraControlData ccd);
    public void EmitDeltaCameras(CameraControlData ccd)
    {
        EmitSignal(nameof(CameraMotionDeltas), ccd);
    }
    
    [Signal] public delegate void DebugMessageEventHandler(string msg, string id);
    public void EmitDebugMessage(string msg, string nodeID)
    {
        EmitSignal(nameof(DebugMessage), msg, nodeID);
    }
    
    [Signal]  public delegate void PlayerIsSetEventHandler(Player player);
    public void EmitSetMeAsPlayer(Player player)
    {
        EmitSignal(nameof(PlayerIsSet), player);
    }
    
    [Signal] public delegate void NewCameraFocusEventHandler(Node3D position);
    public void EmitSetNewCameraFocus(Node3D posistionNode)
    {
        EmitSignal(nameof(NewCameraFocus), posistionNode);
    }
    
    
    [Signal] public delegate void NeedPlayerNodeEventHandler();
    public void EmitNeedPlayer()
    {
        EmitSignal(nameof(NeedPlayerNode));
    }
    
    [Signal] public delegate Tween SetTweenSpeedScaleEventHandler(float speedScale);

    public void EmitSetTweenSpeedScale(float speedScale)
    {
        EmitSignal(nameof(SetTweenSpeedScale), speedScale);
    }

    public void MakeTweenTimeIndependend(Tween tween)
    {
        SetTweenSpeedScale += tween.SetSpeedScale;
        
    }
    // [Signal] public delegate void MissingTimeDeltaEventHandler(double dt);
    //
    // public void EmitMissingTimeDelta(double dt)
    // {
    //     EmitSignal(nameof(MissingTimeDelta), dt);
    // }
}
