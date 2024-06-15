using Godot;
using System;
using Pixelator;

public partial class SystemControl : Node2D
{
    [Export] public int ViewportPixels = 512;
    [Export] public int MaxResolution = 480;

    private CameraProjections _cameraProjections;
    private MotionControl _motionControl;
    private Camera2DSystem _camera2D;
    
    public Vector3 PixelFactors;
    
    public float Pan = 30.0f;
    // private float _rotation = 0.0f;
    // private float _targetRotation = 0.0f;
    private CamerRotationOrganizer _camRotOrg;
    
    private float _prevScale = 0.0f;
    

    public float PixelScale => _prevScale;

    public SystemControl()
    {
        _camRotOrg = new CamerRotationOrganizer();
        _prevScale = -1.0f;
    }
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        EnsurePreparation();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        var fDelta = (float)delta;
        SetScale(MaxResolution);
        UpdateActualCameraRotation(fDelta);
        SetCameraPositions();
    }

    public void HardOverwriteCamera(float dx, float dy)
    {
        var val = new Vector2(dx / 128.0f, dy / 64.0f);
        val.RotateBy(_camRotOrg.Rotation);

        _camRotOrg.HardOverwriteOrigin(val + _camRotOrg.Origin);

    }

    private void UpdateActualCameraRotation(float delta)
    {
        // var newAngle = (_targetRotation - _camRotOrg.Rotation)  + _camRotOrg.Rotation;
        var newAngle = _camRotOrg.GetNewCameraAngle(delta, _motionControl.CameraRotationSpeed);
        if (newAngle != _camRotOrg.Rotation)
        {
            Vector2 relativeCameraWorldPos = GetRelativeCameraWorldPos();
            _camRotOrg.UpdateRotationData(newAngle, relativeCameraWorldPos);
            _cameraProjections.SetRotation(newAngle);
        }
    }

    private Vector2 GetRelativeCameraWorldPos()
    {
        var cX = _camera2D.Position.X / 64.0f / _prevScale;
        var cY = _camera2D.Position.Y / 64.0f / _prevScale * PixelFactors.Z;

       
        return new Vector2(cX, cY);
    }

    private void SetCameraPositions()
    {
        float posX = _camera2D.Position.X / _prevScale;
        float posY = _camera2D.Position.Y / _prevScale;
        
        int quadrantX1 = (int)(posX / ViewportPixels);
        int quadrantX2 = quadrantX1 - 1;
        if (posX % ViewportPixels > 0)
            quadrantX2 += 2;
        
        int quadrantY1 = (int)(posY / ViewportPixels);
        int quadrantY2 = quadrantY1 - 1;
        if (posY % ViewportPixels > 0)
            quadrantY2 += 2;
        
        _cameraProjections.SetRelativePosition(quadrantX1, quadrantX2, quadrantY1, quadrantY2,  _camRotOrg);
        
    }

    public void SetMotionControl(MotionControl mc)
    {
        _motionControl = mc;
        if (_camera2D != null)
            _motionControl.Set2DCamera(_camera2D);
    }

    public void SetCamera2D(Camera2DSystem cam)
    {
        _camera2D = cam;
        if (_motionControl != null)
            _motionControl.Set2DCamera(cam);
    }
    
    private void SetPixelFactors()
    {
        float horizontal = 1.0f;
        float vertical = 1.0f / Mathf.Cos(-Pan.Rad());
        float forward = -1.0f / Mathf.Sin(-Pan.Rad());
        PixelFactors = new Vector3(horizontal, vertical, forward);
        _camRotOrg.SetPixelFactorY(PixelFactors.Z);
        // SetRotatedPixelFactors(0.0f);
    }
    
    public void AddViewProjection(ViewProjection val)
    {
        EnsurePreparation();
        _cameraProjections.SetNextViewProjection(val);
    }

    public void AddCameraSystem(SubCameraSystem val)
    {
        EnsurePreparation();
        _cameraProjections.SetNextCamera(val);
    }

    private void EnsurePreparation()
    {
        SetPixelFactors();

        if (_cameraProjections == null)
            _cameraProjections = new CameraProjections(2,2);
    }

    public void AddToTargetRotation(float angle)
    {
        // _targetRotation += angle;
        _camRotOrg.AddToTargetRotation(angle);
    }
    
    private void SetScale(int max)
    {
        Vector2 screenSize = DisplayServer.WindowGetSize();
        var size = Math.Max(screenSize.X, screenSize.Y);
        float scaleFactor = 1.0f;
        while (size / scaleFactor > max)
        {
            scaleFactor++;
        }

        // scaleFactor = 1;
        
        if (_prevScale != scaleFactor)
        {
            // GD.Print("SystemControl/ scaleFactor Forced to One");

            _camera2D.Position = new Vector2(_camera2D.Position.X * scaleFactor / _prevScale,
                _camera2D.Position.Y * scaleFactor / _prevScale);
            
            GD.Print("Scale changed --> " + scaleFactor);
            for (int i = 0; i < _cameraProjections.Length0; i++)
            {
                for (int j = 0; j < _cameraProjections.Length1; j++)
                {
                    if (_cameraProjections[i,j] == null)
                        GD.Print("SystemControl/ _cameraProjection[" + i + "," + j + "] == null");
                    else
                        _cameraProjections[i, j].SetProjectionScale(scaleFactor);
                }
            }

            _prevScale = scaleFactor;
            _cameraProjections.SetPositions((int)scaleFactor, PixelFactors.Z, _camRotOrg);
            
        }
        
    }
}