using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

//https://www.youtube.com/watch?v=dfp7FIO4GTA&t=407s
public partial class SimpleState : Node
{
    private bool HasBeenInitialized = false;
    private bool OnUpdateFired = false;

    [Signal] public delegate void StateStartEventHandler();
    [Signal] public delegate void StateUpdatedEventHandler();
    [Signal] public delegate void StateExitedEventHandler();

    public virtual void OnStart(Dictionary<string, object> message = null)
    {
        EmitSignal(nameof(StateStartEventHandler));
        HasBeenInitialized = true;
    }

    public virtual void OnUpdate(Dictionary<string, object> message = null) {
        if (!HasBeenInitialized)
            return; 
        EmitSignal(nameof(StateUpdatedEventHandler));
        OnUpdateFired = true;
    }

    public virtual void UpdateState(float dt) {
        
    }

    public virtual void OnExit(string nextState) {
        if (!HasBeenInitialized)
            return;
        EmitSignal(nameof(StateExitedEventHandler));
        HasBeenInitialized = false;
        OnUpdateFired = false;

    }
}