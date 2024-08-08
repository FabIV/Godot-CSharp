extends Node
class_name CharDataCollectorGD

@export var path :String = "res://Data/Chars/"
var _data_management :DataManagementGD

func _ready() -> void:
	_data_management = get_parent()
	var files = DirAccess.get_files_at(path)
	for file in files:
		var scene_to_load := load(path+file)
		var loaded_scene = scene_to_load.instantiate()
		var real_name :String = file.left(file.length() -5)
		loaded_scene.name = real_name
		add_child(loaded_scene)
		_data_management.add_char_data(loaded_scene)
		#CharDataDefinition converted = (CharDataDefinition)loadedScene;
		#_dataManagment.AddCharData(converted);
		loaded_scene.queue_free()
		
