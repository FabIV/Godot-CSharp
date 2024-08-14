extends Node
class_name DebugWindow

@export var max_displayed_elements :int = 10
var _lable :Label
var _messages :Dictionary


func _init() -> void:
	_messages = {}

func _ready() -> void:
	EventBus.debug_message.connect(set_debug_text)
	_lable = get_child(0)
	

func _process(delta :float) -> void:
	var lable_string :String = ""
	var keys_to_remove :Array[String] = []
	var elements :int = 0
	var converted_delta :float = delta / Engine.time_scale
	for el in _messages:
		elements += 1
		if elements == max_displayed_elements:
			break
		lable_string += _messages[el].debug_message + "\n"
		if _messages[el].change_time(converted_delta):
			keys_to_remove.append(el)
			
		for key in keys_to_remove:
			_messages.erase(key)
		_lable.text = lable_string

func set_debug_text(msg :String, id :String) -> void:
	if id in _messages:
		_messages[id].update_message(msg)
	else:
		_messages[id] = DebugElement.new(msg)

