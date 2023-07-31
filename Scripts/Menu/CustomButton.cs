using Godot;
using System;
using RPG3D.General;

public partial class CustomButton : Button
{
	[Export] private Enums.FadeInDirection _fadeInDirection = Enums.FadeInDirection.Left;
	[Export] private float _fadeLengthExtention = 0.1f;
	private Vector2 _tweenOutPosition;
	private Vector2 _tweenInPosition;

	private EventBus _eventBus;

	private double _delay;
	private double _duration;

	public CustomButton()
	{
		_delay = 0.0f;
		_duration = 1.0f;
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_eventBus = _eventBus = GetNode<EventBus>("/root/EventBus");
		_tweenInPosition = GlobalPosition;
		DeterminTweenDirection();
	}

	public void PrepareButton(double duration, double delay)
	{
		_tweenInPosition = GlobalPosition;
		DeterminTweenDirection();
		SetTweenData(duration, delay);
	}
	public void CorrectInitialData(float x, float y)
	{
		_tweenInPosition.X = x;
		_tweenInPosition.Y = y;
		DeterminTweenDirection();
		TweenOut(0.0, 0.0);
	}

	public void SetTweenData(double duration, double delay)
	{
		_duration = duration;
		_delay = delay;
	}

	public void TweenOut() => TweenOut(_duration, _delay);

	public void TweenOut(double duration, double delay) => DoPositionTween(duration, delay, _tweenOutPosition);

	public void TweenIn() => TweenIn(_duration, _delay);

	public void TweenIn(double duration, double delay) => DoPositionTween(duration, delay, _tweenInPosition);
	private void DoPositionTween(double duration, double delay, Vector2 targetPos)
	{
		Tween positionTween = CreateTween();
		positionTween.TweenInterval(delay);
		positionTween.TweenProperty(this, "position", targetPos, duration).SetTrans(Tween.TransitionType.Back).SetEase(Tween.EaseType.Out);
		_eventBus.SetTweenSpeedScale += positionTween.SetSpeedScale;
	}
	
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void DeterminTweenDirection()
	{
		_tweenOutPosition.X = 0.0f;
		_tweenOutPosition.Y = 0.0f;
		if (_fadeInDirection is Enums.FadeInDirection.Left or Enums.FadeInDirection.BottomLeft or
			Enums.FadeInDirection.TopLeft)
			_tweenOutPosition.X = -1;
		if (_fadeInDirection is Enums.FadeInDirection.Right or Enums.FadeInDirection.BottomRight or
			Enums.FadeInDirection.TopRight)
			_tweenOutPosition.X = 1;
		if (_fadeInDirection is Enums.FadeInDirection.Top or Enums.FadeInDirection.TopLeft or
			Enums.FadeInDirection.TopRight)
			_tweenOutPosition.Y = 1;
		if (_fadeInDirection is Enums.FadeInDirection.Bottom or Enums.FadeInDirection.BottomLeft or
			Enums.FadeInDirection.BottomRight)
			_tweenOutPosition.Y = -1;
		_tweenOutPosition *= Size;
		_tweenOutPosition *= (1.0f + _fadeLengthExtention);
		_tweenOutPosition += _tweenInPosition;
	}
}
