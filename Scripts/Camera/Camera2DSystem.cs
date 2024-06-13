using Godot;
using System;

public partial class Camera2DSystem : Camera2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SystemControl sys = GetParent<SystemControl>();
		sys.SetCamera2D(this);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
