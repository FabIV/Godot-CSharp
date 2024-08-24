extends BoxContainer
class_name ControlButtons

var _controler : SubMenuControl
var _buttons :Array[CustomButton]
@export var _duration : float = 0.3
@export var _delay :float = 0.1
@export var _additional_length :float = 0.1
@export var _transparency_fade : bool = false

@export var to_be_selected : int = 0

func _init() -> void:
	_buttons = []

func _ready() -> void:
	_controler = get_parent()
	_buttons = get_children_if_button()
	
	_controler.sub_menu_activated.connect(activate_menu)
	_controler.sub_menu_deactivated.connect(deactivate_menu)
	EventBus.screen_scale_changed_to.connect(_set_new_scale)
	

	var current_delay := 0.0
	var total_length := 0.0

	if vertical:
		for single_button in _buttons:
			single_button.prepare_button(_duration, current_delay, _additional_length, _transparency_fade, self)
			current_delay += _delay
			total_length += single_button.size.y
	
		var delta_length :float = size.y - total_length # zwischen den buttons
		delta_length /= _buttons.size() -1
	
		var current_y :float = 0.0
		for single_button in _buttons:
			single_button.correct_initial_data(single_button.position.x,current_y) #daten für tween 
			current_y += delta_length + single_button.size.y #um höhe und spalt vergrößern	

	else:
		for single_button in _buttons:
			single_button.prepare_button(_duration, current_delay, _additional_length, _transparency_fade, self)
			current_delay += _delay
			total_length += single_button.size.x
		
		var delta_length :float = size.x - total_length # zwischen den buttons
		delta_length /= _buttons.size() -1
		
		var current_x :float = 0.0
		for single_button in _buttons:
			single_button.correct_initial_data(current_x, single_button.position.y) #daten für tween
			single_button.focus_mode = 0 # kann den fokus nicht bekommen
			current_x += delta_length + single_button.size.x #um höhe und spalt vergrößern
		
	#visible = false	
	
func activate_menu() -> void:
	#visible = true
	for single_button in _buttons:
		single_button.tween_in_base()
		single_button.focus_mode = 2
	_buttons[min(to_be_selected,len(_buttons)-1)].grab_focus()

func deactivate_menu() -> void:
	var time_of_last_tween : float = 0.0
	for single_button in _buttons:
		time_of_last_tween = max(time_of_last_tween, single_button.tween_out_base())
		single_button.focus_mode = 0

func set_custom_as_selected(cb :CustomButton) -> void:
	for i in range(_buttons.size()):
		if _buttons[i] == cb:
			to_be_selected = i
			break
		
func get_children_if_button() -> Array[CustomButton]:
	var all_children :Array = get_children().filter(func(v): return v is CustomButton)
	var res :Array[CustomButton] = []
	for single_button in all_children:
		res.append(single_button)
	return res

func _set_new_scale(_new_scale :int) -> void:
	var left = anchor_left
	var top = anchor_top
	
	position.x = -size.x * left * _new_scale
	position.y = -size.y * top * _new_scale

	scale.x = _new_scale
	scale.y = _new_scale	
	
	
	
