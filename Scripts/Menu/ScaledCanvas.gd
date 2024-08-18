extends CanvasLayer
class_name ScaledCanvas

@export var scale_factor : float = 1.0
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	EventBus.screen_scale_changed_to.connect(set_new_scale)	

func set_new_scale(new_scale :int) -> void:
#	scale.x = new_scale * scale_factor
#	scale.y = new_scale * scale_factor
	for child in get_children():
		if child is SubMenuControl:
			child.set_new_scale(new_scale)
		elif child is MainMenu:
			child.set_new_scale(new_scale)
