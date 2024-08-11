extends Node
class_name InputControlGD


var _latest_input :InputContainerGD
var _camera_is_blocked :bool


# Called when the node enters the scene tree for the first time.
func _init() -> void:
	_latest_input = InputContainerGD.new(0.0, 0.0)
	
func _ready() -> void:
	pass

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta) -> void:
	if GameStatusGD.input_motion == Enums.InputMotion.INPUT_ALLOWED:
		var move_right :float = Input.get_action_raw_strength("move_right")
		var move_left  :float = Input.get_action_raw_strength("move_left")
		var move_up    :float = Input.get_action_raw_strength("move_up")
		var move_down  :float = Input.get_action_raw_strength("move_down")
		_latest_input.set_values(move_right - move_left, move_up - move_down)
	
	EventBusGD.player_motion_data.emit(_latest_input.horizontal, _latest_input.vertical)
	
	if Input.is_action_just_pressed("pause_menu"):
		if GameStatusGD.game_status == Enums.GameStatus.NORMAL:
			EventBusMenuGD.open_main_menu.emit()
		elif GameStatusGD.game_status == Enums.GameStatus.MENU:
			EventBusMenuGD.ui_back.emit()
		
