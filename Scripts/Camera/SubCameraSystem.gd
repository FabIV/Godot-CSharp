extends SubViewport
class_name SubCameraSystemGD

var _position : Node3D
var _rotation : Node3D
var _pan : Node3D
var _distance : Node3D
var _camera : Camera3DSystemGD

# Called when the node enters the scene tree for the first time.
func _ready():
	_position = get_child(0)
	_rotation = _position.get_child(0)
	_pan = _rotation.get_child(0)
	_distance = _pan.get_child(0)
	_camera = _distance.get_child(0)
	
	_camera.Size = 8.0 * self.size.x / 512.0
	
	var world : Node3D = get_parent()
	var sys : SystemControlGD = world.get_parent()
	sys.add_camera_system(self)

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func set_position(new_position :Vector3) -> void:
	_position.position = new_position

func set_rotation(angle :float) -> void:
	_rotation.rotate_y(angle - _rotation.rotation.y)
	
