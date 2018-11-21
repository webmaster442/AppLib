// http://www.silverlightshow.net/items/Book-Folding-effect-using-Pixel-Shader.aspx

sampler2D input : register(s0);
sampler2D input2 : register(s1);


/// <summary>Progress </summary>
/// <minValue>0</minValue>
/// <maxValue>1</maxValue>
/// <defaultValue>0</defaultValue>
float progress : register(c0);


float4 transform(float2 uv : TEXCOORD) : COLOR
{
	float right = 1 - progress;
	// transforming the curent point (uv) according to the new boundaries. 
	float2 tuv = float2((uv.x - progress) / (right - progress), uv.y);
	float tx = tuv.x;
	if (tx > 0.5)
	{
		tx = 1 - tx;
	}
	float top = progress * tx;
	float bottom = 1 - top;
	if (uv.y >= top && uv.y <= bottom)
	{
		//linear interpolation between 0 and 1 considering the angle of folding.
		float ty = lerp(0, 1, (tuv.y - top) / (bottom - top));
		// get the pixel from the transformed x and interpolated y.
		return tex2D(input2, float2(tuv.x, ty));
	}
	return  tex2D(input, uv);
}

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float right = 1 - progress;
	if (uv.x > progress && uv.x < right)
	{
		return transform(uv);
	}
	return  tex2D(input, uv);
}