extends BoxContainer
class_name TitelMessages

var _offset_x : float = 0.0
var _label : Label
var _currently_active : bool = false
#@export var print_name : String = "noName"
@export var _tween_time :float = 0.2
@export var rel_y_pos :float = 1.2
var y_pos_delta :float = 0.0
# Called when the node enters the scene tree for the first time.

var _init_label_pos : Vector2

@export var _fade_length_extention :float = 0.1

func _ready() -> void:
	EventBus.screen_scale_changed_to.connect(_set_new_scale)
	_label = get_child(0)
	var titel_manager : TitelManager = get_parent()
	titel_manager.add_titel_messages(self)
	EventBusMenu.close_current_titel.connect(_shut_me_down)
	_init_label_pos = Vector2(_label.position.x, _label.position.y)
	y_pos_delta = -self.size.y * rel_y_pos
	do_position_tween(0.0, 0.0, 1.0)

func activate_me(new_text : String) -> void:
	_label.text = new_text
	#print("{n} activated as {t}".format({"n":print_name, "t": new_text}))
	do_position_tween(_tween_time, 0.0, 0.0)
	_currently_active = true
	

func _shut_me_down() -> void:
	if _currently_active:
		#print("{n} deactivated".format({"n":print_name}))
		do_position_tween(_tween_time, 0.0, 1.0)
		_currently_active = false
		#print("TitelMessages/_shut_me_down has no functionality")

func do_position_tween(duration :float, delay :float, rel_target_position: float) -> void:
	var tween = create_tween()
	tween.tween_interval(delay)
	var start_pos_y :float = (_label.position.y - _init_label_pos.y) / y_pos_delta
	#print("{n}: target {a} from {b}".format({"n": print_name,"a": rel_target_position, "b": start_pos_y}))
	tween.tween_method(_set_positions, start_pos_y, rel_target_position, duration).set_trans(Tween.TRANS_QUAD).set_ease(Tween.EASE_OUT) 
	tween.set_speed_scale(1.0 / Engine.time_scale)
	EventBus.set_tween_speed_scale.connect(tween.set_speed_scale)
	
func _set_positions(rel_pos : float) -> void:
	_label.position.y = _init_label_pos.y + y_pos_delta * rel_pos

func _set_new_scale(new_scale :int) -> void:
	if scale.x == 1.0:
		_offset_x  = -(position.x + size.x / 2) / 1

	position.x = (position.x  - _offset_x * (scale.x - 1) ) * new_scale / scale.x + _offset_x * (new_scale - 1)
	
	scale.x = new_scale
	scale.y = new_scale	
