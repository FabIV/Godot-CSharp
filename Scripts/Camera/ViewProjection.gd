extends Sprite2D
class_name ViewProjectionGD


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	var sys :SystemControlGD = get_parent()
	sys.add_view_projection(self)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta) -> void:
	pass

func set_scale_float(new_scale :float) -> void:
	self.scale = Vector2(new_scale, new_scale)
