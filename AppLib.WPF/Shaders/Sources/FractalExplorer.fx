sampler2D input : register(s0);

#define Iterations 128
#define Aspect 1

/// <summary>Shader mode</summary>
/// <minValue>0</minValue>
/// <maxValue>2</maxValue>
/// <defaultValue>0</defaultValue>
float Mode: register(c0);

/// <summary>PAN</summary>
/// <minValue>0,0</minValue>
/// <maxValue>1,1</maxValue>
/// <defaultValue>0.5,0</defaultValue>
float2 Pan: register(c1);

/// <summary>Zoom</summary>
/// <minValue>0</minValue>
/// <maxValue>5</maxValue>
/// <defaultValue>3</defaultValue>
float Zoom: register(c2);

/// <summary>Color Scale</summary>
/// <minValue>0,0,0</minValue>
/// <maxValue>9,9,9</maxValue>
/// <defaultValue>4,5,6</defaultValue>
float3 ColorScale: register(c3);

float2 squarec(float2 input)
{
	return float2((input.x * input.x) - (input.y * input.y), input.x * input.y * 2);
}

float ComputeValue(float2 v, float2 offset, int mode)
{
	float vxsquare = 0;
	float vysquare = 0;
	int iteration = 0;
	int lastIteration = Iterations;

	do
	{
		vxsquare = v.x * v.x;
		vysquare = v.y * v.y;
		
		if (mode == 0)
		{
			v = float2(vxsquare - vysquare, v.x * v.y * 2) + offset;
		}
		else
		{
			v = float2(vxsquare-vysquare, abs(v.x*v.y) * 2) + offset;
	    }
		iteration++;
		if ((lastIteration == Iterations) && (vxsquare + vysquare) > 4.0)
		{
			lastIteration = iteration + 1;
		}
	}
	while (iteration < lastIteration);
	
	return (float(iteration) - (log(log(sqrt(vxsquare + vysquare))) / log(2.0))) / float(Iterations);
}

float4 Mandelbrot_PixelShader(float2 texCoord : TEXCOORD0) : COLOR0
{
	float2 v = (texCoord - 0.5) * Zoom * float2(1, Aspect) - Pan;

	float val = ComputeValue(v, v, 0);

	return float4(sin(val * ColorScale.x), sin(val * ColorScale.y), sin(val * ColorScale.z), 1);
}

float4 Burningship_PixelShader(float2 texCoord : TEXCOORD0) : COLOR0
{
	float2 v = (texCoord - 0.5) * Zoom * float2(1, Aspect) - Pan;

	float val = ComputeValue(v, v, 1);

	return float4(sin(val * ColorScale.x), sin(val * ColorScale.y), sin(val * ColorScale.z), 1);
}


float4 Julia_PixelShader(float2 texCoord : TEXCOORD0) : COLOR0
{
	float2 v = (texCoord - 0.5) * Zoom * float2(1, Aspect) - Pan;

	float2 JuliaSeed = float2(0.39, -0.2);
	float val = ComputeValue(v, JuliaSeed, 0);

	return float4(sin(val * ColorScale.x), sin(val * ColorScale.y), sin(val * ColorScale.z), 1);
}


float4 main(float2 uv : TEXCOORD) : COLOR 
{ 
	if (Mode == 0) return Mandelbrot_PixelShader(uv.xy);
	else if (Mode == 1) return Julia_PixelShader(uv.xy);
	else return Burningship_PixelShader(uv.xy);
}