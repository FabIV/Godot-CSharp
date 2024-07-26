shader_type spatial;

render_mode ambient_light_disabled, depth_prepass_alpha, depth_draw_opaque;

uniform float WindScale : hint_range(1.0, 20.0) = 1.0;
uniform float WindSpeed : hint_range(0.0, 20.0) = 4.0;
uniform float WindStrength : hint_range(1.0, 20.0) = 5.0;
uniform float WindDensity : hint_range(1.0, 20.0) = 5.0;
uniform float ClampTop : hint_range(0.0, 1.0) = 1.0;
uniform float ClampBtm : hint_range(-1.0, 0.0) = 0.0;

uniform vec4 albedo : source_color = vec4(1.0f);
uniform sampler2D albedo_texture : source_color;

uniform float uv_shift : hint_range(0.0f, 1.0f) = 0.1f;

uniform int cuts : hint_range(1, 8) = 3;
uniform float wrap : hint_range(-2.0f, 2.0f) = 0.0f;

uniform bool use_attenuation = true;

uniform float specular_strength : hint_range(0.0f, 1.0f) = 1.0f;
uniform float specular_shininess : hint_range(0.0f, 32.0f) = 16.0f;
uniform sampler2D specular_map : source_color;

uniform bool use_rim = false;
uniform float rim_width : hint_range(0.0f, 16.0f) = 8.0f;
uniform float rim_factor : hint_range(0.0f, 10.0f) = 5.0f;

//uniform bool use_ramp = false;
uniform bool use_ramp = true;
uniform bool use_partial_uv = true;
uniform bool position_optimized = false;
uniform sampler2D ramp : source_color;
uniform sampler2D alpha : source_color;
varying vec3 vertex_pos;
varying vec3 normal;

float split_specular(float specular) {
	return step(0.5f, specular);
}

void vertex() {
	vertex_pos = VERTEX;
	//normal = NORMAL;
	MODELVIEW_MATRIX = VIEW_MATRIX * mat4(INV_VIEW_MATRIX[0],INV_VIEW_MATRIX[1],INV_VIEW_MATRIX[2],MODEL_MATRIX[3]);
	//Wind-Effect
	//adapted from: https://github.com/ruffiely/windshader_godot
	float contribution = 1.0;// * (1.0 - float(DeactivateGlobalVariation));
	vec3 world_pos = (MODEL_MATRIX * vec4(VERTEX, 1.0)).xyz;
	vec3 world_pos_eff = world_pos * contribution;	//Generates world coordinates for vertecies
	// Removed using world_position due to dragging bug
	float positional_influence = -VERTEX.x + VERTEX.z -world_pos_eff.x + world_pos_eff.z;
	float offset = fract(positional_influence * (1.0 / WindScale) + (TIME * WindScale/1000.0));	//Generates linear curve that slides along vertecies in world space
	offset = min(1.0 - offset, offset);														//Makes generated curve a smooth gradient
	offset = (1.0 - offset) * offset * 2.0;													//Smoothes gradient further
	
	float time = TIME - mod(TIME, 0.16666);
	
	
	float t = time + sin(time + offset + cos(time + offset * WindStrength * 2.0) * WindStrength); //Generates noise in world space value
	
	//float mask = fract(v.y * wind_density) * v.y; //Generates vertical mask, so leaves on top move further than leaves on bottom
	//mask = clamp(mask, 0.0, 1.0);                 //Clamps mask
	
	float mask = clamp(VERTEX.y* WindDensity, 0.0, 1.0) * (ClampTop - ClampBtm) + ClampBtm;
	
	
	float si = sin(t) / 20.0 * WindStrength * offset;	//Generates clamped noise, adds strength, applies gradient mask
	float csi = cos(t)/ 20.0 * WindStrength * offset;	//Generates clamped noise with offset, adds strength, applies gradient mask
		
	vec3 wind_offset = vec3(VERTEX.x * si * mask, VERTEX.y * si * mask, VERTEX.z * csi * mask);
	

	VERTEX += wind_offset;

}

void fragment() {
	ALBEDO = albedo.rgb * texture(albedo_texture, UV).rgb;
	float alpha_text = texture(alpha, UV.xy).r;
	ALPHA = alpha_text;
}

void light() {
	// Attenuation.
	float attenuation = 1.0f;
	if (use_attenuation) {
		attenuation = ATTENUATION;
	}
	
	// Diffuse lighting.
	float NdotL = dot(NORMAL, LIGHT);
	float diffuse_amount = NdotL + (attenuation - 1.0) + wrap;
	
	vec3 diffuse = ALBEDO.rgb * LIGHT_COLOR / PI;
	
	if (use_rim){
		float NdotV = dot(NORMAL, VIEW);
		diffuse_amount += pow(1.0 - NdotV, rim_width) *1.0f * rim_factor;
	}
	
	if (use_ramp) {
		float y_position = uv_shift;
		
		if (use_partial_uv){
			y_position = UV.y;
		}
		
		if (position_optimized){
			float cuts_inv = 1.0f / float(cuts);
			float x_position = clamp(diffuse_amount - mod(diffuse_amount, cuts_inv) + cuts_inv / 2.0f, 0.03f, 0.97f);
			diffuse = texture(ramp, vec2(x_position, y_position)).rgb;
		}
		else{
			diffuse = texture(ramp, vec2(clamp(diffuse_amount, 0.03f, 0.97f), y_position)).rgb;
		}
	} else {

		float cuts_inv = 1.0f / float(cuts);
		float diffuse_stepped = clamp(diffuse_amount + mod(1.0f - diffuse_amount, cuts_inv), 0.03f, 0.97f);
		
		diffuse = vec3(diffuse_stepped, diffuse_stepped, diffuse_stepped);
	}
	//diffuse = LIGHT_COLOR;
	DIFFUSE_LIGHT = diffuse;// * clamp(LIGHT_COLOR,0.1,1.0);
	//DIFFUSE_LIGHT = LIGHT_COLOR;
	
	
	
}