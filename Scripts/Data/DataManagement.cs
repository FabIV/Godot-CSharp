using Godot;
using System;
using System.Collections.Generic;
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
	private Dictionary<string, int[]>  _itemCountList;
	private DataManagement _actualManager;

	public override void _Ready()
	{
		base._Ready();
		if (IsActualManager)
		{
			EnsureItemDictExists();
			EnsureItemCountListExists();
			EnsureCharDataExistence();
			_actualManager = this;
		}
		else
		{
			EnsureActualManagement();
		}
	}

	public void AddCharData(CharDataDefinition inputData)
	{
		if (IsActualManager) 
		{
			EnsureCharDataExistence();
			this._charData.Add(new CharData(inputData));
			if(ShowRegistrations)
				GD.Print("DataManagement/ Chars " + _charData[_charData.Count - 1].CharName + " added.");
		}
		else 
		{
			EnsureActualManagement();
			_actualManager.AddCharData(inputData);
		}
	}

	public void AddItemData(ItemDataDefinition newItem) {
		if (IsActualManager) 
		{
			EnsureItemCountListExists();
			EnsureItemDictExists();
			int keyID = GetItemKey(newItem);
			this._itemData.Add(keyID, new ItemData(newItem));
			if(ShowRegistrations)
				GD.Print("DataManagement/ Item " + keyID + " " + TranslationServer.Translate(newItem.ItemName) + " added.");
		}
		else 
		{
			EnsureActualManagement();
			_actualManager.AddItemData(newItem);
		}
	}

	private int GetItemKey(ItemDataDefinition newItem)
	{
		int itemID = (int)newItem.ItemType;
		int finalID =  itemID * 10;
		string itemKey = newItem.ItemType.ToString();
		int[] tempCounter = _itemCountList[itemKey];
		int referenceID = 0;

		if (newItem.ItemType == Enums.ItemType.Accessoires)
		{
			referenceID = (int)newItem.AccessoiresType - 1;
		}
		else if (newItem.ItemType == Enums.ItemType.Weapon)
		{
			referenceID = (int)newItem.WeaponType - 1;
		}
		else if (newItem.ItemType == Enums.ItemType.Armor)
		{
			referenceID = (int)newItem.ArmorType - 1;
		}
		else if (newItem.ItemType == Enums.ItemType.Shield)
		{			
			referenceID = (int)newItem.ShieldTyp - 1;
		}
		else if (newItem.ItemType == Enums.ItemType.Craft)
		{			
			referenceID = (int)newItem.CraftType - 1;
		}
		finalID += referenceID;
		finalID *= 1000;
		tempCounter[referenceID]++;
		finalID += tempCounter[referenceID];

		return finalID;
	}

	private void EnsureCharDataExistence() => _charData ??= new List<CharData>();

	private void EnsureActualManagement() 
	{
		if (_actualManager == null)
			if (!IsActualManager)
				_actualManager = GetParent<DataManagement>();

	}

	private void EnsureItemDictExists() => _itemData ??= new Dictionary<int, ItemData>();

	private void EnsureItemCountListExists() 
	{
		if (_itemCountList == null) {
			_itemCountList = new Dictionary<string, int[]>();
			foreach (int i  in Enum.GetValues(typeof(Enums.ItemType))) 
			{
				string key = ((Enums.ItemType)i).ToString();
				if (key is "Quest" or "Usable") 
				{
					int[] tempList = new int[1];
					_itemCountList.Add(key, tempList);
				}
				else if (key == "Weapon")
				{
					int[] tempList = new int[Enum.GetNames(typeof(Enums.WeaponType)).Length -1 ];
					_itemCountList.Add(key, tempList);
				}
				else if (key == "Shield") 
				{
					int[] tempList = new int[Enum.GetNames(typeof(Enums.ShieldsType)).Length -1 ];
					_itemCountList.Add(key, tempList);
				}
				else if (key == "Armor") 
				{
					int[] tempList = new int[Enum.GetNames(typeof(Enums.ArmorType)).Length -1 ];
					_itemCountList.Add(key, tempList);
				}
				else if (key == "Accessoires") 
				{
					int[] tempList = new int[Enum.GetNames(typeof(Enums.AccessoiresType)).Length -1];
					_itemCountList.Add(key, tempList);
				}
				else if (key == "Craft") 
				{
					int[] tempList = new int[Enum.GetNames(typeof(Enums.CraftType)).Length -1];
					_itemCountList.Add(key, tempList);
				}
			}

		}
	}
}
