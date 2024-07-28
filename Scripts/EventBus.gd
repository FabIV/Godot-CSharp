extends Node

signal player_motion_data(motion_x :float, motion_y :float)

signal debug_message(msg :String, node_id :String)

signal player_is_set(player :PlayerGD)

signal new_camera_focus(position :Node3D)

signal need_player_node()

signal set_tween_speed_scale(speed_scale :float)
