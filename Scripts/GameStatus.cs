using Godot;
using System;
using RPG3D.General;

public partial class GameStatus : Node
{
    [Export] private float _pauseTransitionTime = 0.5f;
    private float[,] _fixedCameraRotationMatrix;
    public float[,] FixedCameraRotationMatrix => _fixedCameraRotationMatrix;

    private Player _playerNode;
    public Player Player => _playerNode;

    private Enums.GameStatus _gameStatus = Enums.GameStatus.Normal;
    public Enums.GameStatus Status => _gameStatus;
    
    private Enums.GameStatus _preMenuSatus = Enums.GameStatus.Normal;

    private EventBus _eventBus;
    private EventBusMenu _eventBusMenu;

    private UITimer _uiTimer;

    private double _currentDeltaT;

    private UITimer _timerPause;
    
    public Enums.InputMotion InputMotion { get; private set; }
    
    public GameStatus()
    {
        _fixedCameraRotationMatrix = new float[2,2];
        SetCameraRotationMatrix(0.0f);
    }

    public override void _Process(double delta)
    {
        _currentDeltaT = delta;
        _eventBus.EmitDebugMessage("dT = " + delta + " -> " + 1.0/delta + " FPS", "deltaT");
    }
    public void ChangeEngineSpeedTo(double target, float duration)
    {
        Tween engineTween = CreateTween();
        engineTween.TweenMethod(Callable.From<double>(SetEngineSpeed), Convert.ToSingle(Engine.TimeScale), target, duration);
        _eventBus.MakeTweenTimeIndependend(engineTween);
    }

    public void SetEngineSpeed(double timeScale)
    {
        Engine.TimeScale = timeScale;
        _eventBus.EmitSetTweenSpeedScale(Convert.ToSingle(1.0 / timeScale));
        // _eventBus.EmitMissingTimeDelta(_currentDeltaT / timeScale  - _currentDeltaT);
    }
    
    public override void _Ready()
    {
        base._Ready();
        _eventBus = GetNode<EventBus>("/root/EventBus");
        _eventBusMenu = GetNode<EventBusMenu>("/root/EventBusMenu");
        _eventBus.PlayerIsSet += SetPlayerNode;
        _eventBus.NeedPlayerNode += ShoutPlayerNode;
        ConnectSignals();
    }

    private void ConnectSignals()
    {
        _eventBusMenu.OpenMainMenu += SetStatusToMainMenu;
        _eventBusMenu.MainMenuGotClosed += MainMenuGotClosed;
    }
    
    public void SetCameraRotationMatrix(float angle)
    {
        _fixedCameraRotationMatrix[0, 0] =  angle.Cos();
        _fixedCameraRotationMatrix[0, 1] = -(angle.Sin());
        _fixedCameraRotationMatrix[1, 0] = -_fixedCameraRotationMatrix[0, 1];
        _fixedCameraRotationMatrix[1, 1] = _fixedCameraRotationMatrix[0, 0];
    }
	//
	public void SetCameraRotationMatrixDeg(float angle) => SetCameraRotationMatrix(angle.DegToRad());

	private void SetPlayerNode(Player player)
    {
        _playerNode = player;
        if (_gameStatus == Enums.GameStatus.Normal)
        {
            ShoutPlayerNode();
        }
    }

    private void ShoutPlayerNode()
    {
        if (_playerNode == null)
            _eventBus.EmitSetNewCameraFocus(new Node3D());
        else
            _eventBus.EmitSetNewCameraFocus(_playerNode);
    }

    private void SetStatusToMainMenu()
    {
        _preMenuSatus = _gameStatus;
        _gameStatus = Enums.GameStatus.Menu;
        PauseGameIn(1.0);
    }

    private void MainMenuGotClosed()
    {
        InputMotion = Enums.InputMotion.InputAllowed;
        _gameStatus = _preMenuSatus;
        // UnPauseGameIn(1.0);
        UnPauseGameNowAndKillTimer();
        ChangeEngineSpeedTo(1,_pauseTransitionTime); 
    }

    private void PauseGameIn(double time)
    {
        InputMotion = Enums.InputMotion.InputFreezed;
        ClearPauseTimerIfNecessary();
        _timerPause = new UITimer();
        AddChild(_timerPause);
        _timerPause.WaitTime = time;
        _timerPause.Timeout += PauseGameNowAnKillTimer;
        _timerPause.Start();
        ChangeEngineSpeedTo(0.01,_pauseTransitionTime);

    }

    public void ClearPauseTimerIfNecessary()
    {
        if (_timerPause != null)
        {
            _timerPause.QueueFree();
            _timerPause = null;
        }
    }
    private void UnPauseGameIn(double time)
    {
        ClearPauseTimerIfNecessary();
        _timerPause = new UITimer();
        AddChild(_timerPause);
        _timerPause.WaitTime = time;
        _timerPause.Timeout += UnPauseGameNowAndKillTimer;
        _timerPause.ProcessMode = ProcessModeEnum.Always;
        _timerPause.Start();
        ChangeEngineSpeedTo(1,_pauseTransitionTime);       
    }

    private void PauseGameNowAnKillTimer()
    {
        GetTree().Paused = true;
        ClearPauseTimerIfNecessary();
        _eventBus.EmitDebugMessage("Game Paused", Convert.ToString(this)+"_001");
    }

    private void UnPauseGameNowAndKillTimer()
    {
        UnPauseGameNow();
        ClearPauseTimerIfNecessary();
    }

    private void UnPauseGameNow()
    {
        GetTree().Paused = false;
        _eventBus.EmitDebugMessage("Game Unpaused", Convert.ToString(this)+"_001");

    }
    

}