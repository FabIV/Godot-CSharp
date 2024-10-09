extends Control
class_name TitelManager
var _titel_msg : Array[TitelMessages] = []
var active_titel_msg : int            = 0
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	EventBusMenu.set_next_titel.connect(_set_next_titel)

func _set_next_titel(text :String) -> void:
	EventBusMenu.close_current_titel.emit()
	active_titel_msg += 1
	active_titel_msg %= _titel_msg.size()
	
	_titel_msg[active_titel_msg].activate_me(text)

func add_titel_messages(tm : TitelMessages) -> void:
	_titel_msg.append(tm)