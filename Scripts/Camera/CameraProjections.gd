extends Object

class_name CameraProjections

var _camera_projections :Array[Array] #of type _camera_projection

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
	
func _init(i :int, j :int) -> void:
	_camera_projections = []
	for k in range(i):
		var temp_arr: Array[CameraProjection] = []
		for l in range(j):
			temp_arr.append(null)
		_camera_projections.append(temp_arr)
		
	
#func get_projection_at(i :int, j :int) -> CameraProjectionGD:
#	return _camera_projections[i][j]

func get_array_position(mat :Array[Array], position :int) -> Array[int]:
#	var rows :int = mat.size()
	var cols :int = mat[0].size()
	
	var c :int = position % cols
	var r :int = (position - c) / cols
	
	return [r, c]

func set_next_camera(cam : SubCameraSystem) -> void:
	var i : Array[int] = get_array_position(_camera_projections, _next_camera_pos)
	if _camera_projections[i[0]][i[1]] == null:
		_camera_projections[i[0]][i[1]] = CameraProjection.new(i)
	_camera_projections[i[0]][i[1]].set_camera(cam)
	_next_camera_pos += 1
	
func set_next_view_projection(vp : ViewProjection) -> void:
	var i : Array[int] = get_array_position(_camera_projections, _next_viewport_pos)
	if _camera_projections[i[0]][i[1]] == null:
		_camera_projections[i[0]][i[1]] = CameraProjection.new(i)
	var cp : CameraProjection = _camera_projections[i[0]][i[1]]
	cp.set_projection(vp)
	_next_viewport_pos += 1
	
func set_positions(scale :int, pixel_y :float, cro : CameraRotationOrganizer) -> void:
	for i in range(len(_camera_projections)):
		for j in range(len(_camera_projections[i])):
			var cp : CameraProjection = _camera_projections[i][j]
			cp.set_positions(scale, pixel_y, cro)
	_stored_scaler = scale
	_stored_pixel_y = pixel_y

func set_rotation(inverted_angle :float) -> void:
	for i in range(len(_camera_projections)):
		for j in range(len(_camera_projections[i])):
			var cp : CameraProjection = _camera_projections[i][j]
			cp.set_camera_rotation(-inverted_angle)

func set_relative_position(x1 :int, x2 :int, y1 :int, y2 :int, cro : CameraRotationOrganizer) -> void:
	var x := OrderSorter.new(x1, x2)
	var y                       := OrderSorter.new(y1, y2)
	var cp00 : CameraProjection =  _camera_projections[0][0]
	cp00.set_relative_position(x.n1, y.n1)
	var cp10 : CameraProjection = _camera_projections[1][0]
	cp10.set_relative_position(x.n1, y.n2)
	var cp01 : CameraProjection = _camera_projections[0][1]
	cp01.set_relative_position(x.n2, y.n1)
	var cp11 : CameraProjection = _camera_projections[1][1]
	cp11.set_relative_position(x.n2, y.n2)
	
	for i in range(len(_camera_projections)):
		for j in range(len(_camera_projections[i])):
			var cp : CameraProjection = _camera_projections[i][j]
			cp.set_positions(_stored_scaler, _stored_pixel_y, cro)

func set_all_scales(new_scale :float) -> void:
	for i in range(_camera_projections.size()):
		for j in range(_camera_projections[i].size()):
			var cp : CameraProjection = _camera_projections[i][j]
			cp.set_projection_scale(new_scale)
