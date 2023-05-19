using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

//https://www.youtube.com/watch?v=dfp7FIO4GTA&t=407s
public partial class SimpleStateMachine : Node
{
    [Signal] public delegate void PreStartEventHandler();
    [Signal] public delegate void PostStartEventHandler();
    [Signal] public delegate void PreExitEventHandler();
    [Signal] public delegate void PostExitEventHandler();

    public List<SimpleState> States;
erride void _Ready(){
        base._Ready();
        States = GetNode<Node>("States").GetChildren().OfType<SimpleState>().ToList();
    public string CurrentState;
    public string LastState;

    protected SimpleState _state = null;

    public ov
    }

    public void SetState(SimpleState state, Dictionary<string, object> message = null)
    {
        if (state == null){
            return;
        }
        else {
            EmitSignal(nameof(PreExitEventHandler));
            _state.OnExit(state.GetType().ToString());
            EmitSignal(nameof(PostExitEventHandler));
            LastState = CurrentState;
            CurrentState = state.GetType().ToString();

            _state = state;
            EmitSignal(nameof(PreStartEventHandler));
            _state.OnStart(message);
            EmitSignal(nameof(PostStartEventHandler));
            _state.OnUpdate();
        }
    }

    public void _PhysicsProcess(float delta)
    {
        if (_state == null)
            return;
        _state.UpdateState(delta);
    }

}