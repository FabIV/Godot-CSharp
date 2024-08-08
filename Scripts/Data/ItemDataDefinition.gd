extends Node
class_name ItemDataDefinitionGD

@export var item_name :String = "no name"
@export var descrption :String = "no description"

var item_type :Enums.ItemType = Enums.ItemType.USABLE
var weapon_type :Enums.WeaponType = Enums.WeaponType.NO_TYPE
var armor_type :Enums.ArmorType = Enums.ArmorType.NO_TYPE
var shields_type :Enums.ShieldsType = Enums.ShieldsType.NO_TYPE
var accessoires_type :Enums.AccessoiresType = Enums.AccessoiresType.NO_TYPE
var craft_type :Enums.CraftType = Enums.CraftType.NO_TYPE

@export var attack :int = 0
@export var defense :int = 0
@export var strength :int = 0
@export var agility :int = 0
@export var wisdom :int = 0
@export var intelligence :int = 0
	
@export var increase_hp :int = 0
@export var increase_mp :int = 0

@export var no_poisen :bool = false
@export var no_sleep :bool = false
@export var no_blind :bool = false

@export var revive :bool = false

@export var fire :int = 0
@export var ice :int = 0
@export var earth :int = 0
@export var wind :int = 0

func set_item_specification(item :Enums.ItemType, weapon :Enums.WeaponType, shield :Enums.ShieldsType, armor :Enums.ArmorType, accessoires :Enums.AccessoiresType, craft :Enums.CraftType) -> void:
		item_type = item;
		weapon_type = weapon;
		shields_type = shield;
		armor_type = armor;
		accessoires_type = accessoires;
		craft_type = craft;


