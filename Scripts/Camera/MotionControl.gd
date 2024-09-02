extends Node
class_name MotionControl

@export var camera_speed :float = 100.0
@export var camera_rotation_speed : float = 5.0

#var motion_mode :Enums.MotionMode
var _camera : Camera2DSystem
var _system : SystemControl

var horizontal : float = 0.0
var vertical : float = 0.0
var shift : float = 0.0


func _ready() -> void:
#	motion_mode = Enums.MotionMode.FREE_CONTROL
	_system = get_parent()
	_system.set_motion_control(self)

func _process(delta) -> void:
#	if motion_mode == Enums.MotionMode.FREE_CONTROL:
	if GameStatus.input_motion == Enums.InputMotion.INPUT_ALLOWED:
		horizontal = Input.get_action_strength("move_right") - Input.get_action_strength("move_left")
		vertical   = Input.get_action_strength("move_up") - Input.get_action_strength("move_down")
		shift      = Input.get_action_strength("Shift")
		
		if shift > 0.5: #höhenänderungs modus
			if Input.is_action_just_pressed("move_left"):
				_system.add_to_target_rotation(PI / 4.0)
			elif Input.is_action_just_pressed("move_right"):
				_system.add_to_target_rotation(-PI / 4.0)
			
	if shift < 0.5:
		_camera.move_local_x(horizontal * camera_speed * delta * _system.pixel_scale)
		_camera.move_local_y( -vertical * camera_speed * delta * _system.pixel_scale)
		
func set_2d_camera(cam : Camera2DSystem) -> void:
	_camera = cam
