extends ColorRect
class_name BlurredBackground

@export var _blur_time : float = 0.3
@export var blur_value : float = 0.8
@export var brightness_value : float = 0.2
@export var saturation_value : float = 0.5


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	EventBusMenu.open_main_menu.connect(blur_in)
	EventBusMenu.main_menu_got_closed.connect(blur_out)
	_blur_out(0.0)
	
func blur_in() -> void:
	_blur_in(_blur_time)
	pass
	
func blur_out() -> void:
	_blur_out(_blur_time)
	
	
func _blur_in(new_tween_time : float = 0.2) -> void:
	var tween : Tween = create_tween()
	tween.tween_method(_set_blur, 0.0, 1.0, new_tween_time).set_trans(Tween.TRANS_QUAD).set_ease(Tween.EASE_OUT)
	#GameSettings.set_tween_time_independend(tween)
	EventBus.set_tween_speed_scale.connect(tween.set_speed_scale)
	tween.set_speed_scale(1.0 / Engine.time_scale)

func _blur_out(new_tween_time : float = 0.2) -> void:
	var tween : Tween = create_tween()
	tween.tween_method(_set_blur, 1.0, 0.0, new_tween_time).set_trans(Tween.TRANS_QUAD).set_ease(Tween.EASE_OUT)
	#GameSettings.set_tween_time_independend(tween)
	EventBus.set_tween_speed_scale.connect(tween.set_speed_scale)
	tween.set_speed_scale(1.0 / Engine.time_scale)
	
	
func _set_blur(value) -> void:
	self.material.set_shader_parameter("blur", value * blur_value)
	self.material.set_shader_parameter("brightness", 1.0 - value * brightness_value)
	self.material.set_shader_parameter("saturation", 1.0 - value * saturation_value)
