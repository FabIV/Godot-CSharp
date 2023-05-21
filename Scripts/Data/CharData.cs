using System;
using Godot;
using RPG3D.General;
namespace RPG3D.General.Data;


public partial class CharData {
    private string _charName;
    public string CharName => _charName;
    private Enums.CharStyle _charStyle;
    public Enums.CharStyle CharStyle => _charStyle;
    private Enums.CharClass _charClass;
    public Enums.CharClass Class => _charClass;
    private Enums.WeaponType _weapon;
    public Enums.WeaponType Weapon => _weapon;

    public int HitPoints;
    public int ManaPoints;
    public int Strength;
    public int Agility;
    public int Intelligence;

    public int Wisdom;

    // Called when the node enters the scene tree for the first time.
    public CharData(CharDataSimple inputData) {
        _charName = inputData.CharName;
        _charStyle = inputData.CharStyle;
        _charClass = inputData.Class;
        _weapon = inputData.Weapon;
        GD.Print("CharData/  fehlende Uebertragungen");
    }
}
