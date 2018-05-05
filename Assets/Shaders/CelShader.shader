// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/CelShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	_Treshold("Cel treshold", Range(1., 20.)) = 5.
		_Ambient("Ambient intensity", Range(0., 0.5)) = 0.1
	}
		SubShader
	{
		Tags{ "RenderType" = "Opaque" "LightMode" = "ForwardBase" }
		

		Pass
	{

		

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile_fwdbase

#include "UnityCG.cginc"
#include "AutoLight.cginc"

	struct v2f
	{
		float4 pos : SV_POSITION;
		float2 uv : TEXCOORD0;
		float3 worldNormal : NORMAL;
		LIGHTING_COORDS(0, 1)
	};

	float _Treshold;

	float LightToonShading(float3 normal, float3 lightDir)
	{
		float NdotL = max(0.0, dot(normalize(normal), normalize(lightDir)));
		return floor(NdotL * _Treshold) / (_Treshold - 0.5);
	}

	sampler2D _MainTex;
	float4 _MainTex_ST;

	v2f vert(appdata_full v)
	{
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
		o.worldNormal = mul(v.normal.xyz, (float3x3) unity_WorldToObject);
		TRANSFER_VERTEX_TO_FRAGMENT(o);
		return o;
	}

	fixed4 _LightColor0;
	half _Ambient;

	fixed4 frag(v2f i) : SV_Target
	{
		fixed4 col = tex2D(_MainTex, i.uv);
	col.rgb *= saturate(LightToonShading(i.worldNormal, -_WorldSpaceLightPos0.xyz) + _Ambient) * _LightColor0.rgb;
	float attenuation = LIGHT_ATTENUATION(i);
	return col;
	}
		ENDCG
	}
	}

	Fallback "VertexLit"
}