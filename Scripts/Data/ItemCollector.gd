extends Node
class_name ItemCollector

@export var item_type :Enums.ItemType = Enums.ItemType.USABLE
@export var weapon_type :Enums.WeaponType = Enums.WeaponType.NO_TYPE
@export var armor_type :Enums.ArmorType = Enums.ArmorType.NO_TYPE
@export var shields_type :Enums.ShieldsType = Enums.ShieldsType.NO_TYPE
@export var accessoires_type :Enums.AccessoiresType = Enums.AccessoiresType.NO_TYPE
@export var craft_type :Enums.CraftType = Enums.CraftType.NO_TYPE

var _data_management : DataManagement

func _ready() -> void:
	var path :String = get_item_data_path()
	_data_management = get_parent()
	var files := DirAccess.get_files_at(path)
	for file in files:
		var scene = load(path + file)
		var loaded_scene = scene.instantiate()
		var real_name :String = file.left(file.length() -5)
		#var real_name :String = file(0:file.length() -5)
		
		loaded_scene.name = real_name
		add_child(loaded_scene)
		loaded_scene.set_item_specification(item_type, weapon_type, shields_type, armor_type, accessoires_type, craft_type)
		_data_management.add_item_data(loaded_scene)
		#ItemDataDefinition converted = (ItemDataDefinition)loadedScene;
		#converted.SetItemSpecification(ItemType, WeaponType, ShieldTyp, ArmorType, AccessoiresType, CraftType);
		#_dataManagment.AddItemData(converted);
		loaded_scene.queue_free()

func get_item_data_path() -> String:
	var path : String
	if item_type == Enums.ItemType.USABLE or item_type == Enums.ItemType.QUEST:
		path = "".join(Enums.ItemType.keys()[item_type].capitalize().split(" "))
		
	elif item_type == Enums.ItemType.WEAPON:
		if not weapon_type == Enums.WeaponType.NO_TYPE:
			path += "".join(Enums.WeaponType.keys()[weapon_type].capitalize().split(" "))
	elif item_type == Enums.ItemType.ARMOR:
		if not armor_type == Enums.ArmorType.NO_TYPE:
			path = "".join(Enums.ArmorType.keys()[armor_type].capitalize().split(" "))
	elif item_type == Enums.ItemType.SHIELD:
		if not shields_type == Enums.ShieldsType.NO_TYPE:
			path = "".join(Enums.ShieldsType.keys()[shields_type].capitalize().split(" "))
	elif item_type == Enums.ItemType.ACCESSOIRES:
		if not accessoires_type == Enums.AccessoiresType.NO_TYPE:
			path = "".join(Enums.AccessoiresType.keys()[accessoires_type].capitalize().split(" "))
	elif item_type == Enums.ItemType.CRAFT:
		if not craft_type == Enums.CraftType.NO_TYPE:
			path = "".join(Enums.CraftType.keys()[craft_type].capitalize().split(" "))
	
		
	return "res://Data/" + path + "/"
	
