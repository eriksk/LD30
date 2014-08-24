sampler2D tex : register(S0);

bool negate;

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 col = tex2D(tex, uv);

	if (negate)
	{
		col = 1.0 - col;
	}
	
	// black and white
	//col.rgb = (col.r + col.g + col.b) / 4.0;

	// vignette
	float burn = 0.3;
	float d = sqrt(pow(uv.x - 0.5, 2) + pow(uv.y - 0.5, 2));
	col.rgb -= d * burn;

	// sat
	float saturation = 0.3;
	float a = col.r + col.g + col.b;
	a /= 3.0;
	a *= 1.0 - saturation;
	col.r = (col.r * saturation + a);
	col.g = (col.g * saturation + a);
	col.b = (col.b * saturation + a);
	
	col.a = 1.0;

	return col;
}

technique Technique1
{
	pass Pass1
	{
		PixelShader = compile ps_2_0 main();
	}
}