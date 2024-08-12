extends Node2D
class_name SystemControlGD

@export var viewport_pixels :int = 512
@export var max_resolution :int = 480

var _camera_projections :CameraProjectionsGD
var _motion_control :MotionControlGD
var _camera2d_system :Camera2DSystemGD

var pixel_factors :Vector3
var pan :float = 30.0
var _cam_rot_org :CameraRotationOrganizerGD
var _prev_scale :float = 0.0

var pixel_scale : float:
	get:
		return _prev_scale

func _init() -> void:
	_cam_rot_org = CameraRotationOrganizerGD.new()
	_prev_scale = -1.0
	
func _ready() -> void:
	ensure_preparation()

func _process(delta) -> void:
	set_screen_scale(max_resolution)
	update_actual_camera_rotation(delta)
	set_camera_positions()
	
#func hard_overwrite_camera(dx :float, dy :float) -> void:
#	var norm_val := Vector2(dx / 128.0, dy / 64.0)
#	var val = FuncXT.Vect2.rotate_by(norm_val, _cam_rot_org.rotation)
#	_cam_rot_org.hard_overwrite_origin(val + _cam_rot_org.origin)

func update_actual_camera_rotation(delta: float) -> void:
	var new_angle : float = _cam_rot_org.get_new_camera_angle(delta, _motion_control.camera_rotation_speed)
	if new_angle != _cam_rot_org.rotation:
		var relative_camera_world_pos :Vector2 = get_relative_camera_world_pos()
		_cam_rot_org.update_rotation_data(new_angle, relative_camera_world_pos)
		_camera_projections.set_rotation(new_angle)

func get_relative_camera_world_pos() -> Vector2:
	var x :float = _camera2d_system.position.x / 64.0 / _prev_scale
	var y :float = _camera2d_system.position.y / 64.0 / _prev_scale * pixel_factors.z
	return Vector2(x,y)
	
func set_camera_positions() -> void:
	var pos_x :float = _camera2d_system.position.x / _prev_scale
	var quadrantX1 : int = int(pos_x / viewport_pixels)
	var quadrantX2 : int = quadrantX1 - 1
	if int(pos_x) % viewport_pixels > 0:
		quadrantX2 += 2
		
	var pos_y :float = _camera2d_system.position.y / _prev_scale
	var quadrantY1 : int = int(pos_y / viewport_pixels)
	var quadrantY2 : int = quadrantY1 - 1
	if int(pos_y) % viewport_pixels > 0:
		quadrantY2 += 2
	
	_camera_projections.set_relative_position(quadrantX1, quadrantX2, quadrantY1, quadrantY2, _cam_rot_org)

func set_motion_control(mc :MotionControlGD) -> void:
	_motion_control = mc
	if _camera2d_system != null:
		_motion_control.set_2d_camera(_camera2d_system)

func set_camera_2d(cam :Camera2DSystemGD) -> void:
	_camera2d_system = cam
	if _motion_control != null:
		_motion_control.set_2d_camera(cam)

func set_pixel_factors() -> void:
	var horizontal :float = 1.0
	var vertical :float =  1.0 / cos(-pan / 180.0 * PI)
	var forward :float = -1.0 / sin(-pan / 180.0 * PI)
	pixel_factors = Vector3(horizontal, vertical, forward)
	_cam_rot_org.set_pixel_factor_y(pixel_factors.z)

func add_view_projection(vp :ViewProjectionGD) -> void:
	ensure_preparation()
	_camera_projections.set_next_view_projection(vp)

func add_camera_system(scs : SubCameraSystemGD) -> void:
	ensure_preparation()
	_camera_projections.set_next_camera(scs)

func ensure_preparation() -> void:
	set_pixel_factors()
	if _camera_projections == null:
		_camera_projections = CameraProjectionsGD.new(2,2)

func add_to_target_rotation(angle :float) -> void:
	_cam_rot_org.add_to_target_rotation(angle)
	
func set_screen_scale(max_pixels :int) -> void:
	var screen_size :Vector2 = DisplayServer.window_get_size()
	var size :int = max(screen_size.x, screen_size.y)
	
	var scale_factor :float = 1.0
	
	while (size / scale_factor > max_pixels):
		scale_factor += 1
	
	#Hard Overwrite
	#scale_factor = 1.0
	
	if _prev_scale != scale_factor:
		_camera2d_system.position = Vector2(_camera2d_system.position.x *scale_factor / _prev_scale, _camera2d_system.position.y * scale_factor / _prev_scale)
		EventBusGD.debug_message.emit("Scale changed to --> " + str(scale_factor), "scrn_scl")
#		print("Scale changed to --> " + str(scale_factor))
		_camera_projections.set_all_scales(scale_factor)
		#for i in range(_camera_projections.length0):
			#for j in range(_camera_projections.length1):
				#var cp := _camera_projections.get_projection_at(i,j)
				#if cp == null:
					#EventBusGD.debug_message.emit("/SystemControl.gd/ _camera_projections[i][j] == null", "scrn_001")
				#else:
					#cp.set_projection_scale(scale)
		_prev_scale = scale_factor
		_camera_projections.set_positions(int(scale_factor), pixel_factors.z, _cam_rot_org)
		EventBusGD.screen_scale_changed_to.emit(scale_factor)

