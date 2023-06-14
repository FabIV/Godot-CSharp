using Godot;
using System;

public partial class EventBus : Node
{
    [Signal] public delegate void PlayerMotionDataEventHandler(float motionX, float motionY);
    [Signal] public delegate void CameraMotionDeltasEventHandler(CameraControlData ccd);
    [Signal] public delegate void DebugMessageEventHandler(string msg, int id);
    [Signal]  public delegate void PlayerIsSetEventHandler(Player player);
    [Signal] public delegate void NewCameraFocusEventHandler(Node3D position);
    [Signal] public delegate void NeedPlayerNodeEventHandler();

    [Signal] public delegate Tween SetTweenSpeedScaleEventHandler(float speedScale);
    
    public void EmitPlayerMotionData(float x, float y)
    {
        EmitSignal(nameof(PlayerMotionData), x, y);
    }

    public void EmitDeltaCameras(CameraControlData ccd)
    {
        EmitSignal(nameof(CameraMotionDeltas), ccd);
    }

    public void EmitDebugMessage(string msg, int nodeID)
    {
        EmitSignal(nameof(DebugMessage), msg, nodeID);
    }

    public void SetMeAsPlayer(Player player)
    {
        EmitSignal(nameof(PlayerIsSet), player);

    }

    public void SetNewCameraFocus(Node3D posistionNode)
    {
        EmitSignal(nameof(NewCameraFocus), posistionNode);
    }

    public void EmitNeedPlayer()
    {
        EmitSignal(nameof(NeedPlayerNode));
    }
}
