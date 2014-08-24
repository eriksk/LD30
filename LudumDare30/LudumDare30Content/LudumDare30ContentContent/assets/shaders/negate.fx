sampler2D tex : register(S0);

bool negate;

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 col = tex2D(tex, uv);

	if (negate)
	{
		col = 1.0 - col;
	}
	
	col.rgb = (col.r + col.g + col.b) / 3.0;
	
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