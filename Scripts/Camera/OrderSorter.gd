extends Object
class_name OrderSorter

var n1 :int
var n2 :int

func _init(a1 :int, a2:int) -> void:
	if a1 % 2 != 0:
		n2 = a1
		n1 = a2
	else:
		n1 = a1
		n2 = a2
