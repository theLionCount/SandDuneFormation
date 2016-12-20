float4x4 MatrixTransform;
void SpriteVertexShader(inout float4 vColor : COLOR0, inout float2 texCoord : TEXCOORD0, inout float4 position : POSITION0)
{
    position = mul(position, MatrixTransform);
}

uniform extern texture ScreenTexture;
sampler ScreenS = sampler_state
{
    Texture = <ScreenTexture>;
	AddressU = Wrap;
    AddressV = Wrap;
};


texture shadow;
sampler2D shadowsmplr = sampler_state {
    Texture = (shadow);
    MinFilter = Point;
    MagFilter = Point;
    MipFilter = Point;
    AddressU = Wrap;
    AddressV = Wrap;
};

int blur;

float pixelWidth, pixelHeight;
float2 hopDistance;
float sandHeight;
float repHeight;
float pixelSize;
float avalancheHeight;

float P;

float2 avalancheOrder[4];

float RandomSeed[32];
static int RandomNum=0;
float Noise(float2 xy)
{
    float2 noise = (frac(sin(dot(xy ,float2(12.9898,78.233)*2.0)) * RandomSeed[RandomNum+3] * 2 * 43758.5453));
    return abs(noise.x + noise.y) * 0.5;
}


float Random(float2 xy)
{
    RandomNum++;
    return Noise(xy+float2(RandomSeed[RandomNum],RandomSeed[RandomNum+1])*RandomSeed[RandomNum+2]*10);
}



float4 PixelShaderFunction(float2 texCoord: TEXCOORD0) : COLOR
{
    float4 t1 = tex2D(ScreenS,texCoord);
    float s = t1.g;
    float h = t1.r;
	
	
    if (Random(texCoord)<P || h<=0 || s>h)
		return float4(0,0,h,0);
    int i=0;
    h-=sandHeight;
    float2 t = texCoord;
    while (i<16)
	{
        i++;
        t+=hopDistance;
        t1 = tex2Dlod(ScreenS,float4(t.xy,0,0));
        float r = Random(texCoord);
        if (t1.g>t1.r || r<0.4)
			return float4(frac(t.x),frac(t.y),h,1);
		else if (r<0.4+0.6*0.6 && t1.r>0)
			return float4(frac(t.x),frac(t.y),h,1);
    }
	h+=sandHeight;
    return float4(0,0,h,0);
}

float4 TranslatePixelShader(float2 texCoord: TEXCOORD0) : COLOR
{
    float4 t1 = tex2D(ScreenS,texCoord);
	
    float h = t1.b;
    float s = t1.a;
	
    int i=0;
    float2 t=texCoord;
    float4 t2;
    while (i<16)
	{
        i++;
        t-=hopDistance;
		
        t2=tex2Dlod(ScreenS,float4(t.xy,0,0));
		
		if (t2.a>0.5 && t2.x>=texCoord.x-pixelSize/2 && t2.y >= texCoord.y-pixelSize/2 && t2.x<=texCoord.x+pixelSize/2 && t2.y <= texCoord.y+pixelSize/2)
			h+=sandHeight;
		
		
   
    }
	return float4(h,0,0,1);
}

float4 PreAvalancePixelShader(float2 texCoord: TEXCOORD0) : COLOR
{
    float4 tt1 = tex2D(ScreenS,texCoord);
    float h = tt1.r;
	float2 t1 = avalancheOrder[0];
	float2 t2 = avalancheOrder[1];
	float2 t3 = avalancheOrder[2];
	float2 t4 = avalancheOrder[3];
	
	if (tex2D(ScreenS,texCoord+t1).r<h-avalancheHeight)
		return float4(h-sandHeight,frac(texCoord+t1).xy,1);
	if (tex2D(ScreenS,texCoord+t2).r<h-avalancheHeight)
		return float4(h-sandHeight,frac(texCoord+t2).xy,1);
	if (tex2D(ScreenS,texCoord+t3).r<h-avalancheHeight)
		return float4(h-sandHeight,frac(texCoord+t3).xy,1);
	if (tex2D(ScreenS,texCoord+t4).r<h-avalancheHeight)
		return float4(h-sandHeight,frac(texCoord+t4).xy,1);

	return float4(h,0,0,0);
}

