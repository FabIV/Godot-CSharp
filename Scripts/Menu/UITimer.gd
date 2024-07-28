extends Timer

class_name UITimerGD

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	var actual_delta :float = delta / Engine.time_scale
	var time_delta  = actual_delta - delta
	
	if time_delta < time_left:
		wait_time = time_left - time_delta
		start()
	else:
		wait_time = 0.5 * delta
		start()
