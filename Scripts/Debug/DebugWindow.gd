extends Node
class_name DebugWindowGD


var _lable :Label

var _messages :Dictionary


func _init() -> void:
	_messages = {}

func _ready() -> void:
	EventBusGD.debug_message.connect(set_debug_text)
	_lable = get_child(0)

func _process(delta :float) -> void:
	var lable_string :String
	var keys_to_remove :Array[String] = []
	for el in _messages:
		lable_string += _messages[el].debug_message + "\n"
		if _messages[el].change_time(delta):
			keys_to_remove.append(el)
			
		for key in keys_to_remove:
			_messages.erase(key)
		_lable.text = lable_string

func set_debug_text(msg :String, id :String) -> void:
	if id in _messages:
		_messages[id].update_message(msg)
	else:
		_messages[id] = DebugElementGD.new(msg)
