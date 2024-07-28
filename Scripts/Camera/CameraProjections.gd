extends Object

class_name CameraProjectionsGD

var _camera_projections :Array[Array]

var _next_viewport_pos :int = 0
var _next_camera_pos :int = 0

var _stored_scaler :int = 1
var _stored_pixel_y :float = 1.0

var length0 :int :
	get:
		return _camera_projections.size()
	#set:
		#pass

var length1 :int :
	get:
		return _camera_projections[0].size()
	
func get_projection_at(i :int, j :int) -> CameraProjectionGD:
	return _camera_projections[i][j]

func get_array_position(mat :Array[Array], position :int) -> Array[int]:
	var rows :int = mat.size()
	var cols :int = mat[0].size()
	
	var c :int = position % cols
	var r :int = (position - c) / cols
	
	return [r, c]

func set_next_camera(cam :SubCameraSystemGD) -> void:
	var i : Array[int] = get_array_position(_camera_projections, _next_camera_pos)
	_camera_projections CameraProjectionGD.new(i)
	
	pass
