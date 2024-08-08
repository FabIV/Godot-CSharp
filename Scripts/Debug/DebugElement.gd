extends Object
class_name DebugElementGD

const MESSAGE_TIME :float = 3.0

var _time :float
var debug_message : String

func _init(msg :String) -> void:
	_time = MESSAGE_TIME
	debug_message = msg
	

func change_time(delta :float) -> bool:
	_time -= delta
	return _time < 0.0


func update_messate(new_msg :String) -> void:
	_time = MESSAGE_TIME
	debug_message = new_msg
