using Godot;
using System;

public partial class CameraTilt : Node3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CameraControl cc = GetOwner<CameraControl>();
		cc.SetCameraControlNode(this);
	}
}
