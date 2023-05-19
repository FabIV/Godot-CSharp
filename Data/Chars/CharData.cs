using Godot;
using System;
using RPG3D.General;

public partial class CharData : Node
{
	[Export] public string CharName = "NoName";
	[Export] public Enums.CharStyle CharStyle = Enums.CharStyle.None;
	[Export] public Enums.CharClass Class;
	[Export] public Enums.WeaponType Weapon;

	[Export] public int HitPointa;
	[Export] public int ManaPoints;
	[Export] public int Strength;
	[Export] public int Agility;
	[Export] public int Intelligence;
	[Export] public int Wisdom;
	// Called when the node enters the scene tree for the first time.

}
