sampler2D input : register(s0);

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 Color = tex2D(input , uv.xy);
	float grayscale = dot(Color.rgb, float3(0.3, 0.59, 0.11));
	Color.r = grayscale;
	Color.g = grayscale;
	Color.b = grayscale;
	Color.a = 1.0f;

	return Color;
}