shader_type canvas_item;

uniform float blur: hint_range(0.0, 2.5);
uniform float brightness: hint_range(0.0, 1.0);
uniform float saturation: hint_range(0.0, 1.0);
//#unifrom float brightness: hint_range(0.0, 1.0);
uniform sampler2D SCREEN_TEXTURE : hint_screen_texture, filter_linear_mipmap;
void fragment() {
	vec3 grayScale = texture(SCREEN_TEXTURE, SCREEN_UV).rgb;
	COLOR = textureLod(SCREEN_TEXTURE, SCREEN_UV, blur);
	COLOR.rgb = vec3(dot(COLOR.rgb, COLOR.rgb)) * (1.0 - saturation) + COLOR.rgb * saturation;
	COLOR.rgb *= brightness;
}
