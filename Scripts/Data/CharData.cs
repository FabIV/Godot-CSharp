using System;
using Godot;
using RPG3D.General;
namespace RPG3D.General.Data;

public partial class CharData 
{
    private string _charName;
    public string CharName => _charName;
    private Enums.CharStyle _charStyle;
    public Enums.CharStyle CharStyle => _charStyle;
    private Enums.CharClass _charClass;
    public Enums.CharClass Class => _charClass;
    private Enums.WeaponType _weapon;
    public Enums.WeaponType Weapon => _weapon;

    private int _hp;
    public int HitPoints => _hp;
    private int _hpMax;
    public int HitPointsMax => _hpMax;  
    private int _hpTempMax;
    public int HitPointsTempMax => _hpTempMax;
    private int _mp;
    public int ManaPoints => _mp;
    private int _mpMax;
    public int ManaPointsMax => _mpMax;
    private int _mpTempMax;
    public int ManaPointsTempMax => _mpTempMax;
    private int _strength;
    public int Strength => _strength;
    private int _agility;
    public int Agility => _agility;
    private int _intelligence;
    public int Intelligence => _intelligence;
    private int _wisdom;
    public int Wisdom => _wisdom;

    // Called when the node enters the scene tree for the first time.
    public CharData(CharDataDefinition inputData) 
    {
        _charName = inputData.CharName;
        _charStyle = inputData.CharStyle;
        _charClass = inputData.Class;
        _weapon = inputData.Weapon;
        _hp = inputData.HitPoints;
        _hpMax = inputData.HitPoints;
        _hpTempMax = inputData.HitPoints;
        _mp = inputData.ManaPoints;
        _mpMax = inputData.ManaPoints;
        _mpTempMax = inputData.ManaPoints;
        _strength = inputData.Strength;
        _agility = inputData.Agility;
        _intelligence = inputData.Intelligence;
        _wisdom = inputData.Wisdom;

    }
}
