shader_type spatial;

uniform sampler2D SCREEN_TEXTURE : hint_screen_texture, filter_nearest;
uniform sampler2D DEPTH_TEXTURE : hint_depth_texture, filter_nearest;

uniform vec3 albedo : source_color = vec3(0.02, 0.45, 0.8);
uniform float transparency : hint_range(0.0, 1.0, 0.01) = 0.1;
uniform float refraction_intensity : hint_range(0.0, 0.10, 0.001) = 0.02; 

uniform float ssr_resolution   : hint_range(0.01, 1.0, 0.001)	= 0.25;
uniform float ssr_max_travel   : hint_range(0.0, 10.0, 0.1) 	= 3.8;
uniform float ssr_max_diff     : hint_range(0.1, 5.0, 0.1) 	= 0.3; //0.6
uniform float ssr_mix_strength : hint_range(0.0, 1.0, 0.01) 	= 0.13;
uniform float ssr_refaction_intensity : hint_range(0.0, 1.0, 0.01) = 0.07;

varying vec3 color;

uniform sampler2D border_noise;
uniform sampler2D highlight_noise;

uniform vec3  border_color : source_color = vec3(1.0);
uniform float border_thickness : hint_range(0.0, 5.0, 0.01) = 1.8;
uniform float border_near = 4.0;
uniform float border_far = 300.0;

uniform float frames_per_second : hint_range(1.0, 100.0, 1.0) = 8.0;

vec2 get_uv_from_view_position(vec3 position_view_space, mat4 proj_m)
{
	vec4 position_clip_space = proj_m * vec4(position_view_space.xyz, 1.0);
	vec2 position_ndc = position_clip_space.xy / position_clip_space.w;
	return position_ndc.xy * 0.5 + 0.5;
}

vec3 get_view_position_from_uv(vec2 uv, float depth, mat4 inv_proj_m)
{
	vec4 position_ndc = vec4((uv * 2.0) - 1.0, depth, 1.0);
	vec4 view_position = inv_proj_m * position_ndc;
	return view_position.xyz /= view_position.w;
}

bool is_within_screen_boundaries(vec2 position) {
	return position.x > 0.0 && position.x < 1.0 && position.y > 0.0 && position.y < 1.0;
}

vec3 get_ssr_color(vec3 surface_view_position, vec3 normal_view_space, vec3 view_view_space, mat4 proj_m, mat4 inv_proj_m)
{
	
	vec3 current_position_view_space = surface_view_position;
	vec3 view_direction_view_space = view_view_space * -1.0;
	vec3 reflect_vector_view_space = normalize(reflect(view_direction_view_space.xyz, normal_view_space.xyz));
	
	vec2 current_screen_position = vec2(0.0);
	
	vec3 resulting_color = vec3(-1.0);
	for(float travel=0.0; resulting_color.x < 0.0 && travel < ssr_max_travel; travel = travel + ssr_resolution)
	{
		current_position_view_space += reflect_vector_view_space * ssr_resolution;
		current_screen_position = get_uv_from_view_position(current_position_view_space, proj_m);

		float depth_texture_probe_raw = texture(DEPTH_TEXTURE, current_screen_position).x;
		vec3 depth_texture_probe_view_position = get_view_position_from_uv(current_screen_position, depth_texture_probe_raw, inv_proj_m);
		
		float depth_diff = depth_texture_probe_view_position.z - current_position_view_space.z;
		
		resulting_color = (is_within_screen_boundaries(current_screen_position) && depth_diff >= 0.0 && depth_diff < ssr_max_diff) ? texture(SCREEN_TEXTURE, current_screen_position.xy).rgb : vec3(-1.0);
	}
	return resulting_color;
}

float border(float cur_depth)
{
	return border_near * border_far / (border_far + (2.0 * cur_depth - 1.0) * (border_near - border_far));
}

void fragment() {
	vec3 iNV = texture(DEPTH_TEXTURE, SCREEN_UV).bgr* 1.0f;
	
	//float angle = 30.0f;
	//float cp = cos(angle/180.0f * PI);
	//float sp = sin(angle/180.0f * PI);
	//vec3 normal_view = vec3(iNV.r, iNV.g * cp + iNV.b * sp, iNV.b * cp + iNV.b * sp);
	//normal_view = iNV;
	vec3 normal_view = iNV;
	
	float screen_depth_raw = texture(DEPTH_TEXTURE, SCREEN_UV).x;
	float surface_depth = FRAGCOORD.z;
	
	//float time = TIME - mod(TIME, 0.16666);
	//float time = TIME - mod(TIME, 0.08333);
	float time = TIME - mod(TIME, 1.0 / frames_per_second);
	
	float border_noise_factor  = clamp(texture(border_noise, UV*4.0 + (time* 0.05)).x * texture(border_noise, -UV * -2.0 + (-time* 0.05)).x, 0.0, 0.50);
	float highlights = clamp(texture(highlight_noise, UV*2.0 + (time* 0.01)).x * texture(highlight_noise, -UV * -1.0 + (-time* 0.01)).x, 0.0, 0.50);
	
		
	vec2 refraction_uv = SCREEN_UV + border_noise_factor * refraction_intensity;
	vec2 ssr_uv = SCREEN_UV + vec2(0.0,  -highlights) * ssr_refaction_intensity;
	
	vec3 surface_view_position 	= get_view_position_from_uv(ssr_uv, surface_depth, INV_PROJECTION_MATRIX);
	
	vec3 ssr_color = get_ssr_color(surface_view_position, NORMAL, normal_view, PROJECTION_MATRIX, INV_PROJECTION_MATRIX);
	
	vec3 screen_color 	= texture(SCREEN_TEXTURE, refraction_uv).rgb;
	vec3 water_color = mix(albedo.rgb, screen_color, transparency);
	
	vec3 color_no_border = (ssr_color.x >= 0.0) ? mix(water_color, ssr_color, ssr_mix_strength) : water_color;
	
	float border_diff = border(screen_depth_raw) - border(surface_depth);	


	//float final_border_diff = clamp(border_diff * 10000.0 - border_noise_factor * border_thickness, 0.0, 1.0) > 0.01 ? 1.0: 0.0;
	
		
	//float foam = highlights > 0.03 ? final_border_diff : 0.0;
	
	//color = mix(border_color, color_no_border,  foam);
	
	float final_border_diff = step(border_diff * 10000.0 - border_noise_factor * border_thickness, 0.1);

	float foam = step(highlights, 0.03) + final_border_diff;
	
	color = mix(color_no_border, border_color,   foam);
	
	//color = vec3(foam);
}

void light() {
	DIFFUSE_LIGHT = color;
	
}