extends Object
class_name CharDataGD

var char_name :String
var char_style :Enums.CharStyle
var char_class :Enums.CharClass
var weapon_type :Enums.WeaponType

var hp :int
var hp_max :int
var hp_max_temp :int
var mp :int
var mp_max :int
var mp_max_temp :int

var strength :int
var agility :int
var intelligence :int
var wisdom :int


func _init(input_data :CharDataDefinitionGD) -> void:
	char_name = input_data.CharName
	char_style = input_data.CharStyle
	char_class = input_data.Class
	weapon_type = input_data.weapon_type
	hp = input_data.hp
	hp_max = input_data.hp
	hp_max_temp = input_data.hp
	mp = input_data.mp
	mp_max = input_data.mp
	mp_max_temp = input_data.mp
	strength = input_data.strength
	agility = input_data.agility
	intelligence = input_data.intelligence
	wisdom = input_data.wisdom
