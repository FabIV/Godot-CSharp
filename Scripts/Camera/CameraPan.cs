using Godot;
using System;

public partial class CameraPan : Node3D
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        CameraControl cc = GetOwner<CameraControl>();
        cc.SetCameraPanNode(this);
    }
}