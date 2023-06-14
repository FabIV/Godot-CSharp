using Godot;
using System;

public partial class SubMenuControl : Control
{
    [Signal] public delegate void SubMenuActivatedEventHandler();
    [Signal] public delegate void SubMenuDeactivateEventHandler();
    
    public void Activate()
    {
        EmitSignal(nameof(SubMenuActivated));
    }

    public void Deactivate()
    {
        EmitSignal(nameof(SubMenuDeactivate));
    }
    
    
}
