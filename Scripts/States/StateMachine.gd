extends Node
class_name StateMachineGD

#https://www.youtube.com/watch?v=dfp7FIO4GTA&t=407s

signal pre_start()
signal post_start()
signal pre_exit()
signal post_exit()

var states :Array#[StateGD]
#var states :Dictionary
var current_state :String
var last_state :String

var _state :StateGD = null


func _ready() -> void:
	states = get_node("States").get_children()
	for state in states:
		state.set_state_machine(self)

func set_state(state :StateGD, msg :Dictionary) -> void:
	if state == null:
		return
	else:
		pre_exit.emit()
		_state.on_exit(str(state))
		post_exit.emit()
		last_state = current_state
		current_state =  str(state)
		_state = state
		pre_start.emit()
		_state.on_start(msg)
		post_start.emit()
		_state.on_update({})
		

func change_state(state_name :String, msg :Dictionary) -> void:
	for state in states:
		if str(state) == str(state_name):
			set_state(state, msg)
			break

func _physics_process(delta :float) -> void:
	if _state == null:
		return
	_state.update_state(delta)
	
