extends Node
class_name DataManagement

@export var is_actual_manager :bool = false
@export var show_registrations :bool = false
@export var show_warning :bool = false

var _char_data :Array[CharData]
var _item_data :Dictionary #keys of int values ItemData
var _item_count_list :Dictionary  # keys string values Array[int]
#private Dictionary<int, ItemData> _itemData;
#private Dictionary<string, int[]>  _itemCountList;
var _actual_manager : DataManagement

func _ready() -> void:
	#base.ready()
	if is_actual_manager:
		ensure_item_dict_exists()
		ensure_item_count_list_exists()
		ensure_char_data_exists()
		_actual_manager = self
	else:
		ensure_actual_management()
	
func add_char_data(input_data : CharDataDefinition) -> void:
	if is_actual_manager:
		await EventBus.last_loaded
		ensure_char_data_exists()
		_char_data.append(CharData.new(input_data))
		if show_registrations:
#			var msg :String = "DataManagement/Chars " + _char_data[_char_data.size() - 1].char_name + " added.".format({"name": })
			var msg :String = "DataManagement/Chars {name} added.".format({"name": _char_data[_char_data.size() - 1].char_name})
			var msg_id :String = "chr_reg"+str(randi()%9999)
#			print("DataManagement/Chars " + _char_data[_char_data.size() - 1].char_name + " added.")
			EventBus.debug_message.emit(msg, msg_id)
	else:
		ensure_actual_management()
		_actual_manager.add_char_data(input_data)
			
func add_item_data(new_item : ItemDataDefinition) -> void:
	if is_actual_manager:
		await EventBus.last_loaded
		ensure_item_count_list_exists()
		ensure_item_dict_exists()
		var key_id :int =  get_item_key(new_item)
		_item_data[key_id] = ItemData.new(new_item)
		if show_registrations:
#			print("DataManagement/ Item " + str(key_id) + " " + TranslationServer.translate(new_item.item_name) + " added.")
#			EventBus.debug_message.emit("DataManagement/ Item " + str(key_id) + " " + TranslationServer.translate(new_item.item_name) + " added.", "itm_reg"+str(randi()))
			var msg :String = "DataManagement/Item {id} {name} added.".format({"id": key_id, "name": TranslationServer.translate(new_item.item_name)})
			var msg_id :String = "itm_reg"+str(randi()%99999)
			EventBus.debug_message.emit(msg, msg_id)
	else:
		ensure_actual_management()
		_actual_manager.add_item_data(new_item)

func get_item_key(new_item : ItemDataDefinition) -> int:
	var item_id  :int = int(new_item.item_type)
	var final_id :int = item_id * 10
	#var item_key :String = str(new_item.item_type)
	var item_key :String = Enums.ItemType.keys()[new_item.item_type]
	#var item_key :int = new_item.item_type
	
	ensure_item_count_list_exists()
		
	var temp_counter :Array[int] = _item_count_list[item_key]
	var ref_id : int = 0
	
	if new_item.item_type == Enums.ItemType.ACCESSOIRES:
		ref_id = int(new_item.accessoires_type) -1
	elif new_item.item_type == Enums.ItemType.WEAPON:
		ref_id = int(new_item.weapon_type) -1
	elif new_item.item_type == Enums.ItemType.ARMOR:
		ref_id = int(new_item.armor_type) -1
	elif new_item.item_type == Enums.ItemType.SHIELD:
		ref_id = int(new_item.shields_type) -1
	elif new_item.item_type == Enums.ItemType.CRAFT:
		ref_id = int(new_item.craft_type) -1
	
	final_id += ref_id
	final_id *= 1000
	temp_counter[ref_id] += 1
	final_id += temp_counter[ref_id]
	
	return final_id
	
func ensure_char_data_exists() -> void:
	if _char_data == null:
		_char_data = []
		
func ensure_actual_management() -> void:
	if _actual_manager == null:
		if not is_actual_manager:
			_actual_manager = get_parent() 

func ensure_item_dict_exists() -> void:
	_item_data = {}

func ensure_item_count_list_exists() -> void:
	if _item_count_list == null or _item_count_list.size() == 0:
		_item_count_list = {}
		for key in Enums.ItemType.keys():
			if key == "QUEST" or key == "USABLE":
				var temp :Array[int] = [0]
				_item_count_list[key] = temp
			elif key == "WEAPON":
				var temp :Array[int] = []
				for sub_key in Enums.WeaponType.keys():
					temp.append(0)
				_item_count_list[key] = temp
			elif key == "SHIELD":
				var temp :Array[int] = []
				for sub_key in Enums.ShieldsType.keys():
					temp.append(0)
				_item_count_list[key] = temp
			elif key == "ARMOR":
				var temp :Array[int] = []
				for sub_key in Enums.ArmorType.keys():
					temp.append(0)
				_item_count_list[key] = temp
			elif key == "ACCESSOIRES":
				var temp :Array[int] = []
				for sub_key in Enums.AccessoiresType.keys():
					temp.append(0)
				_item_count_list[key] = temp
			elif key == "CRAFT":
				var temp :Array[int] = []
				for sub_key in Enums.CraftType.keys():
					temp.append(0)
				_item_count_list[key] = temp
