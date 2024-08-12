extends Control
class_name MainMenu

@export var _init_menu_path : NodePath

var _init_menu :SubMenuControlGD
var _call_menu_order : Array[SubMenuControlGD]

func _ready() -> void:
	_init_menu = get_node(_init_menu_path)
	_call_menu_order = []
	EventBusMenuGD.open_main_menu.connect(open_main_menu)
	EventBusMenuGD.ui_back.connect(go_back)

func open_main_menu() -> void:
	initiate_open_new_menu(_init_menu)

func close_main_menu() -> void:
	EventBusMenuGD.main_menu_got_closed.emit()
	
func go_back() -> void:
	var menu_to_close : SubMenuControlGD = _call_menu_order[_call_menu_order.size()-1]
	menu_to_close.deactivate()
	_call_menu_order.remove_at(_call_menu_order.size()-1)
	if _call_menu_order.size() > 0:
		change_sub_menu()
	else:
		close_main_menu()
		
func initiate_open_new_menu(new_menu :SubMenuControlGD) -> void:
	_call_menu_order.append(new_menu)
	change_sub_menu()

func change_sub_menu() -> void:
	var menu_to_open : SubMenuControlGD = _call_menu_order[_call_menu_order.size()-1]
	menu_to_open.activate()
