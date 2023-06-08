using Godot;
using System;

public partial class EventBus : Node
{
    [Signal] public delegate void PlayerMotionDataEventHandler(float motionX, float motionY);
    [Signal] public delegate void CameraMotionDeltasEventHandler(CameraControlData ccd);
    [Signal] public delegate void DebugMessageEventHandler(string msg, int id);
    [Signal]  public delegate void PlayerIsSetEventHandler(Player player);
    [Signal] public delegate void NewCameraFocusEventHandler(Vector3 position);
    [Signal] public delegate void NeedPlayerNodeEventHandler();
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

    public void SetNewCameraFocus(Vector3 pos)
    {
        EmitSignal(nameof(NewCameraFocus), pos);
    }

    public void EmitNeedPlayer()
    {
        EmitSignal(nameof(NeedPlayerNode));
    }
}
