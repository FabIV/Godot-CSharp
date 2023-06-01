using Godot;
using System;

public partial class InputControl : Node
{
	// Called when the node enters the scene tree for the first time.

	private EventBus _eventBus;
	private bool _cameraIsBlocked;

	public override void _Ready()
	{
		_eventBus = GetNode<EventBus>("/root/EventBus");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		float moveRight = Input.GetActionStrength("move_right");
		float moveLeft = Input.GetActionStrength("move_left");
		float moveUp = Input.GetActionStrength("move_up");
		float moveDown = Input.GetActionStrength("move_down");
		
		_eventBus.EmitPlayerMotionData(moveRight - moveLeft, moveUp - moveDown);
		
	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		bool doEmit = false;
		CameraControlData ccd = new CameraControlData();
		if (@event is InputEventMouseMotion)
		{
			
			if (Input.IsActionPressed("mouse_left"))
			{
				InputEventMouseMotion inp = (InputEventMouseMotion)@event;
				ccd.SetPan(-inp.Relative.X);
				ccd.SetTilt(inp.Relative.Y);
				doEmit = true;
			}
		}

		if (Input.IsActionJustPressed("WheelUp"))
		{
			doEmit = true;
			if (Input.IsActionPressed("UpAndDown"))
				ccd.SetOffset(-1.0f);
			else
				ccd.SetDistance(-1.0f);

		}
		else if (Input.IsActionJustPressed("WheelDown"))
		{
			doEmit = true;
			if (Input.IsActionPressed("UpAndDown"))
				ccd.SetOffset(1.0f);
			else
				ccd.SetDistance(1.0f);
		}
		if (doEmit){}
			_eventBus.EmitDeltaCameras(ccd);
		// _eventBus.EmitDeltaCameras(ccd.Distance, ccd.Offset,ccd.Pan, ccd.Tilt);


	}
}
