using Godot;
using System;
using RPG3D.General;

public partial class GameStatus : Node
{
    private float[,] _fixedCameraRotationMatrix;
    public float[,] FixedCameraRotationMatrix => _fixedCameraRotationMatrix;

    private Player _playerNode;
    public Player Player => _playerNode;

    private Enums.GameStatus _gameStatus = Enums.GameStatus.Normal;
    public Enums.GameStatus Status => _gameStatus;

    private EventBus _eventBus;

    public override void _Ready()
    {
        base._Ready();
        _eventBus = GetNode<EventBus>("/root/EventBus");
        _eventBus.PlayerIsSet += SetPlayerNode;
        _eventBus.NeedPlayerNode += ShoutPlayerNode;
    }

    public GameStatus()
    {
        _fixedCameraRotationMatrix = new float[2,2];
        SetCameraRotationMatrix(0.0f);
    }
    
    public void SetCameraRotationMatrix(float angle)
    {
        _fixedCameraRotationMatrix[0, 0] =  angle.Cos();
        _fixedCameraRotationMatrix[0, 1] = -(angle.Sin());
        _fixedCameraRotationMatrix[1, 0] = -_fixedCameraRotationMatrix[0, 1];
        _fixedCameraRotationMatrix[1, 1] = _fixedCameraRotationMatrix[0, 0];
    }
    //
    public void SetCameraRotationMatrixDeg(float angle)
    {
        SetCameraRotationMatrix(angle.DegToRad());
    }

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
            _eventBus.SetNewCameraFocus(new Vector3(0.0f, 0.0f, 0.0f));
        else
            _eventBus.SetNewCameraFocus(_playerNode.Position);
    }
    

}