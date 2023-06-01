using Godot;
using System;

public partial class CameraControl : Node3D
{
	[Export] public float MouseSensitivity = 0.03f;
	[Export] public float StepsOffset = 5f;
	[Export] public float StepDistance = 10f;
	[Export] private int MinDistance = 5;
	[Export] private int MaxDistance = 100;
	[Export] private int MinTilt = 5;
	[Export] private int MaxTilt = 80;
	
	// Called when the node enters the scene tree for the first time.
	private CameraOffset _offsetNode;
	private CameraDistance _distanceNode;
	private CameraTilt _tiltNode;


	private float _targetPan;
	private float _targetTilt;
	private float _targetDistance;
	private float _targetOffset;

	// private CameraPan _panNode;
	public override void _Ready()
	{
		EventBus eventBus = GetNode<EventBus>("/root/EventBus");
		eventBus.CameraMotionDeltas += SetCameraMotions;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	// private void SetCameraMotions(float deltaDistance, float deltaOffset, float deltaPan, float deltaTilt)
	private void SetCameraMotions(CameraControlData ccd)
	{
		// CameraControlData deltas = new CameraControlData(deltaDistance, deltaOffset, deltaPan, deltaTilt);
		
		if (_offsetNode != null)
		{
		}
		if (_distanceNode != null)
		{
			
		}
		if (_tiltNode != null)
		{
			_tiltNode.RotateX(-ccd.Tilt * MouseSensitivity);
		}
		
		this.RotateY(ccd.Pan * MouseSensitivity);
		

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
