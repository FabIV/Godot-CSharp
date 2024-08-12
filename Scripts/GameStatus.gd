extends Node

@export var _pause_transition_time :float = 0.5

var fixed_camera_rotation_matrix :Array[Array]
var _player_node :PlayerGD
var game_status :Enums.GameStatus = Enums.GameStatus.NORMAL
var pre_menu_status :Enums.GameStatus = Enums.GameStatus.NORMAL
#var _ui_timer : UITimer
var _current_delta_t :float
var _timer_pause : UITimer
var input_motion :Enums.InputMotion

var current_engine_speed :float = 1.0
var inverted_engine_speed :float = 1.0

func _init() -> void:
	var t :Array[float] = [0,0]
	fixed_camera_rotation_matrix.append(t)
	t = [0,0]
	fixed_camera_rotation_matrix.append(t)
	set_camera_rotation_matrix(0.0)

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	EventBusGD.player_is_set.connect(self.set_player_node)
	EventBusGD.need_player_node.connect(self.shout_player_node)
	connect_signals()
	
func connect_signals() -> void:
	EventBusMenuGD.open_main_menu.connect(self.set_status_to_main_menu)
	EventBusMenuGD.main_menu_got_closed.connect(self.main_menu_got_closed)

func set_status_to_main_menu() -> void:
	pre_menu_status = game_status
	game_status = Enums.GameStatus.MENU
	pause_game_in(1.0)

func pause_game_in(transition_time :float) -> void:
	input_motion = Enums.InputMotion.INPUT_FREEZED
	clear_pause_timer_if_necessary()
	_timer_pause = UITimer.new()
	add_child(_timer_pause)
	_timer_pause.wait_time = transition_time
	_timer_pause.timeout.connect(pause_game_now_and_kill_timer)
	_timer_pause.start()
	change_engine_speed_to(0.01, _pause_transition_time)

func clear_pause_timer_if_necessary() -> void:
	if _timer_pause != null:
		_timer_pause.queue_free()
		_timer_pause = null
		
func unpaus_game() -> void:
	unpause_game_in(_pause_transition_time)
	
func unpause_game_in(transition_time :float) -> void:
	clear_pause_timer_if_necessary()
	_timer_pause = UITimer.new()
	add_child(_timer_pause)
	_timer_pause.timeout.connect(unpause_game_now_and_kill_timer)
	_timer_pause.process_mode = Node.PROCESS_MODE_ALWAYS
	_timer_pause.start()
	change_engine_speed_to(1.0, transition_time)
		
func pause_game_now_and_kill_timer() -> void:
	get_tree().paused = true
	clear_pause_timer_if_necessary()
	EventBusGD.debug_message.emit("Game paused",  str(get_class()) + "001")

func unpause_game_now_and_kill_timer() -> void:
	unpause_game_now()
	clear_pause_timer_if_necessary()

func unpause_game_now() -> void:
	get_tree().paused = false;
	EventBusGD.debug_message.emit("Game unpaused",  str(get_class()) + "001")
	

func main_menu_got_closed() -> void:
	input_motion = Enums.InputMotion.INPUT_ALLOWED
	game_status = pre_menu_status
	unpause_game_now_and_kill_timer()
	change_engine_speed_to(1.0, _pause_transition_time)
	

func set_player_node(player_to_set :PlayerGD) -> void:
	_player_node = player_to_set
	if game_status == Enums.GameStatus.NORMAL:
		shout_player_node()
	
func shout_player_node() -> void:
	if _player_node == null:
		EventBusGD.new_camera_focus.emit(Node3D.new())
	else:
		EventBusGD.new_camera_focus.emit(_player_node)
		

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta) -> void:
	_current_delta_t = delta
	var dt :String = String.num(delta,3)
	var fps :String = String.num(1.0/delta,3)
	EventBusGD.debug_message.emit("dt {dt} -> {fps} FPS".format({"dt": dt, "fps": fps}), "deltaT")
	
func change_engine_speed_to(speed :float, tween_time :float) -> void:
	var engine_tween: Tween = create_tween()
	engine_tween.tween_method(set_engine_speed, current_engine_speed, speed, tween_time).set_trans(Tween.TRANS_CUBIC).set_ease(Tween.EASE_OUT)

func set_engine_speed(time_scale :float) -> void:
	Engine.time_scale = time_scale
	EventBusGD.set_tween_speed_scale.emit(1.0 / time_scale)
	
func set_camera_rotation_matrix(angle :float) -> void:
	fixed_camera_rotation_matrix[0][0] = cos(angle)
	fixed_camera_rotation_matrix[0][1] = -(sin(angle))
	fixed_camera_rotation_matrix[1][0] = -fixed_camera_rotation_matrix[0][1]
	fixed_camera_rotation_matrix[1][1] =fixed_camera_rotation_matrix[0][0]
