using System.Security.AccessControl;

namespace RPG3D.General;

public class InputContainer
{
    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }
    public InputContainer(float horizontal, float vertical)
    {
        Horizontal = horizontal;
        Vertical = vertical;
    }
    
    public void SetValues(float horizontal, float vertical)
    {
        Horizontal = horizontal;
        Vertical = vertical;
    }
}