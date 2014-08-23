sampler2D tex : register(S0);

bool negate;

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 col = tex2D(tex, uv);

	if (negate)
	{
		col = 1.0 - col;
	}
	
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