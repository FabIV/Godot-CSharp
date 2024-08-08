extends Node

class_name StateGD

#https://www.youtube.com/watch?v=dfp7FIO4GTA&

var has_been_initialized :bool = false
var on_update_fired : bool = false
var _state_machine : StateMachineGD

signal state_start()
signal state_updated()
signal state_exited()

func on_start(msg :Dictionary) -> void:
	state_start.emit()
	has_been_initialized = true

func on_update(msg :Dictionary) -> void:
	if not has_been_initialized:
		return
	state_updated.emit()
	on_update_fired = true

func update_state(dt :float) -> void:
	pass

func on_exit(next_state : String) -> void:
	if not has_been_initialized:
		return
	state_exited.emit()
	has_been_initialized = false
	on_update_fired = false

func set_state_machine(state_machine :StateMachineGD) -> void:
	_state_machine = state_machine
