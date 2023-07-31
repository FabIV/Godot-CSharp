using Godot;
using System;
using System.Collections.Generic;
public partial class MainMenu : Control
{
	EventBusMenu _eventBusMenu;

	[Export] private NodePath _initMenuPath;
	private SubMenuControl _initMenu;
	// Called when the node enters the scene tree for the first time.

	private List<SubMenuControl> _callMenuOrder;
	public override void _Ready()
	{
		_eventBusMenu = GetNode<EventBusMenu>("/root/EventBusMenu");
		_initMenu = GetNode<SubMenuControl>(_initMenuPath);
		_callMenuOrder = new List<SubMenuControl>();
		_eventBusMenu.OpenMainMenu += OpenMainMenu;
		_eventBusMenu.UIBack += GoBack;
	}

	private void OpenMainMenu() => InitiateOpenNewMenu(_initMenu);

	private void CloseMainMenu() => _eventBusMenu.CloseMainMenu();

	private void GoBack()
	{
		// _callMenuOrder.RemoveAt(_callMenuOrder.Count - 1);
		SubMenuControl menuToClose = _callMenuOrder.LastElement();
		menuToClose.Deactivate();
		_callMenuOrder.RemoveLast();
		if (_callMenuOrder.Count > 0)
		{
			ChangeSubMenu();
		}
		else
		{			
			CloseMainMenu();
		}
	}

	public void InitiateOpenNewMenu(SubMenuControl newMenu)
	{
		_callMenuOrder.Add(newMenu);
		ChangeSubMenu();
	}
	
	private void ChangeSubMenu()
	{
		SubMenuControl menuToOpen = _callMenuOrder.LastElement();
		menuToOpen.Activate();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
