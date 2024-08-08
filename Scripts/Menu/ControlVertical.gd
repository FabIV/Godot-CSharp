extends VBoxContainer
class_name ControlVerticalGD

var _controler :SubMenuControlGD
var _buttons :Array #CustomButtonGD
@export var _duration : float = 0.3
@export var _delay :float = 0.1

func _init() -> void:
	_buttons = []

func _ready() -> void:
	_controler = get_parent()
	_buttons = get_children()
	
	_controler.sub_menu_activated.connect(activate_menu)
	_controler.sub_menu_deactivated.connect(deactivate_menu)
		
	var current_delay := 0.0
	var total_length := 0.0
	
	for single_button in _buttons:
		single_button.prepare_button(_duration, current_delay)
		current_delay += _delay
		total_length += single_button.size.y
		
	var delta_length :float = size.y - total_length
	delta_length /= _buttons.size() -1
	var current_x := 0.0
	var current_y := 0.0
	
	_buttons[0].correct_initial_data(current_x, current_y)

	for i in range(_buttons.size()):
		current_y += delta_length + _buttons[i -1].size.y
		_buttons[i].correct_initial_data(_buttons[i].global_position.x, current_y)
	
func activate_menu() -> void:
	for single_button in _buttons:
		single_button.tween_in_base()

func deactivate_menu() -> void:
	for single_button in _buttons:
		single_button.tween_out_base()

