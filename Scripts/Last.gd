extends Node
class_name LastNode

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	EventBus.last_loaded.emit()
	self.queue_free()
