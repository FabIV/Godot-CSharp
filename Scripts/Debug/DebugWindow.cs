using Godot;
using System;
using Microsoft.VisualBasic;

public partial class DebugWindow : Control
{
	private Label _lable;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		EventBus eventBus = GetNode<EventBus>("/root/EventBus");
		eventBus.DebugMessage += SetDebugText;
		_lable = GetChild<Label>(0);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void SetDebugText(string msg, int id)
	{
		_lable.Text = msg;
	}
}
