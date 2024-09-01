extends Node
class_name InputControl


var _latest_input : InputContainer
#var _camera_is_blocked :bool


var move_right : float = 0.0
var move_left : float = 0.0
var move_up : float = 0.0
var move_down : float = 0.0

# Called when the node enters the scene tree for the first time.
func _init() -> void:
	_latest_input = InputContainer.new(0.0, 0.0)

func _ready() -> void:
	pass

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta :float) -> void:
	if GameStatus.input_motion == Enums.InputMotion.INPUT_ALLOWED:
		move_right = Input.get_action_raw_strength("move_right")
		move_left  = Input.get_action_raw_strength("move_left")
		move_up    = Input.get_action_raw_strength("move_up")
		move_down  = Input.get_action_raw_strength("move_down")
		_latest_input.set_values(move_right - move_left, move_up - move_down)

	EventBus.player_motion_data.emit(_latest_input.horizontal, _latest_input.vertical)

	if Input.is_action_just_pressed("pause_menu"):
		if GameStatus.game_status == Enums.GameStatus.NORMAL:
			EventBusMenu.open_main_menu.emit()
		elif GameStatus.game_status == Enums.GameStatus.MENU:
			EventBusMenu.ui_back.emit()

