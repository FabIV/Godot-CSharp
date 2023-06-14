using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class ControlVertical : VBoxContainer
{
	private SubMenuControl _controler;
	private List<CustomButton> _buttons;
	[Export] private double _duration = 0.3;
	[Export] private double _delay = 0.1;
	
	// Called when the node enters the scene tree for the first time.
	public ControlVertical()
	{
		_buttons = new List<CustomButton>();
	}
	public override void _Ready()
	{
		_controler = GetParent <SubMenuControl>();
		_buttons = GetChildren().OfType<CustomButton>().ToList();
		
		_controler.SubMenuActivated += ActivateMenu;
		_controler.SubMenuDeactivate += DeactivateMenu;
		
		double currentDelay = 0.0;
		float totalLength = 0;
		foreach (var button in _buttons)
		{	
			button.PrepareButton(_duration, currentDelay);
			currentDelay += _delay;
			totalLength += button.Size.Y;
		}

		float deltaLength = this.Size.Y - totalLength;
		deltaLength /= _buttons.Count - 1;
		// float currentX = _buttons[0].GlobalPosition.X;
		// float currentY = _buttons[0].GlobalPosition.Y;
		float currentX = 0.0f;
		float currentY = 0.0f;
		_buttons[0].CorrectInitialData(currentX, currentY);
		
		for (int i = 1; i < _buttons.Count; i++)
		{
			currentY += deltaLength + _buttons[i - 1].Size.Y;
			_buttons[i].CorrectInitialData(_buttons[i].GlobalPosition.X, currentY);
		}
	}

	public void ActivateMenu()
	{
		foreach (var button in _buttons)
		{
			button.TweenIn();
		}
	}

	public void DeactivateMenu()
	{
		foreach (var button in _buttons)
		{
			button.TweenOut();
		}
	}
	
	
}
