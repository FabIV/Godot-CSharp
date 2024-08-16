extends Control
class_name SubMenuControl

signal sub_menu_activated()
signal sub_menu_deactivated()

var _previous_sub_menu :SubMenuControl

func activate(prev_sub_menu : SubMenuControl) -> void:
	_previous_sub_menu = prev_sub_menu
	sub_menu_activated.emit()
	
func deactivate() -> void:
	sub_menu_deactivated.emit()
	
