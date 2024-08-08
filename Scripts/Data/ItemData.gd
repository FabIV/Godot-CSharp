extends Object
class_name ItemDataGD


var item_name :String
var description :String
#definition
var item_type : Enums.ItemType
var weapon_type :Enums.WeaponType
var armor_type : Enums.ArmorType
var shields_type :Enums.SchieldsType
var accessoires_type :Enums.AccessoiresType
var craf_type :Enums.CraftType

#What it can do
var attack :int
var defense :int
var strength :int
var agility :int
var intelligence :int
var wisdom :int

var increase_hp :int
var increase_mp :int

var no_poison :bool
var no_sleep :bool
var no_blind :bool

var revive :bool
	
var fire :int
var ice :int
var earth :int
var wind :int
	
func _init(item_data :ItemDataDefinitionGD) -> void:
	item_name = item_data.item_name
	description = item_data.descrption
	item_type = item_data.item_type
	weapon_type = item_data.weapon_type
	shields_type = item_data.shields_type
	accessoires_type = item_data.accessoires_type
	craft_type = item_data.craft_type
	
	attack = item_data.attack
	defense = item_data.defense
	strength = item_data.strength
	agility = item_data.agility
	intelligence = item_data.intelligence
	wisdom = item_data.wisdom
	increase_hp = item_data.increase_hp
	increase_mp = item_data.increase_mp
	no_poison = item_data.no_poisen
	no_sleep = item_data.no_sleep
	no_blind = item_data.no_blind
	revive = item_data.revive
	fire =item_data.fire
	ice = item_data.ice
	earth = item_data.earth
	wind = item_data.wind
	
