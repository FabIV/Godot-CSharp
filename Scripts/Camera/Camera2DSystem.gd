extends Camera2D
class_name Camera2DSystemGD


# Called when the node enters the scene tree for the first time.
func _ready():
	var sys : SystemControlGD = get_parent()
	sys.set_camera_2d(self)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass
