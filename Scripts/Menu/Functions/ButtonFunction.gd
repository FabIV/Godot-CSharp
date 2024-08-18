extends Node
class_name ButtonFunction


func connect_function_to_button(cb : CustomButton) -> void:
	cb.pressed.connect(call_function)
	
func call_function() -> void:
	custom_function()

func custom_function() -> void:
	EventBus.debug_message.emit("no function in button", "error" + str(randi()%99999))
	
