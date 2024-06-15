using Godot;
using System;

public partial class SubCameraSystem : SubViewport
{
	private Node3D _position;

	private Node3D _rotation;

	private Node3D _pan;

	private Node3D _distance;

	private Camera3DSystem _camera;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_position = this.GetChild<Node3D>(0);
		_rotation = _position.GetChild<Node3D>(0);
		_pan = _rotation.GetChild<Node3D>(0);
		_distance = _pan.GetChild<Node3D>(0);
		_camera = _distance.GetChild<Camera3DSystem>(0);
		
		_camera.Size = 8.0f * this.Size.X / 512.0f;
		
		
		

		Node3D world = GetParent<Node3D>();
		SystemControl sys = world.GetParent<SystemControl>();
		sys.AddCameraSystem(this);

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void SetPosition(Vector3 newPos)
	{
		_position.Position = newPos;

	}

	public void SetRotation(float angle)
	{
		_rotation.RotateY(angle - _rotation.Rotation.Y);
		
	}
}
