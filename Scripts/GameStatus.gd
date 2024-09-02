extends Node

const PAUSE_OUT_TRANSITION_TIMER :float = 0.5
const PAUSE_IN_TRANSITION_TIMER :float  = 0.5


var fixed_camera_rotation_matrix :Array[Array]
var _player_node : Player
var game_status :Enums.GameStatus = Enums.GameStatus.NORMAL
var pre_menu_status :Enums.GameStatus = Enums.GameStatus.NORMAL
#var _ui_timer : UITimer
var _current_delta_t :float
var _timer_pause : UITimer
var input_motion :Enums.InputMotion
var prev_input_motion :Enums.InputMotion

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
	EventBus.player_is_set.connect(self.set_player_node)
	EventBus.need_player_node.connect(self.shout_player_node)
	connect_signals()
	
func connect_signals() -> void:
	EventBusMenu.open_main_menu.connect(self.set_status_to_main_menu)
	EventBusMenu.main_menu_got_closed.connect(self.main_menu_got_closed)

func set_status_to_main_menu() -> void:
	pre_menu_status = game_status
	game_status = Enums.GameStatus.MENU
	pause_game_in(1.0)

func pause_game_in(transition_time :float) -> void:
	prev_input_motion = input_motion
	input_motion = Enums.InputMotion.INPUT_FREEZED
	clear_pause_timer_if_necessary()
	_timer_pause = UITimer.new()
	add_child(_timer_pause)
	_timer_pause.wait_time = transition_time
	_timer_pause.timeout.connect(pause_game_now_and_kill_timer)
	_timer_pause.start()
	change_engine_speed_to(0.0001, PAUSE_IN_TRANSITION_TIMER)

func clear_pause_timer_if_necessary() -> void:
	if _timer_pause != null:
		_timer_pause.queue_free()
		_timer_pause = null
		
func unpause_game() -> void:
	unpause_game_in(PAUSE_OUT_TRANSITION_TIMER)
	
func unpause_game_in(transition_time :float) -> void:
	clear_pause_timer_if_necessary()
	_timer_pause = UITimer.new()
	add_child(_timer_pause)
	_timer_pause.timeout.connect(unpause_game_now_and_kill_timer)
	_timer_pause.process_mode = Node.PROCESS_MODE_ALWAYS
	_timer_pause.start()
	change_engine_speed_to(1.0, transition_time)
		
func pause_game_now_and_kill_timer() -> void:
	set_engine_speed(0.001)
	get_tree().paused = true
	clear_pause_timer_if_necessary()
	EventBus.debug_message.emit("Game paused",  str(get_class()) + "001")

func unpause_game_now_and_kill_timer() -> void:
	unpause_game_now()
	clear_pause_timer_if_necessary()

func unpause_game_now() -> void:
	get_tree().paused = false;
	EventBus.debug_message.emit("Game unpaused",  str(get_class()) + "001")
	input_motion = prev_input_motion
	

func main_menu_got_closed() -> void:
	input_motion = Enums.InputMotion.INPUT_ALLOWED
	game_status = pre_menu_status
	unpause_game_now_and_kill_timer()
	change_engine_speed_to(1.0, PAUSE_OUT_TRANSITION_TIMER)
	

func set_player_node(player_to_set : Player) -> void:
	_player_node = player_to_set
	if game_status == Enums.GameStatus.NORMAL:
		shout_player_node()
	
func shout_player_node() -> void:
	if _player_node == null:
		EventBus.new_camera_focus.emit(Node3D.new())
	else:
		EventBus.new_camera_focus.emit(_player_node)

func _process(delta) -> void:
	_current_delta_t = delta
	var dt :String = String.num(delta,3)
	var fps :String = String.num(1.0/delta,3)
	EventBus.debug_message.emit("Engine speed of {es}".format({"es": Engine.time_scale}), "engine_speed")
	EventBus.debug_message.emit("dt {dt} -> {fps} FPS".format({"dt": dt, "fps": fps}), "deltaT")
	
func change_engine_speed_to(speed :float, tween_time :float) -> void:
	var engine_tween: Tween = create_tween()
	engine_tween.tween_method(set_engine_speed, max(current_engine_speed,0.08), speed, tween_time).set_trans(Tween.TRANS_CUBIC).set_ease(Tween.EASE_OUT)

func set_engine_speed(time_scale :float) -> void:
	Engine.time_scale = time_scale
	current_engine_speed = time_scale
	EventBus.set_tween_speed_scale.emit(1.0 / time_scale)
	
func set_camera_rotation_matrix(angle :float) -> void:
	fixed_camera_rotation_matrix[0][0] = cos(angle)
	fixed_camera_rotation_matrix[0][1] = -(sin(angle))
	fixed_camera_rotation_matrix[1][0] = -fixed_camera_rotation_matrix[0][1]
	fixed_camera_rotation_matrix[1][1] =fixed_camera_rotation_matrix[0][0]
