extends Camera2D
class_name Camera2DSystem


# Called when the node enters the scene tree for the first time.
func _ready():
	var sys : SystemControl = get_parent()
	sys.set_camera_2d(self)
	