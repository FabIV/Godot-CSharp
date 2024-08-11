extends Button
class_name CustomButtonGD

@export var _fade_in_direction :Enums.FadeInDirection = Enums.FadeInDirection.LEFT
@export var _fade_length_extention :float = 0.1
var _tween_in_position :Vector2
var _tween_out_position :Vector2

var _delay :float
var _duration :float

func _init() -> void:
	_delay = 0.0
	_duration = 1.0
	
func _ready() -> void:
	_tween_in_position = global_position
	determin_tween_direction()
	
func prepare_button(duration :float, delay :float) -> void:
	_tween_in_position = global_position
	determin_tween_direction()
	set_tween_data(duration, delay)

func correct_initial_data(x :float, y: float) -> void:
	_tween_in_position.x = x
	_tween_in_position.y = y
	determin_tween_direction()
	tween_out(0.0, 0.0)

func set_tween_data(duration :float, delay :float) -> void:
	_duration = duration
	_delay = delay
	
func tween_out_base() -> void:
	#tween_out(_duration, delay)
	do_position_tween(_duration, _delay, _tween_out_position)
	
func tween_out(duration :float, delay :float) -> void:
	do_position_tween(duration, delay, _tween_out_position)

func tween_in_base() -> void:
	do_position_tween(_duration, _delay, _tween_in_position)

func tween_in(duration :float, delay :float) -> void:
	do_position_tween(duration, delay, _tween_in_position)

func do_position_tween(duration :float, delay :float, target_position :Vector2) -> void:
	var pos_tween = create_tween()
	pos_tween.tween_interval(delay)
	pos_tween.tween_property(self, "position", target_position, duration).set_trans(Tween.TRANS_BACK).set_ease(Tween.EASE_OUT)
	EventBusGD.set_tween_speed_scale.connect(pos_tween.set_speed_scale)

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
		_tween_out_position.y = 1.0
	elif btm:
		_tween_out_position.y = -1.0
	_tween_out_position *= size 
	_tween_out_position *= (1.0 + _fade_length_extention) 
	_tween_out_position += _tween_in_position
	
