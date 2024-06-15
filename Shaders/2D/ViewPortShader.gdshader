shader_type canvas_item;

uniform bool border = true;

uniform float frame = 0.0169172932330827;
void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	
	float alpha = 1.0f;
	//alpha = step(0, UV.x - frame);
	//alpha = step(0, 1.0f - UV.x - frame);
	//alpha = step(0, UV.y - frame);
	//alpha = step(0, 1.0f - UV.y - frame);
	
	//float highlight = max(sin(TIME) * 2.0 -1.0, 0.0);
	//float highlight = max(sin(TIME) * 0.5 +0.5, 0.0);
	
	if (border)
	{
		alpha = min(min(min(step(0, UV.x - frame) ,
		step(0, 1.0f - UV.x - frame)), 
		step(0, UV.y - frame)),
		step(0, 1.0f - UV.y - frame)) ;
		
		//float addAlpha = (1.0 - alpha) * highlight;
		//COLOR.a = alpha + addAlpha;
		COLOR.a = alpha;
		//COLOR.r = step(highlight, )
	}


}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}
