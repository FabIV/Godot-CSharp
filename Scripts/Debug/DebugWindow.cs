using Godot;
using System;
using Microsoft.VisualBasic;
using RPG3D.General.Debug;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

public partial class DebugWindow : Control
{
	private Label _lable;

	private Dictionary<string, DebugElement> _messages;
	// Called when the node enters the scene tree for the first time.
	public DebugWindow()
	{
		_messages = new Dictionary<string, DebugElement>();
	}
	public override void _Ready()
	{
		EventBus eventBus = GetNode<EventBus>("/root/EventBus");
		eventBus.DebugMessage += SetDebugText;
		_lable = GetChild<Label>(0);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		string lableString = "";
		List<string> keysToRemove = new List<string>();
		foreach (var el in _messages)
		{
			lableString += el.Value.DebugMessage;
			lableString += "\n";
			if (el.Value.ChangeTime(delta))
			{
				keysToRemove.Add(el.Key);
			}
		}

		foreach (var key in keysToRemove)
		{
			_messages.Remove(key);
		}

		_lable.Text = lableString;
	}

	private void SetDebugText(string msg, string id)
	{
		if (_messages.ContainsKey(id))
		{
			_messages[id].UpdateMessage(msg);
		}
		else
		{
			_messages.Add(id, new DebugElement(msg));
		}
	}
}
