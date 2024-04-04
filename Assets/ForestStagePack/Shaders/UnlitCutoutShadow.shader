Shader "Fatty/Transparent Cutout (Shadow)"
{
	Properties
	{
		_Color("Main Color", COLOR) = (1,1,1,1)
		_MainTex("Base Texture", 2D) = "white" {}
	_Cutoff("Alpha cutoff", Range(0,1)) = 0.5
	}
		SubShader
	{

		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		LOD 100

		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile_fog

#include "UnityCG.cginc"

		struct VertexInput { float4 vertex : POSITION; float2 uv : TEXCOORD0; };
	struct VertexOutput { float4 vertex : SV_POSITION; float2 uv : TEXCOORD0; UNITY_FOG_COORDS(1) };

	float4 _Color;
	sampler2D _MainTex;
	float4 _MainTex_ST;
	float _Cutoff;

	VertexOutput vert(VertexInput v)
	{
		VertexOutput o;
		float4 pos0 = mul(unity_ObjectToWorld, v.vertex);
		o.vertex = mul(UNITY_MATRIX_VP, pos0);
		o.uv = TRANSFORM_TEX(v.uv, _MainTex);
		UNITY_TRANSFER_FOG(o, o.vertex);
		return o;
	}

	fixed4 frag(VertexOutput i) : SV_Target
	{
		// sample the texture
		fixed4 col = tex2D(_MainTex, i.uv);
	col *= _Color;	//is same col = col * _Color;

	if (col.a < _Cutoff)	// alpha value less than user-specified threshold?
	{
		discard; // yes: discard this fragment
	}

	// apply fog
	UNITY_APPLY_FOG(i.fogCoord, col);

	return col;
	}
		ENDCG
	}
	}

		Fallback "Transparent/Cutout/VertexLit"
}
