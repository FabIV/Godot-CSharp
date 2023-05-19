using Godot;
using System;

public partial class DataManagement : Node
{
	[Export] public bool ShowRegistrations = false;
	[Export] public bool Show_Warnings = false;

	public DataManagement() {
		this.AddToGroup("DataManagement");
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
