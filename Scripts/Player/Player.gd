extends RigidBody3D
class_name PlayerGD

@export var player_type :Enums.PlayerType = Enums.PlayerType.NPC
@export var char_type :Enums.CharStyle = Enums.CharStyle.NONE
@export var motion_force :float = 30.0

var _game_status: GameStatusGD
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta) -> void:
	pass


func motion_functions(x :float, y :float) -> void:
	var direction :Vector2 = Vector2(x,y)
	direction = direction.normalized()
	direction = FunctionExtensions.Vect2.rotate_by_matrix(direction, _game_status.fixed_camera_rotation_matrix)
	self.apply_central_force(Vector3(direction.x, 0.0, direction.y) * motion_force)