float4 AvalancePixelShader(float2 texCoord: TEXCOORD0) : COLOR
{
	float4 t1 = tex2D(ScreenS,texCoord);
    float h = t1.r;
	float3 c;
	c = tex2D(ScreenS,texCoord+float2(pixelSize,0)).gba;
    if (c.z>0.2 && c.x==texCoord.x && c.y==texCoord.y)
		h+=sandHeight;
	c = tex2D(ScreenS,texCoord+float2(-pixelSize,0)).gba;
	if (c.z>0.2 && c.x==texCoord.x && c.y==texCoord.y)
		h+=sandHeight;
	c = tex2D(ScreenS,texCoord+float2(0,pixelSize)).gba;
	if (c.z>0.2 && c.x==texCoord.x && c.y==texCoord.y)
		h+=sandHeight;
	c = tex2D(ScreenS,texCoord+float2(0,-pixelSize)).gba;
	if (c.z>0.2 && c.x==texCoord.x && c.y==texCoord.y)
		h+=sandHeight;
	return float4(h,0,0,1);
}

float4 ShadowCorrectionFunction(float2 texCoord: TEXCOORD0) : COLOR
{
    float4 initial = tex2D(ScreenS,texCoord*0.8+0.1);
	float2 tcoord = texCoord*0.8+0.1;
	
	tcoord.x=((tcoord.x<0.2)?tcoord.x+0.8:((tcoord.x>0.8)?tcoord.x-0.8:tcoord.x));
	tcoord.y=((tcoord.y<0.2)?tcoord.y+0.8:((tcoord.y>0.8)?tcoord.y-0.8:tcoord.y));
	if (tcoord.x==texCoord.x && tcoord.y==texCoord.y)
		return initial;
	float4 correction1 = tex2D(ScreenS,float2(texCoord.x*0.8+0.1,tcoord.y));
	float4 correction2 = tex2D(ScreenS,float2(tcoord.x,texCoord.y*0.8+0.1));
	float4 correction3 = tex2D(ScreenS,tcoord);
	return float4(initial.r, max(correction3.g,max(correction2.g,max(correction1.g,initial.g))),0,1);
}


float4 HBlurFunction(float2 texCoord: TEXCOORD0) : COLOR
{
    float4 ret=float4(0,0,0,0);
	for (int i=-blur;i<=blur;i++)
		ret+= tex2Dlod(ScreenS,float4(texCoord+float2(pixelSize*i,0),0,0));
	return ret/(2*blur+1);	
	
}

float4 VBlurFunction(float2 texCoord: TEXCOORD0) : COLOR
{
    float4 ret=float4(0,0,0,0);
	for (int i=-blur;i<=blur;i++)
		ret+= tex2Dlod(ScreenS,float4(texCoord+float2(0,pixelSize*i),0,0));
	return ret/(2*blur+1);
	
	
}

technique preAnimate
{
    pass Pass1
    {
        // TODO: set renderstates here.
		VertexShader = compile vs_3_0 SpriteVertexShader();
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
}

technique postAnimate
{
    pass Pass1
    {
        // TODO: set renderstates here.
		VertexShader = compile vs_3_0 SpriteVertexShader();
        PixelShader = compile ps_3_0 TranslatePixelShader();
		
    }
}

technique preAvalanche
{
    pass Pass1
    {
        // TODO: set renderstates here.
		VertexShader = compile vs_3_0 SpriteVertexShader();
        PixelShader = compile ps_3_0 PreAvalancePixelShader();
    }
}

technique avalanche
{
    pass Pass1
    {
        // TODO: set renderstates here.
		VertexShader = compile vs_3_0 SpriteVertexShader();
        PixelShader = compile ps_3_0 AvalancePixelShader();
    }
}

technique shadowCorrection
{
    pass Pass1
    {
        // TODO: set renderstates here.
		VertexShader = compile vs_3_0 SpriteVertexShader();
        PixelShader = compile ps_3_0 ShadowCorrectionFunction();
    }
}

technique horizontalBlur
{
    pass Pass1
    {
        // TODO: set renderstates here.
		VertexShader = compile vs_3_0 SpriteVertexShader();
        PixelShader = compile ps_3_0 HBlurFunction();
    }
}

technique verticalBlur
{
    pass Pass1
    {
        // TODO: set renderstates here.
		VertexShader = compile vs_3_0 SpriteVertexShader();
        PixelShader = compile ps_3_0 VBlurFunction();
    }
}


