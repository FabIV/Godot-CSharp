using Godot;
using System;
using RPG3D.General;

public partial class GamePlayControl : Node
{
	[Export] private Enums.Language Language = Enums.Language.de;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() => TranslationServer.SetLocale(Language.ToString());

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
