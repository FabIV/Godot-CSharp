extends Node
class_name GamePlayControl


@export var Language :Enums.Language = Enums.Language.DE


func _ready():
	TranslationServer.set_locale(Enums.Language.keys()[Language].to_lower())


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
