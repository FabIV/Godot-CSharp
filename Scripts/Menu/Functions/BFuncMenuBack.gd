extends ButtonFunction
class_name BFuncMenuBack

func custom_function() -> void:
	EventBusMenu.ui_back.emit()
