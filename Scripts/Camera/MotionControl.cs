using Godot;
using System;
using Pixelator;

public partial class MotionControl : Node
{
	[Export] public float CameraSpeed = 100f;
	[Export] public float CameraRotationSpeed = 5f;
	public MotionMode MotionMode { get; private set; }
	private Camera2DSystem _camera;
	
	private SystemControl _system;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		MotionMode = MotionMode.FreeControl;
		_system = GetParent<SystemControl>();
		_system.SetMotionControl(this);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		float horizontal = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
		float vertical = Input.GetActionStrength("move_up") - Input.GetActionStrength("move_down");

		float action = Input.GetActionStrength("UpAndDown");
		// float action = Input.GetActionStrength("Ctrl");
		bool actionJustPressed = Input.IsActionJustPressed("UpAndDown");
		// bool actionJustPressed = Input.IsActionJustPressed("Ctrl");
		
		float shift = Input.GetActionStrength("Shift");
		
		float fDelta = (float)delta;
		if (MotionMode == MotionMode.FreeControl)
		{

			if (shift > 0.5f)
			{
				if(Input.IsActionJustPressed("move_left"))
					_system.AddToTargetRotation(MathF.PI/4.0f);
				if(Input.IsActionJustPressed("move_right"))
					_system.AddToTargetRotation(-MathF.PI/4.0f);
				
			}
			else
			{
				_camera.MoveLocalX(horizontal * CameraSpeed * fDelta * _system.PixelScale);
				_camera.MoveLocalY(-vertical * CameraSpeed * fDelta * _system.PixelScale);
				// _system.HardOverwriteCamera(horizontal * CameraSpeed * fDelta * _system.PixelScale, 
				// 	-vertical * CameraSpeed * fDelta * _system.PixelScale);
			}

			
		}
	}

	public void Set2DCamera(Camera2DSystem cam)
	{
		_camera = cam;
	}
	
}
