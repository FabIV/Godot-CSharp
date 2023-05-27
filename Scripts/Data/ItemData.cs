namespace RPG3D.General.Data;

using Godot;
using RPG3D.General;

public partial class ItemData
{
	//Overview
	public string ItemName { get; }
	public string Description  { get; }

	//Definition
	public Enums.ItemType ItemType  { get; }
	public Enums.WeaponType WeaponType  { get; }
	public Enums.ArmorType ArmorType  { get; }
	public Enums.ShieldsType ShieldTyp  { get; }
	public Enums.AccessoiresType AccessoiresType  { get; }
	public Enums.CraftType CraftType  { get; }
	
	//What it can do
	public int Attack { get; }
	public int Defense { get; }
	public int Strength { get; }
	public int Agilty { get; }
	public int Intelligence { get; }
	public int Wisdom { get; }

	public int IncreaseHitPoints { get; }
	public int IncreaseManaPoints { get; }
	
	//State
	public bool NoPoison { get; }
	public bool NoSleep { get; }
	public bool NoBlind { get; }
	
	//Actions
	public bool Revive { get; }
	
	public int IsFire  { get; }
	public int IsIce { get; }
	public int IsEarth { get; }
	public int IsWind { get; }
	
    // Called when the node enters the scene tree for the first time.
    public ItemData(ItemDataDefinition itemData) 
    {
	    ItemName = itemData.Name;
	    Description = itemData.Description;
	    ItemType = itemData.ItemType;
	    WeaponType = itemData.WeaponType;
	    ArmorType = itemData.ArmorType;
	    ShieldTyp = itemData.ShieldTyp;
	    AccessoiresType = itemData.AccessoiresType;
	    CraftType = itemData.CraftType;
	    Attack = itemData.Attack;
	    Defense = itemData.Defense;
	    Strength = itemData.Strength;
	    Agilty = itemData.Agilty;
	    Intelligence = itemData.Intelligence;
	    Wisdom = itemData.Wisdom;
	    IncreaseHitPoints = itemData.IncreaseHitPoints;
	    IncreaseManaPoints = itemData.IncreaseManaPoints;
	    NoPoison = itemData.NoPoison;
	    NoSleep = itemData.NoSleep;
	    NoBlind = itemData.NoBlind;
	    Revive = itemData.Revive;
	    IsFire = itemData.IsFire;
	    IsIce = itemData.IsIce;
	    IsEarth = itemData.IsEarth;
	    IsWind = itemData.IsWind;
    }

}
