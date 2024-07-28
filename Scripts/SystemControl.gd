extends Node2D
class_name SystemControlGD

@export var viewport_pixels = 512
@export var max_resolution = 480

var _camera_projections :CameraProjectionsGD
var _motion_control :MotionControlGD
var _camera2d_system :Camera2DSystemGD

var pixel_factors :Vector3

var pan :float = 30.0

#func _init() -> void:
	#pass
#
## Called when the node enters the scene tree for the first time.
#func _ready() -> void:
	#pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta :float) -> void:
	pass

func hard_overwrite_camera(dx :float, dy :float) -> void:
	pass

func update_actual_camera_rotation(delta :float) -> void:
	pass
	
func get_relative_camera_world_pos() -> Vector2:
	return Vector2(0.0, 0.0)

func set_camera_positions() -> void:
	pass
	
func set_motion_control(mc :MotionControlGD) -> void:
	pass

func set_camera2d(cam :Camera2DSystemGD) -> void:
	pass

func set_pixel_factors() -> void:
	pass

func add_view_projection(val :ViewProjectionGD) -> void:
	pass
	
func add_camera_system(sub_cam_system :SubCameraSystemGD) -> void:
	pass
	
func ensure_preparatio() -> void:
	pass

func add_to_target_rotation(angle :float) -> void:
	pass

func set_screen_scale(max :int) -> void:
	pass
