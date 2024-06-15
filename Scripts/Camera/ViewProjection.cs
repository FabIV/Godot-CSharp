using Godot;
using System;

public partial class ViewProjection : Sprite2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SystemControl sys = GetParent<SystemControl>();
		sys.AddViewProjection(this);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void SetScale(float newScale)
	{
		this.Scale = new Vector2(newScale, newScale);
	}
}
