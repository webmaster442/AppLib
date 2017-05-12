#define FuzzyAmount 0.245
#define CircleSize float2(0.7, 0.7)
#define CenterPoint float2(0.5, 0.5)


/// <description>A transition effect </description>
/// <summary>The amount(%) of the transition from first texture to the second texture. </summary>
/// <minValue>0</minValue>
/// <maxValue>1</maxValue>
/// <defaultValue>0.3</defaultValue>
float Progress : register(C0);

sampler2D Texture1 : register(s0);

struct VS_OUTPUT
{
    float4 Position  : POSITION;
    float4 Color     : COlOR;
    float2 UV        : TEXCOORD;
};

float4 Gray(float4 Color) 
{ 
	float grayscale = dot(Color.rgb, float3(0.3, 0.59, 0.11));
    Color.r = grayscale;
    Color.g = grayscale;
    Color.b = grayscale;
    Color.a = 1.0f;
	return Color; 
}

float4 Circle(float2 uv,float progress)
{
  float radius = -FuzzyAmount + progress * (CircleSize + 2.0 * FuzzyAmount);
  float fromCenter = length(uv - CenterPoint);
  float distFromCircle = fromCenter - radius;

  float4 c2 = tex2D(Texture1, uv);
  float4 c1 = Gray(c2);
  

  float p = saturate((distFromCircle + FuzzyAmount) / (2.0 * FuzzyAmount));
  return lerp(c2, c1, p);
}

//--------------------------------------------------------------------------------------
// Pixel Shader
//--------------------------------------------------------------------------------------
float4 main(VS_OUTPUT input) : COlOR
{
  return Circle(input.UV, Progress);
}
