Shader "Hidden/ChocDino/UIFX/Blend-Glow" {
	Properties {
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		[PerRendererData] _ResultTex ("Sprite Texture", 2D) = "white" {}
		[PerRendererData] _SourceTex ("Source Texture", 2D) = "white" {}
		[PerRendererData] _FalloffTex ("Falloff Texture", 2D) = "white" {}
		[PerRendererData] _GradientTex ("Gradient Texture", 2D) = "white" {}
		_StencilComp ("Stencil Comparison", Float) = 8
		_Stencil ("Stencil ID", Float) = 0
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask ("Stencil Read Mask", Float) = 255
		_MaxDistance ("Max Distance", Float) = 128
		_FalloffParams ("Falloff Params", Vector) = (4,2,0,2.2)
		_GlowColor ("Glow Color", Vector) = (1,1,1,1)
		_GradientParams ("Gradient Params", Vector) = (1,1,1,1)
		_AdditiveFactor ("Additive Factor", Float) = 1
		_SourceAlpha ("Source Alpha", Float) = 1
		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
		_ColorMask ("Color Mask", Float) = 15
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
}