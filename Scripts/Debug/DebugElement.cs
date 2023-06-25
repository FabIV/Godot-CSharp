namespace RPG3D.General.Debug;

public class DebugElement
{
    private const float MESSAGE_TIME = 3.0f;
    private double _time;
    public string DebugMessage { get; private set; }

    public DebugElement(string debugMessage)
    {
        _time = MESSAGE_TIME;
        DebugMessage = debugMessage;
    }

    public bool ChangeTime(double delta)
    {
        _time -= delta;
        return _time < 0.0f;
    }

    public void UpdateMessage(string newMessage)
    {
        _time = MESSAGE_TIME;
        DebugMessage = newMessage;

    }
}