using Godot;

namespace Pixelator;

public class CameraProjection
{
    public bool Ready { get; private set; }

    public bool HasViewProjection
    { get { return ViewProjection != null; }}
    public bool HasCameraSystem
    { get { return CameraSystem != null; }}
    public ViewProjection ViewProjection { get; private set;}
    public SubCameraSystem CameraSystem { get; private set;}
    public IntXY RelativePositions { get; private set;}

    public CameraProjection(ViewProjection vp, int[] pos)
    {
        ViewProjection = vp;
        RelativePositions = new IntXY(-pos[0], pos[1]);
    }

    public void SetProjectionScale(float newScale)
    {
        ViewProjection.SetScale(newScale);
    }

    public void SetCameraRotation(float angle)
    {
        CameraSystem.SetRotation(angle);
    }
    
    public CameraProjection(SubCameraSystem cs, int[] pos)
    {
        CameraSystem = cs;
        RelativePositions = new IntXY(-pos[0], pos[1]);
    }

    public void SetCamera(SubCameraSystem cs)
    {
        CameraSystem = cs;
        if (ViewProjection != null)
            Ready = true;
    }
    
    public void SetProjection(ViewProjection vp)
    { 
        ViewProjection = vp;
        if (CameraSystem != null)
            Ready = true;
    }

    public void SetRelativePosition(int posX, int posY)
    {
        RelativePositions.SetXY(posX, posY);
    }

    public void SetPositions(int scaler, float pixelY, CamerRotationOrganizer cro)
    {
        ViewProjection.Position = new Vector2(RelativePositions.X * 512.0f * scaler, RelativePositions.Y * 512.0f * scaler);


        float newX = cro.Origin.X + (RelativePositions.X * cro.XPart.X + RelativePositions.Y * cro.YPart.X * pixelY) * 8.0f;
        float newY = cro.Origin.Y + (RelativePositions.Y * cro.YPart.Y * pixelY + RelativePositions.X * cro.XPart.Y) * 8.0f;
        
        CameraSystem.SetPosition(new Vector3(newX, 0.0f, newY));
    }
    
}

