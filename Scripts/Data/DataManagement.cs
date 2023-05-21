using Godot;
using System;
using System.Collections.Generic;
// using Godot.Collections;
using RPG3D.General;
using RPG3D.General.Data;

public partial class DataManagement : Node
{
	[Export] public bool IsActualManager = false;
	[Export] public bool ShowRegistrations = false;
	[Export] public bool ShowWarnings = false;
	
	private List<CharData> _charData;
	// private List<LootBox> _LootBoxes;
	private Dictionary<int, ItemData> _itemData;
	private DataManagement _actualManager;

	private Dictionary<string, int[]>  _itemCountList;
	public DataManagement() {
		EnsureItemCountList();
		if (IsActualManager) {
			EnsureCharDataExistence();
			_actualManager = this;
		}
		else {
			_actualManager = GetParent<DataManagement>();
		}
	}

	public void AddCharData(CharDataSimple inputData)
	{
		if (IsActualManager) {
			EnsureCharDataExistence();
			this._charData.Add(new CharData(inputData));
		}
		else {
			_actualManager._charData.Add(new CharData(inputData));
		}
	}

	public void AddItemData(ItemDataDefinition newItem) {
		GD.Print("DataManagement/ Item added");
	}

	private void EnsureCharDataExistence() {
		if (_charData == null)
			_charData = new List<CharData>();
	}

	private void EnsureItemCountList() {
		if (_itemCountList == null) {
			_itemCountList = new Dictionary<string, int[]>();
			foreach (int i  in Enum.GetValues(typeof(Enums.ItemType))) {
				string key = ((Enums.ItemType)i).ToString();
				if (key == "Quest" || key == "Usable") {
					int[] tempList = new int[1];
					_itemCountList.Add(key, tempList);
				}
				else if (key == "Weapon")
				{
					int[] tempList = new int[Enum.GetNames(typeof(Enums.WeaponType)).Length -1 ];
					_itemCountList.Add(key, tempList);
				}
				else if (key == "Shield") {
					int[] tempList = new int[Enum.GetNames(typeof(Enums.ShieldsType)).Length -1 ];
					_itemCountList.Add(key, tempList);
				}
				else if (key == "Armor") {
					int[] tempList = new int[Enum.GetNames(typeof(Enums.ArmorType)).Length -1 ];
					_itemCountList.Add(key, tempList);
				}
				else if (key == "Accessoires") {
					int[] tempList = new int[Enum.GetNames(typeof(Enums.AccessoiresType)).Length -1];
					_itemCountList.Add(key, tempList);
				}
				else if (key == "Craft") {
					int[] tempList = new int[Enum.GetNames(typeof(Enums.CraftType)).Length -1];
					_itemCountList.Add(key, tempList);
				}
				//{Quest, Usable, Weapon, Shield, Armor, Accessoires, Craft, Invalid}
			}
			GD.Print("Finished");
		}
	}
}
