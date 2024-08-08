extends Node
class_name ItemCollectorGD

@export var item_type :Enums.ItemType = Enums.ItemType.USABLE
@export var weapon_type :Enums.WeaponType = Enums.WeaponType.NO_TYPE
@export var armor_type :Enums.ArmorType = Enums.ArmorType.NO_TYPE
@export var shields_type :Enums.ShieldsType = Enums.ShieldsType.NO_TYPE
@export var accessoires_type :Enums.AccessoiresType = Enums.AccessoiresType.NO_TYPE
@export var craft_type :Enums.CraftType = Enums.CraftType.NO_TYPE

var _data_management :DataManagementGD

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
	var i_path : String = "res://Data/"
	if item_type == Enums.ItemType.USABLE or item_type == Enums.ItemType.QUEST:
		i_path += str(item_type)
	elif item_type == Enums.ItemType.WEAPON:
		if not weapon_type == Enums.WeaponType.NO_TYPE:
			i_path += str(weapon_type) 
	elif item_type == Enums.ItemType.ARMOR:
		if not armor_type == Enums.ArmorType.NO_TYPE:
			i_path += str(armor_type)
	elif item_type == Enums.ItemType.SHIELD:
		if not shields_type == Enums.ShieldsType.NO_TYPE:
			i_path += str(shields_type)
	elif item_type == Enums.ItemType.ACCESSOIRES:
		if not accessoires_type == Enums.AccessoiresType.NO_TYPE:
			i_path += str(accessoires_type)
	elif item_type == Enums.ItemType.CRAFT:
		if not craft_type == Enums.CraftType.NO_TYPE:
			i_path += str(craft_type)
	
	i_path += i_path + "/"
	
	return i_path
	
