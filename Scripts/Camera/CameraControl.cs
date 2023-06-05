using Godot;
using System;

public partial class CameraControl : Node3D
{
	[Export] public float MouseSensitivity = 0.5f;
	[Export] public float StepsOffset = 0.5f;
	[Export] public float StepDistance = 2.5f;
	[Export] private int MinDistance = 5;
	[Export] private int MaxDistance = 100;
	[Export] private int MinOffset = 0;
	[Export] private int MaxOffset = 3;	
	[Export] private int MinTilt = -80;
	[Export] private int MaxTilt = -5;
	[Export] private float LerpFactorDistance = 0.9f;
	[Export] private float LerpFactorRotation = 0.6f;
	
	private CameraControlData _currentCCD;
	private CameraControlDataDelta _targetCCD;
	
	// Called when the node enters the scene tree for the first time.
	private CameraOffset _offsetNode;
	private CameraDistance _distanceNode;
	private CameraTilt _tiltNode;

	private float _targetPan;
	private float _targetTilt;
	private float _targetDistance;
	private float _targetOffset;

	public CameraControl()
	{
		InitializeControlData(13.0f, 0.0f, 0.0f, -30f);
	}

	private void InitializeControlData(float distance, float offset, float pan, float tilt)
	{
		_targetCCD = new CameraControlDataDelta(MinDistance, MaxDistance, MinOffset, MaxOffset, MinTilt, MaxTilt);
		_targetCCD.SetPositioning(distance, offset, pan, tilt);
		_currentCCD = new CameraControlData(distance, offset, pan, tilt);

	}
	// private CameraPan _panNode;
	public override void _Ready()
	{
		EventBus eventBus = GetNode<EventBus>("/root/EventBus");
		eventBus.CameraMotionDeltas += ApplyCameraMotions;
	}

	public override void _Process(double delta)
	{
		SetCameraMotions(delta);
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
		CameraControlDeltaInterpolator ccd = new CameraControlDeltaInterpolator(_currentCCD, _targetCCD);
		ccd.InterpolatePositions(LerpFactorRotation, LerpFactorDistance);
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
		this.RotateY(ccd.Pan.DegToRad());
		float panDeg = this.Rotation.Y.RadToDeg();
		float panDifference = panDeg - _currentCCD.Pan - ccd.Pan;
		if (panDifference.Abs() > 0.5f) // Sprung von 360° vorhanden
		{
			_targetCCD.AddTargetPan(-ccd.Pan.Sign() * 360.0f);
		}
		_currentCCD.SetPan(panDeg);
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
}
