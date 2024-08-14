extends Control
class_name MainMenu

@export var _init_menu_path : NodePath

var _init_menu : SubMenuControl
var _call_menu_order : Array[SubMenuControl]

func _ready() -> void:
	_init_menu = get_node(_init_menu_path)
	_call_menu_order = []
	EventBusMenu.open_main_menu.connect(open_main_menu)
	EventBusMenu.ui_back.connect(go_back)

func open_main_menu() -> void:
	initiate_open_new_menu(_init_menu)

func close_main_menu() -> void:
	EventBusMenu.main_menu_got_closed.emit()
	
func go_back() -> void:
	var menu_to_close : SubMenuControl = _call_menu_order[_call_menu_order.size()-1]
	menu_to_close.deactivate()
	_call_menu_order.remove_at(_call_menu_order.size()-1)
	if _call_menu_order.size() > 0:
		change_sub_menu()
	else:
		close_main_menu()
		
func initiate_open_new_menu(new_menu : SubMenuControl) -> void:
	_call_menu_order.append(new_menu)
	change_sub_menu()

func change_sub_menu() -> void:
	var menu_to_open : SubMenuControl = _call_menu_order[_call_menu_order.size()-1]
	menu_to_open.activate()
