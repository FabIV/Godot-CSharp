extends Node
class_name GamePlayControl


@export var Language :Enums.Language = Enums.Language.DE


func _ready():
	var scale_factor : int = 4
	
	TranslationServer.set_locale(Enums.Language.keys()[Language].to_lower())
	var temp_size = DisplayServer.window_get_size()
	var temp_pos = DisplayServer.window_get_position()
	
	DisplayServer.window_set_size(temp_size * scale_factor)
	var new_pos = temp_pos - temp_size * (scale_factor -1) / 2
	DisplayServer.window_set_position(new_pos)
	
	
	


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
