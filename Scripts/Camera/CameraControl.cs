using Godot;
using RPG3D.General;


public partial class CameraControl : Node3D
{
	[Export] private float _mouseSensitivity = 0.5f;
	[Export] private float _sStepsOffset = 0.5f;
	[Export] private float _stepDistance = 2.5f;
	[Export] private int _minDistance = 5;
	[Export] private int _maxDistance = 30;
	[Export] private int _minOffset;
	[Export] private int _maxOffset = 3;	
	[Export] private int _minTilt = -80;
	[Export] private int _maxTilt = -5;
	[Export] private float _lerpFactorDistance = 0.9f;
	[Export] private float _lerpFactorRotation = 0.6f;
	[Export] private float _lerpFactorFloor = 0.95f;
	[Export] private float _cameraFloorOffset = 2.0f;

	private Node3D _targetWorldPosNode;
	private CameraControlData _currentCcd;
	private Vector2 _currentFloorOffset;
	private CameraControlDataDelta _targetCcd;
	
	// Called when the node enters the scene tree for the first time.
	private CameraFloor _floorNode;
	private CameraPan _panNode;
	private CameraOffset _offsetNode;
	private CameraDistance _distanceNode;
	private CameraTilt _tiltNode;

	private float _targetPan;
	private float _targetTilt;
	private float _targetDistance;
	private float _targetOffset;

	private GameStatus _gameStatus;
	private EventBus _eventBus;

	public CameraControl()
	{
		_currentFloorOffset = new Vector2(0.0f, 0.0f);
		InitializeControlData(13.0f, 0.0f, 0.0f, -30f);
	}

	private void InitializeControlData(float distance, float offset, float pan, float tilt)
	{
		_targetCcd = new CameraControlDataDelta(_minDistance, _maxDistance, _minOffset, _maxOffset, _minTilt, _maxTilt);
		_targetCcd.SetPositioning(distance, offset, pan, tilt, new Vector2(0.0f, 0.0f), new Vector3(0.0f, 0.0f,0.0f));
		_currentCcd = new CameraControlData(distance, offset, pan, tilt);
		_currentFloorOffset.X = 0.0f;
		_currentFloorOffset.Y = 0.0f;
	}
	
	public override void _Ready()
	{
		_gameStatus =  GetNode<GameStatus>("/root/GameStatus");
		_eventBus = GetNode<EventBus>("/root/EventBus");
		_eventBus.CameraMotionDeltas += ApplyCameraMotions;
		_eventBus.PlayerMotionData += ApplyFloorOffset;
		_eventBus.NewCameraFocus += NewCameraFocus;

		if (_gameStatus.Status == Enums.GameStatus.Normal)
		{
			if (_gameStatus.Player != null)
			{
				_targetWorldPosNode = _gameStatus.Player;
			}
		}
	}

	public override void _Process(double delta)
	{
		_targetCcd.SetWorldPos(_targetWorldPosNode.Position);
		SetCameraMotions(delta);
	}

	private void ApplyFloorOffset(float x, float y)
	{
		_targetCcd.SetTargetFloorOffset(x * _cameraFloorOffset, -y * _cameraFloorOffset); //orientation
		_targetCcd.RotateFloorOffset(_gameStatus.FixedCameraRotationMatrix); // orientation based on camera
		string msg = "pan " + _panNode.Rotation.Y.RadToDeg() + " | x = " + x + " y = " + y + " --> x = " 
		         + _targetCcd.FloorOffset.X + " y = " + _targetCcd.FloorOffset.Y;
		_eventBus.EmitDebugMessage(msg, 0);
	}
	
	private void ApplyCameraMotions(CameraControlData ccd)
	{
		_targetCcd.AddTargetDistance(ccd.Distance * _stepDistance);
		_targetCcd.AddTargetOffset(ccd.Offset * _sStepsOffset);
		_targetCcd.AddTargetPan(ccd.Pan * _mouseSensitivity);
		_targetCcd.AddTargetTilt(-ccd.Tilt * _mouseSensitivity);
	}
	
	private void SetCameraMotions(double delta)
	{
		CameraControlDeltaInterpolator ccd = new CameraControlDeltaInterpolator(_currentCcd, _currentFloorOffset, 
																				this.Position ,_targetCcd);
		
		ccd.InterpolatePositions(_lerpFactorRotation, _lerpFactorDistance, _lerpFactorFloor);
		
		if (_offsetNode != null)
		{
			_offsetNode.Translate(new Vector3(0.0f, ccd.Offset, 0.0f));
			_currentCcd.SetOffset(_offsetNode.Position.Y);
		}
		
		if (_distanceNode != null)
		{
			_distanceNode.Translate(new Vector3(0.0f, 0.0f, ccd.Distance));
			_currentCcd.SetDistance(_distanceNode.Position.Z);
		}
		
		if (_tiltNode != null)
		{
			_tiltNode.RotateX(ccd.Tilt.DegToRad());
			_currentCcd.SetTilt(_tiltNode.Rotation.X.RadToDeg());
		}

		if (_panNode != null)
		{
			_panNode.RotateY(ccd.Pan.DegToRad());
			float panDeg = _panNode.Rotation.Y.RadToDeg();
			float panDifference = panDeg - _currentCcd.Pan - ccd.Pan;
			if (panDifference.Abs() > 0.5f) // There is a jump of 360°
			{
				_targetCcd.AddTargetPan(-ccd.Pan.Sign() * 360.0f);
			}
			_currentCcd.SetPan(panDeg);	
			_gameStatus.SetCameraRotationMatrix(_panNode.Rotation.Y);
		}

		if (_floorNode != null)
		{
			_floorNode.Translate(new Vector3(ccd.FloorOffset.X, 0.0f, ccd.FloorOffset.Y));
			_currentFloorOffset.X = _floorNode.Position.X;
			_currentFloorOffset.Y = _floorNode.Position.Z;
		}
		
		this.Translate(ccd.WorldPosition);
	}

	public void SetCameraControlNode(CameraOffset value)
	{
		_offsetNode = value;
	}
	
	public void SetCameraControlNode(CameraDistance value)
	{
		_distanceNode = value;
	}
	public void SetCameraControlNode(CameraTilt value)
	{
		_tiltNode = value;
	}

	public void SetCameraFloorNode(CameraFloor value)
	{
		_floorNode = value;
	}

	public void SetCameraPanNode(CameraPan value)
	{
		_panNode = value;
	}

	private void NewCameraFocus(Vector3 pos)
	{
		// _targetWorldPosition = pos;
	}
}
