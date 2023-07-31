using Godot;
using System;
using RPG3D.General;

public partial class InputControl : Node
{
	// Called when the node enters the scene tree for the first time.

	private EventBus _eventBus;
	private EventBusMenu _eventBusMenu;
	private GameStatus _gameStatus;
	private bool _cameraIsBlocked;

	private InputContainer _latestInput;

	public InputControl()
	{
		_latestInput = new InputContainer(0.0f, 0.0f);
	}

	public override void _Ready()
	{
		_eventBus = GetNode<EventBus>("/root/EventBus");
		_eventBusMenu = GetNode<EventBusMenu>("/root/EventBusMenu");
		_gameStatus = GetNode<GameStatus>("/root/GameStatus");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_gameStatus.InputMotion == Enums.InputMotion.InputAllowed)
		{
			float moveRight = Input.GetActionStrength("move_right");
			float moveLeft = Input.GetActionStrength("move_left");
			float moveUp = Input.GetActionStrength("move_up");
			float moveDown = Input.GetActionStrength("move_down");
			_latestInput.SetValues(moveRight - moveLeft, moveUp - moveDown);
		}

		_eventBus.EmitPlayerMotionData(_latestInput.Horizontal, _latestInput.Vertical);

		if (Input.IsActionJustPressed("pause_menu"))
		{
			if (_gameStatus.Status == Enums.GameStatus.Normal)
				_eventBusMenu.EmitUIBack(false);
			else if(_gameStatus.Status == Enums.GameStatus.Menu)
				_eventBusMenu.EmitUIBack(true);
		}
	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		if (_gameStatus.Status == Enums.GameStatus.Normal)
			HandleMouseControl(@event);
	}

	private void HandleMouseControl(InputEvent @event)
	{
		bool doEmit = false;
		CameraControlData ccd = new CameraControlData();
		if (@event is InputEventMouseMotion motion)
		{
			
			if (Input.IsActionPressed("mouse_left"))
			{
				InputEventMouseMotion inp = motion;
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
	}
}
