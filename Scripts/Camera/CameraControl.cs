using Godot;
using System;

public partial class CameraControl : Node3D
{
	[Export] public float MouseSensitivity = 0.5f;
	[Export] public float StepsOffset = 0.5f;
	[Export] public float StepDistance = 2.5f;
	[Export] private int MinDistance = 5;
	[Export] private int MaxDistance = 30;
	[Export] private int MinOffset = 0;
	[Export] private int MaxOffset = 3;	
	[Export] private int MinTilt = -80;
	[Export] private int MaxTilt = -5;
	[Export] private float LerpFactorDistance = 0.9f;
	[Export] private float LerpFactorRotation = 0.6f;
	[Export] private float LerpFactorFloor = 0.95f;
	[Export] private float CameraFloorOffset = 2.0f;
	
	private CameraControlData _currentCCD;
	private Vector2 _currentFloorOffset;
	private CameraControlDataDelta _targetCCD;
	
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

	public CameraControl()
	{
		_currentFloorOffset = new Vector2(0.0f, 0.0f);
		InitializeControlData(13.0f, 0.0f, 0.0f, -30f);
	}

	private void InitializeControlData(float distance, float offset, float pan, float tilt)
	{
		_targetCCD = new CameraControlDataDelta(MinDistance, MaxDistance, MinOffset, MaxOffset, MinTilt, MaxTilt);
		_targetCCD.SetPositioning(distance, offset, pan, tilt, new Vector2(0.0f, 0.0f));
		_currentCCD = new CameraControlData(distance, offset, pan, tilt);
		_currentFloorOffset.X = 0.0f;
		_currentFloorOffset.Y = 0.0f;

	}
	public override void _Ready()
	{
		EventBus eventBus = GetNode<EventBus>("/root/EventBus");
		eventBus.CameraMotionDeltas += ApplyCameraMotions;
		eventBus.PlayerMotionData += ApplyFloorOffset;
	}

	public override void _Process(double delta)
	{
		SetCameraMotions(delta);
	}

	private void ApplyFloorOffset(float x, float y)
	{
		_targetCCD.SetTargetFloorOffset(x * CameraFloorOffset, -y * CameraFloorOffset);
		_targetCCD.RotateFloorOffsetDeg(-_currentCCD.Pan.DegToRad());
	}
	
	private void ApplyCameraMotions(CameraControlData ccd)
	{
		_targetCCD.AddTargetDistance(ccd.Distance * StepDistance);
		_targetCCD.AddTargetOffset(ccd.Offset * StepsOffset);
		_targetCCD.AddTargetPan(ccd.Pan * MouseSensitivity);
		_targetCCD.AddTargetTilt(-ccd.Tilt * MouseSensitivity);
	}
	
	private void SetCameraMotions(double delta)
	{
		CameraControlDeltaInterpolator ccd = new CameraControlDeltaInterpolator(_currentCCD, _currentFloorOffset, _targetCCD);
		ccd.InterpolatePositions(LerpFactorRotation, LerpFactorDistance, LerpFactorFloor);
		
		if (_offsetNode != null)
		{
			_offsetNode.Translate(new Vector3(0.0f, ccd.Offset, 0.0f));
			_currentCCD.SetOffset(_offsetNode.Position.Y);
		}
		
		if (_distanceNode != null)
		{
			_distanceNode.Translate(new Vector3(0.0f, 0.0f, ccd.Distance));
			_currentCCD.SetDistance(_distanceNode.Position.Z);
		}
		
		if (_tiltNode != null)
		{
			_tiltNode.RotateX(ccd.Tilt.DegToRad());
			_currentCCD.SetTilt(_tiltNode.Rotation.X.RadToDeg());
		}

		if (_panNode != null)
		{
			_panNode.RotateY(ccd.Pan.DegToRad());
			float panDeg = _panNode.Rotation.Y.RadToDeg();
			float panDifference = panDeg - _currentCCD.Pan - ccd.Pan;
			if (panDifference.Abs() > 0.5f) // Sprung von 360° vorhanden
			{
				_targetCCD.AddTargetPan(-ccd.Pan.Sign() * 360.0f);
			}
			_currentCCD.SetPan(panDeg);	
		}

		if (_floorNode != null)
		{
			_floorNode.Translate(new Vector3(ccd.FloorOffset.X, 0.0f, ccd.FloorOffset.Y));
			_currentFloorOffset.X = _floorNode.Position.X;
			_currentFloorOffset.Y = _floorNode.Position.Z;
		}
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
}
