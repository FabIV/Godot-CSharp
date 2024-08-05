extends Node
class_name MotionControlGD

@export var camera_speed :float = 100.0
@export var camera_rotation_speed : float = 5.0

var motion_mode :Enums.MotionMode
var _camera :Camera2DSystemGD
var _system :SystemControlGD

func _ready() -> void:
	motion_mode = Enums.MotionMode.FREE_CONTROL
	_system = get_parent()
	_system.set_motion_control(self)

func _process(delta) -> void:
	var horizontal :float = Input.get_action_strength("move_right") - Input.get_action_strength("move_left")
	var vertical :float   = Input.get_action_strength("move_up") - Input.get_action_strength("move_down")
	var action :float     = Input.get_action_strength("UpAndDown")
	var action_just_pressed :bool = Input.is_action_just_pressed("UpAndDown")
	var shift :float     = Input.get_action_strength("Shift")
	
	if motion_mode == Enums.MotionMode.FREE_CONTROL:
		if shift > 0.5: #höhenänderungs modus
			if Input.is_action_just_pressed("mouse_left"):
				_system.add_to_target_rotation(PI / 4.0)
			elif Input.is_action_just_pressed("mouse_right"):
				_system.add_to_target_rotation(-PI / 4.0)
		else:
			_camera.move_local_x(horizontal * camera_speed * delta * _system.pixel_scale)
			_camera.move_local_y(-vertical * camera_speed * delta * _system.pixel_scale)

func set_2d_camera(cam :Camera2DSystemGD) -> void:
	_camera = cam
