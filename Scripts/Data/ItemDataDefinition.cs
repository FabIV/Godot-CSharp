using Godot;
using System;
using System.Collections.Generic;
using System.Dynamic;
using RPG3D.General;


public partial class ItemDataDefinition : Node
{
    //Overview
    [Export] public string ItemName = "no name";
    [Export] public string Description = "no description" ;
    
    public Enums.ItemType ItemType = Enums.ItemType.Usable;
    public Enums.WeaponType WeaponType = Enums.WeaponType.NoType;
    public Enums.ArmorType ArmorType = Enums.ArmorType.NoType;
    public Enums.ShieldsType ShieldTyp = Enums.ShieldsType.NoType;
    public Enums.AccessoiresType AccessoiresType = Enums.AccessoiresType.NoType;
    public Enums.CraftType CraftType = Enums.CraftType.NoType;
    
    //What it can do
    [Export] public int Attack = 0;
    [Export] public int Defense = 0;
    [Export] public int Strength = 0;
    [Export] public int Agilty = 0;
    [Export] public int Wisdom = 0;
    [Export] public int Intelligence = 0;

    [Export] public int IncreaseHitPoints = 0;
    [Export] public int IncreaseManaPoints = 0;

    //State
    [Export] public bool NoPoison = false;
    [Export] public bool NoSleep = false;
    [Export] public bool NoBlind = false;

    //Actions
    [Export] public bool Revive = false;

    [Export] public int IsFire = 0;
    [Export] public int IsIce = 0;
    [Export] public int IsEarth = 0;
    [Export] public int IsWind = 0;

    public void SetItemSpecification(Enums.ItemType item, Enums.WeaponType weapon, Enums.ShieldsType shield , 
                                    Enums.ArmorType armor, Enums.AccessoiresType accessoires, Enums.CraftType craft) {
        ItemType = item;
        WeaponType = weapon;
        ShieldTyp = shield;
        ArmorType = armor;
        AccessoiresType = accessoires;
        CraftType = craft;
    }

}