namespace RPG3D.General;

public class Enums
{
    public enum Language { DE, ENG, FR, IT }
    public enum CharStyle {None, MC01, MC02, MC03}
    public enum CharClass {Fighter, Healer, Mage, Thief}
    public enum MovedBy {Nothing, Event, Player}
    public enum Direction {D000, D045, D090, D135, D180, D225, D270, D315, Nothing}
    public enum InstructionType {None, Resolution, Test}
    public enum ItemType {Quest, Usable, Weapon, Shield, Armor, Accessoires, Craft, Invalid}
    public enum WeaponType {NoType, Sword, Bow, Mace, Axe}
    public enum ShieldsType{NoType, SmallShield , HugeShield}
     public enum ArmorType {NoType, Light, Heavy, Robes}
    public enum AccessoiresType {NoType, Ring, Amulet, Bracelet}
    public enum CraftType {NoType, Crystal, Herb, Element, Metal, Wood, Spirit}

    public enum Quest {None,XXX}

    public enum CharControlID {SomethingElse, Player1, Player2, Player3}

    public enum BattleMode {NoBattle, Initiated, Ongoing, DuringAttack, Paused}

    public enum GameStatus {Title, Menu, Normal, Battle, BattleMenue, Pause}

    public enum FadeInDirection {None, Top, TopLeft, Left, BottomLeft, Bottom, BottomRight, Right, TopRight}
}