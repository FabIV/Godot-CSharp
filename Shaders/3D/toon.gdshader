shader_type spatial;

render_mode ambient_light_disabled;


uniform vec4 albedo : source_color = vec4(1.0f);

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

varying vec3 vertex_pos;
varying vec3 normal;


float split_specular(float specular) {
	return step(0.5f, specular);
}

void vertex() {
	vertex_pos = VERTEX;
	normal = NORMAL;
}

void fragment() {
	//ALBEDO = vec3(1.0f);
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
	
	//vec3 diffuse = ALBEDO.rgb * LIGHT_COLOR / PI;
	vec3 diffuse = LIGHT_COLOR / PI;
	
	//vec3 view = vec3(0.0f);
	if (use_rim){
		
		//newnew 
		//rim = VIEW;
		vec3 view = (vertex_pos) * vec3(-0.02f, -0.20f, 1.0f) * 0.5f + vec3(0.0f, 0.0f, 1.0f);
		float NdotV = dot(NORMAL, view);
		//rim = vec3(pow(1.0f - NdotV, rim_width));
		diffuse_amount += clamp(pow(1.0f - NdotV, rim_width),0.0f, 1.0f) * rim_factor;
	}
	
	if (use_ramp) {
		float y_position = uv_shift;
		
		if (use_partial_uv) {
			y_position = UV.y;
		}
		
		if (position_optimized){
			float cuts_inv = 1.0f / float(cuts);
			float x_position = clamp(diffuse_amount - mod(diffuse_amount, cuts_inv) + cuts_inv / 2.0f, 0.03f, 0.97f);
			diffuse = texture(ramp, vec2(x_position, y_position)).rgb;
		}
		else {
			diffuse = texture(ramp, vec2(clamp(diffuse_amount, 0.03f, 0.97f), y_position)).rgb;
		}
	} 
	else	{
		float cuts_inv = 1.0f / float(cuts);
		float diffuse_stepped = clamp(diffuse_amount + mod(1.0f - diffuse_amount, cuts_inv), 0.03f, 0.97f) ;
		
		diffuse = albedo. rgb * diffuse_stepped;
	}
	DIFFUSE_LIGHT = diffuse;// * clamp(LIGHT_COLOR,0.1,1.0);

	
}