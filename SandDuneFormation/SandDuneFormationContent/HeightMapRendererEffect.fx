float4x4 World;
float4x4 View;
float4x4 Projection;
int material;
texture text;
sampler2D smplr = sampler_state {
    Texture = (text);
    MinFilter = Point;
    MagFilter = Point;
    AddressU = Clamp;
    AddressV = Clamp;
}
;
texture height;
sampler2D heightsmplr = sampler_state {
    Texture = (height);
    MinFilter = Point;
    MagFilter = Point;
    MipFilter = Point;
    AddressU = Clamp;
    AddressV = Clamp;
}
;
texture shadow;
sampler2D shadowsmplr = sampler_state {
    Texture = (shadow);
    MinFilter = Point;
    MagFilter = Point;
    MipFilter = Point;
    AddressU = Clamp;
    AddressV = Clamp;
}
;
float mapsize;
float maxheight;
float mpixelsize;
float musize;
struct VertexShaderInput
{
    float4 Position : POSITION0;
    float2 TextCoord : TEXCOORD0;
}
;
struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float3 Normal : TEXCOORD0;
    float2 TextCoord : TEXCOORD1;
    float height : TEXCOORD2;
    float2 tpos : TEXCOORD3;
}
;
struct ShadowVertexShaderOutput
{
    float4 Position : POSITION0;
    float4 pos : TEXCOORD1;
    float Height : TEXCOORD0;
	float2 tcoord : TEXCOORD2;
}
;
VertexShaderOutput heightFunction(VertexShaderInput input)
{
    float4 pos=input.Position;
    float2 htex = float2(mpixelsize/2.0f, mpixelsize/2.0f);
    float2 texcoord=float2(pos.x/mapsize+0.5, pos.z/mapsize+0.5)-htex;

	float4 v1,v2,v3,v4,v5,v6;
    v1=float4(pos.x+musize, tex2Dlod(heightsmplr, float4(texcoord.x+mpixelsize,texcoord.y,0,0)).r*maxheight, pos.z, 1)-pos; //+x
    v2=float4(pos.x, tex2Dlod(heightsmplr, float4(texcoord.x,texcoord.y+mpixelsize,0,0)).r*maxheight,pos.z+musize,1)-pos;	//+y
    v3=float4(pos.x-musize, tex2Dlod(heightsmplr, float4(texcoord.x-mpixelsize,texcoord.y+mpixelsize,0,0)).r*maxheight,pos.z+musize,1)-pos;
    v4=float4(pos.x-musize, tex2Dlod(heightsmplr, float4(texcoord.x-mpixelsize,texcoord.y,0,0)).r*maxheight,pos.z,1)-pos;	//-x
    v5=float4(pos.x, tex2Dlod(heightsmplr, float4(texcoord.x,texcoord.y-mpixelsize,0,0)).r*maxheight,pos.z-musize,1)-pos;	//-y
    v6=float4(pos.x+musize, tex2Dlod(heightsmplr, float4(texcoord.x+mpixelsize,texcoord.y-mpixelsize,0,0)).r*maxheight,pos.z-musize,1)-pos;

    VertexShaderOutput output;
    output.tpos = texcoord;
    output.height=tex2Dlod(heightsmplr, float4(texcoord.xy,0,0)).r;
    pos.y=(output.height*maxheight +v1.g+v2.g+v4.g+v5.g)/5*2;
	//pos.y=output.height*maxheight;
    float4 worldPosition = mul(pos, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);
	

    output.TextCoord=input.TextCoord;
    
    float3 rnormal=-normalize(cross(v1,v2))-normalize(cross(v2,v3))-normalize(cross(v3,v4))-normalize(cross(v4,v5))-normalize(cross(v5,v6))-normalize(cross(v6,v1));
    rnormal=-(cross(v1,v2))-(cross(v2,v3))-(cross(v3,v4))-(cross(v4,v5))-(cross(v5,v6))-(cross(v6,v1));
    output.Normal=normalize(mul(float4(rnormal.xyz,0),World));
    return output;
}


ShadowVertexShaderOutput shadowVertexFunction(VertexShaderInput input)
{
    ShadowVertexShaderOutput output;
    float4 pos=input.Position;
    float2 htex = float2(mpixelsize/2.0f, mpixelsize/2.0f);
    float2 texcoord=float2(pos.x/mapsize+0.5, pos.z/mapsize+0.5)-htex;
	output.tcoord = texcoord;
    output.Height=tex2Dlod(heightsmplr, float4(texcoord.xy,0,0)).r;
    pos.y=output.Height*maxheight;
    float4 worldPosition = mul(pos, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);
    output.pos=output.Position;
    return output;
}

 void InstancingVertexShader(inout float4 position : POSITION0, in float4 world : POSITION1)
 {
	position = position+world;
 }

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
    // TODO: add your pixel shader code here.
    return float4(tex2D(smplr,input.TextCoord).rgb*dot(input.Normal,float3(1,1,1)),1);//float4(input.tpos.x,input.tpos.y,0,1)*dot(input.Normal,float3(1,1,1)); //
}


float4 PixelShaderFunctionWithShadowz(VertexShaderOutput input) : COLOR0
{
    // TODO: add your pixel shader code here.
    return float4(tex2D(smplr,input.TextCoord).rgb*dot(input.Normal,float3(1,1,1))*(tex2D(shadowsmplr,input.tpos).r>input.height?0:1),1);
}


float4 ShadowPixelShaderFunction(ShadowVertexShaderOutput input) : COLOR0
{
    // TODO: add your pixel shader code here.
    return float4(0,input.Height,0,1);
}


technique heightMap
{
    pass Pass1
    {
        // TODO: set renderstates here.
		CULLMODE=NONE;
        FillMode = Solid;//WireFrame;//
        ZENABLE=true;

        VertexShader = compile vs_3_0 heightFunction();
        PixelShader = compile ps_3_0 PixelShaderFunction();//PixelShaderFunctionWithShadowz();//
    }

}

technique shadowMap
{
    pass Pass1
    {
        // TODO: set renderstates here.
		CULLMODE=NONE;
        FillMode = Solid;
		ZENABLE=true;
        VertexShader = compile vs_3_0 shadowVertexFunction();
        PixelShader = compile ps_3_0 ShadowPixelShaderFunction();
    }

}
