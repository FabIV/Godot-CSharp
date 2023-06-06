using Godot;
using System;
using RPG3D.General;

public partial class Player : RigidBody3D
{
	[Export] private Enums.CharStyle PlayerType = Enums.CharStyle.None;

	private GameStatus _gameStatus;
	public override void _Ready()
	{
		EventBus eventBus = GetNode<EventBus>("/root/EventBus");
		eventBus.PlayerMotionData += MotionFunction;
		_gameStatus = GetNode<GameStatus>("/root/GameStatus");

	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void MotionFunction(float x, float y)
	{
		Vector2 direction = new Vector2(x, -y);
		direction = direction.Normalized();
		direction.RotateByMatrix(_gameStatus.FixedCameraRotationMatrix);
		this.ApplyCentralForce(new Vector3(direction.X,0.0f, direction.Y) * 40.0f);
	}
}
