using Godot;
using System;
using System.Collections.Generic;
using RPG3D.General.Data;

public partial class DataManagement : Node
{
	[Export] public bool IsActualManager = false;
	[Export] public bool ShowRegistrations = false;
	[Export] public bool ShowWarnings = false;
	
	private List<CharData> _charData;
	// private List<LootBox> _LootBoxes;
	// private Dictionary<ItemData> _itemData;
	private DataManagement _actualmanager;
	
	public DataManagement() {
		if (IsActualManager) {
			_charData = new List<CharData>();
			_actualmanager = this;
		}
	}

	public void AddCharData(CharDataSimple inputData) {
		_actualmanager._charData.Add(new CharData(inputData));
	}
}
