extends Object
class_name CameraRotationOrganizerGD

var target_rotation : float
var _previous_rotation :float
var rotation :float

var origin :Vector2

var x_part :Vector2
var y_part :Vector2

var _pixel_factor_y :float

const CIRCLE     :float = PI * 2.0
const TWO_CIRCLE :float = PI * 4.0

func _init() -> void:
	_previous_rotation = -1.0
	origin = Vector2(0.0, 0.0)
	update_rotation_data(0.0, Vector2(0.0, 0.0))
	_pixel_factor_y = 2.0

func hard_overwrite_origin(val :Vector2) -> void:
	origin = val

func add_to_target_rotation(val :float) -> void:
	target_rotation += val
	compensate_too_big_angle_differences()
	
func compensate_too_big_angle_differences() -> void:
	if target_rotation > rotation + CIRCLE:
		target_rotation -= CIRCLE
	elif target_rotation < rotation - CIRCLE:
		target_rotation += CIRCLE
		
func get_new_camera_angle(delta_time :float, speed :float) -> float:
	var delta_angles :float = target_rotation - rotation
	var final_delta :float = min((speed * delta_time), abs(delta_angles)) * sign(delta_angles)
	var new_angle :float = final_delta + rotation
	
	return new_angle

func set_pixel_factor_y(val :float) -> void:
	_pixel_factor_y = val

func update_rotation_data(angle :float, relative_camera_world_pos :Vector2) -> void:
	#var init_rel_camera_world_pos = Vector2(relative_camera_world_pos.x, relative_camera_world_pos.y)
	var abs_world_rotation_point := FuncXT.Vect2.rotate_by(relative_camera_world_pos, rotation) + origin
	
	var rotated_camera_world_pos := FuncXT.Vect2.rotate_by(relative_camera_world_pos, angle)
	
	origin = abs_world_rotation_point - rotated_camera_world_pos 
	
	rotation = angle
	
	update_internal_values(rotated_camera_world_pos)
	
	_previous_rotation = rotation
	
func update_internal_values(rotation_point :Vector2) -> void:
	x_part = FuncXT.Vect2.rotate_by(Vector2(1.0, 0.0), rotation)
	y_part = FuncXT.Vect2.rotate_by(Vector2(0.0, 1.0), rotation)
