extends Control
class_name SubMenuControl

signal sub_menu_activated()
signal sub_menu_deactivated()

@export var titel_text : String = ""
func activate() -> void:
	sub_menu_activated.emit()
	EventBusMenu.set_next_titel.emit(titel_text)
	
	

func deactivate() -> void:
	sub_menu_deactivated.emit()
	
func set_new_scale(new_scale : int) -> void:
	self.scale
