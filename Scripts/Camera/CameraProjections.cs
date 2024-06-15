using Godot;

namespace Pixelator;

public class CameraProjections
{
    private CameraProjection[,] _cameraProjections;

    private int _nextViewportPos = 0;
    private int _nextCameraPos = 0;
    
    private int _storedScaler = 1;
    private float _storedPixelY = 1.0f;
    
    public int Length0 { get { return _cameraProjections.GetLength(0); } }
    public int Length1 { get { return _cameraProjections.GetLength(1); } }

    public CameraProjection this[int i, int j]
    {
        get { return _cameraProjections[i, j]; }
    }

    public CameraProjections(int x, int y)
    {
        _cameraProjections = new CameraProjection[x, y];
    }
    
    public void SetNextCamera(SubCameraSystem cam)
    {
        int[] i = GetArrayPositions(_cameraProjections, _nextCameraPos);
        if (_cameraProjections[i[0], i[1]] == null)
            _cameraProjections[i[0], i[1]] = new CameraProjection(cam, i);
        else
            _cameraProjections[i[0], i[1]].SetCamera(cam);
        _nextCameraPos++;
    }
    
    public void SetNextViewProjection(ViewProjection cam)
    {
        int[] i = GetArrayPositions(_cameraProjections, _nextViewportPos);
        if (_cameraProjections[i[0], i[1]] == null)
            _cameraProjections[i[0], i[1]] = new CameraProjection(cam, i);
        else
            _cameraProjections[i[0], i[1]].SetProjection(cam);
        _nextViewportPos++;
    }
    
    
    
    public void SetPositions(int scaler, float pixelY, CamerRotationOrganizer cro)
    {
        foreach (var cam in _cameraProjections)
        {
            cam.SetPositions(scaler, pixelY, cro);
            
        }

        _storedScaler = scaler;
        _storedPixelY = pixelY;
    }

    public void SetRotation(float invAngle)
    {
        foreach (var cam in _cameraProjections)
        {
            cam.SetCameraRotation(-invAngle);
        }
    }
    
    private int[] GetArrayPositions<T>(T[,] mat, int position)
    {
        int rows = mat.GetLength(0);
        int columns = mat.GetLength(1);

        int c = position % columns;
        int r = (position - c) / columns;

        int[] res = { r, c };

        return res;
    }

    public void SetRelativePosition(int x1, int x2, int y1, int y2, CamerRotationOrganizer cro)
    {
        EnsurePositionalOrder(ref x1, ref x2);
        EnsurePositionalOrder(ref y1, ref y2);
        
        _cameraProjections[0,0].SetRelativePosition(x1, y1);
        _cameraProjections[1,0].SetRelativePosition(x1, y2);
        _cameraProjections[0,1].SetRelativePosition(x2, y1);
        _cameraProjections[1,1].SetRelativePosition(x2, y2);

        foreach (var projection in _cameraProjections)
        {
            projection.SetPositions(_storedScaler, _storedPixelY, cro);
        }

    }

    private void EnsurePositionalOrder(ref int a1, ref int a2)
    {
        if (a1 % 2 != 0)
        {
            int t = a2;
            a2 = a1;
            a1 = t;
        }
    }
}