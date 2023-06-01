using Godot;
using System;
using RPG3D.General;

public partial class Player : RigidBody3D
{
	[Export] private Enums.CharStyle PlayerType = Enums.CharStyle.None;
	public override void _Ready()
	{
		EventBus eventBus = GetNode<EventBus>("/root/EventBus");
		eventBus.PlayerMotionData += MotionFunction;

	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void MotionFunction(float x, float y)
	{
	}
}
