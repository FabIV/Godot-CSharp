extends Object
class_name CameraProjection

var ready :bool
var has_view_projection :bool :
	get:
		return view_projection != null
		
var has_camera_system :bool :
	get:
		return camera_system != null

var view_projection : ViewProjection
var camera_system : SubCameraSystem

var relative_positions : IntXY

func _init(pos :Array[int]) -> void:
	relative_positions = IntXY.new(pos[0], pos[1])
	view_projection = null
	camera_system = null
	
#func init_view_projection(vp :ViewProjectionGD, pos :Array[int]) -> void:
	#view_projection = vp
	#relative_positions = IntXYGD.new(pos[0], pos[1])
#
#func init_camera_system(cs :SubCameraSystemGD, pos :Array[int]) -> void:
	#camera_system = cs
	#relative_positions = IntXYGD.new(pos[0], pos[1])

func set_projection_scale(new_scale : float) -> void:
	view_projection.set_scale_float(new_scale)

func set_camera_rotation(angle :float) -> void:
	camera_system.set_rotation(angle)
	
func set_camera(cs : SubCameraSystem) -> void:
	camera_system = cs
	if view_projection != null:
		ready = true

func set_projection(vp : ViewProjection) -> void:
	view_projection = vp
	if camera_system != null:
		ready = true

func set_relative_position(x :int, y :int) -> void:
	relative_positions.set_xy(x,y)
	
func set_positions(scaler :int, pixel_y :float, cro : CameraRotationOrganizer) -> void:
	view_projection.position = Vector2(relative_positions.x * 512.0 * scaler, relative_positions.y * 512.0 * scaler)
	
	var new_x :float = cro.origin.x + (relative_positions.x * cro.x_part.x           + relative_positions.y * cro.y_part.x * pixel_y) * 8.0
	var new_y :float = cro.origin.y + (relative_positions.y * cro.y_part.y * pixel_y + relative_positions.x * cro.x_part.y ) * 8.0
	
	camera_system.set_position(Vector3(new_x, 0.0, new_y))
