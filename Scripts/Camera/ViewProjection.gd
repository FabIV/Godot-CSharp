extends Sprite2D
class_name ViewProjection


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	var sys : SystemControl = get_parent()
	sys.add_view_projection(self)
	var level : SystemControl = get_parent()
	for world in level.get_children():
		if world is Node3D:
			for projection in world.get_children():
				if projection is SubCameraSystem:
					var x = name.right(1)
					var y = projection.name.right(1)
					if name.right(1) == projection.name.right(1):
						self.texture.viewport_path = projection.get_path()
						return

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta) -> void:
	pass

func set_scale_float(new_scale :float) -> void:
	self.scale = Vector2(new_scale, new_scale)
