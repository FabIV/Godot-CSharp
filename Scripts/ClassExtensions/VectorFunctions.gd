extends Object
class_name Vector2Functions

func rotate_by_matrix(vect :Vector2, matrix :Array[Array]) -> Vector2:
	var x :float = matrix[0][0] * vect.x - matrix[0][1] * vect.y
	var y :float = matrix[1][0] * vect.x + matrix[1][1] * vect.y
	return Vector2(x,y)

func rotate_by_deg(vect :Vector2, angle :float) -> Vector2:
	return rotate_by(vect, angle / 180.0 * PI)

func rotate_by(vect :Vector2, angle :float) -> Vector2:
	var rotation_matrix : Array[Array] = get_rotation_matrix(vect, angle)
	var x = rotation_matrix[0][0] * vect.x + rotation_matrix[0][1] * vect.y
	var y = rotation_matrix[1][0] * vect.x + rotation_matrix[1][1] * vect.y
	
	return Vector2(x,y)
	
func get_rotation_matrix(vect :Vector2, angle :float) -> Array[Array]:
	var matrix : Array[Array] = []
	var sub_matrix : Array[float] = []
	sub_matrix.append( cos(angle))
	sub_matrix.append(-sin(angle))
	matrix.append(sub_matrix)
	
	var sub_matrix2 : Array[float] = []
	sub_matrix2.append(-sub_matrix[1])
	sub_matrix2.append( sub_matrix[0])
	matrix.append(sub_matrix2)
	
	return matrix
