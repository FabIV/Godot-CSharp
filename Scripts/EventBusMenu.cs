using Godot;
using System;

public partial class EventBusMenu : Node
{
    [Signal] public delegate void UIBackEventHandler();
    [Signal] public delegate void OpenMainMenuEventHandler();

    [Signal] public delegate void MainMenuGotClosedEventHandler();

    
    public void EmitUIBack(bool alreadyIn)
    {
        if(alreadyIn)
            EmitSignal(nameof(UIBack));
        else
            EmitSignal(nameof(OpenMainMenu));
    }

    public void CloseMainMenu()
    {
        EmitSignal(nameof(MainMenuGotClosed));
    }

}