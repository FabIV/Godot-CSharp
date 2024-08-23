extends Button
class_name CustomButton

@export var _fade_in_direction :Enums.FadeInDirection = Enums.FadeInDirection.LEFT
@export var _fade_length_extention :float = 0.1
var _tween_in_position :Vector2
var _tween_out_position :Vector2

var _delay :float
var _duration :float

@export var _node_path_follow_up_menu : NodePath

var _transparency_fade : bool = false
var _is_init_menu : bool      = false

var _follow_up_menu : SubMenuControl = null

var _trancperency_target :float = 1.0

func _init() -> void:
	_delay = 0.0
	_duration = 1.0
	
func _ready() -> void:
	_tween_in_position = position
	determin_tween_direction()
	get_follow_up_menu()
	connect_button_function()
	if not _node_path_follow_up_menu.is_empty():
		self.pressed.connect(open_next_menu)

func open_next_menu() -> void:
	EventBusMenu.open_next_sub_menu.emit(_follow_up_menu)

func get_follow_up_menu() -> void:
	if not _node_path_follow_up_menu.is_empty():
		var n = get_node(_node_path_follow_up_menu)
		if n is SubMenuControl:
			_follow_up_menu = n

func connect_button_function() -> void:
	var all_functions = get_children()
	for sub_child in all_functions:
		sub_child.connect_function_to_button(self)
	

func prepare_button(duration :float, delay :float, length_extention :float, trans_fade :bool) -> void:
#	_tween_in_position = global_position
	_tween_in_position = position
	determin_tween_direction()
	set_tween_data(duration, delay)
	_fade_length_extention = length_extention
	_transparency_fade = trans_fade

func correct_initial_data(x :float, y: float) -> void:
	_tween_in_position.x = x
	_tween_in_position.y = y
	determin_tween_direction()
	tween_out(0.0, 0.0)

func set_tween_data(duration :float, delay :float) -> void:
	_duration = duration
	_delay = delay
	
func tween_out_base() -> float:
	_trancperency_target = 0.0
	do_position_tween(_duration, _delay, _tween_out_position)
	return _duration + _delay
	
func tween_out(duration :float, delay :float) -> void:
	_trancperency_target = 0.0
	do_position_tween(duration, delay, _tween_out_position)

func tween_in_base() -> void:
	_trancperency_target = 1.0
	do_position_tween(_duration, _delay, _tween_in_position)

func tween_in(duration :float, delay :float) -> void:
	_trancperency_target = 1.0
	do_position_tween(duration, delay, _tween_in_position)

func do_position_tween(duration :float, delay :float, target_position :Vector2) -> void:
	var pos_tween = create_tween()
	pos_tween.tween_interval(delay)
	var current_position : Vector2 = Vector2(position.x, position.y)
	pos_tween.tween_property(self, "position", target_position, duration).from(current_position).set_trans(Tween.TRANS_BACK).set_ease(Tween.EASE_OUT)
	EventBus.set_tween_speed_scale.connect(pos_tween.set_speed_scale)
	if not _is_init_menu:
		pos_tween.set_speed_scale(1.0 / Engine.time_scale)
		
	if _transparency_fade:
		do_transparency_tween(duration, delay, _trancperency_target)
		

func do_transparency_tween(duration:float, delay: float, target_trans :float) -> void:
	var trans_tween = create_tween()
	trans_tween.tween_interval(delay)
	trans_tween.tween_method(set_new_alpha, modulate.a, target_trans, duration * 0.95).set_trans(Tween.TRANS_BACK).set_ease(Tween.EASE_OUT)
	EventBus.set_tween_speed_scale.connect(trans_tween.set_speed_scale)
	if not _is_init_menu:
		trans_tween.set_speed_scale(1.0 / Engine.time_scale)
		
func set_new_alpha(new_alpha : float) -> void:
	self.modulate.a = new_alpha

func determin_tween_direction() -> void:
	_tween_out_position.x = 0.0
	_tween_out_position.y = 0.0
	var left := _fade_in_direction == Enums.FadeInDirection.LEFT
	left = left or _fade_in_direction == Enums.FadeInDirection.TOP_LEFT
	left = left or _fade_in_direction == Enums.FadeInDirection.BOTTOM_LEFT
	var right := _fade_in_direction == Enums.FadeInDirection.RIGHT
	right = right or _fade_in_direction == Enums.FadeInDirection.TOP_RIGHT
	right = right or _fade_in_direction == Enums.FadeInDirection.BOTTOM_RIGHT
	var top := _fade_in_direction == Enums.FadeInDirection.TOP
	top = top or _fade_in_direction == Enums.FadeInDirection.TOP_LEFT
	top = top or _fade_in_direction == Enums.FadeInDirection.TOP_RIGHT
	var btm := _fade_in_direction == Enums.FadeInDirection.BOTTOM
	btm = btm or _fade_in_direction == Enums.FadeInDirection.BOTTOM_LEFT
	btm = btm or _fade_in_direction == Enums.FadeInDirection.BOTTOM_RIGHT

	if left:
		_tween_out_position.x = -1.0
	elif right:
		_tween_out_position.x = 1.0
	if top:
		_tween_out_position.y = -1.0
	elif btm:
		_tween_out_position.y = 1.0
	_tween_out_position *= size 
	_tween_out_position *= (1.0 + _fade_length_extention) 
	_tween_out_position += _tween_in_position
	
