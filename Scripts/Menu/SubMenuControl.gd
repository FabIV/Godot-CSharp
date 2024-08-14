extends Control
class_name SubMenuControl

signal sub_menu_activated()
signal sub_menu_deactivated()

func activate() -> void:
	sub_menu_activated.emit()
	
func deactivate() -> void:
	sub_menu_deactivated.emit()
	
