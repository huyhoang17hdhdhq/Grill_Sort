Shader "Hidden/ChocDino/UIFX/Blend-Frame" {
	Properties {
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		[PerRendererData] _ResultTex ("Sprite Texture", 2D) = "white" {}
		[PerRendererData] _SourceTex ("Source Texture", 2D) = "white" {}
		[PerRendererData] _FillTex ("Fill Texture", 2D) = "white" {}
		[PerRendererData] _BorderFillTex ("Border Fill Texture", 2D) = "white" {}
		_StencilComp ("Stencil Comparison", Float) = 8
		_Stencil ("Stencil ID", Float) = 0
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask ("Stencil Read Mask", Float) = 255
		_CutoutAlpha ("Cutout Alpha", Float) = 1
		_FillSoft ("Fill Soft", Float) = 1
		_FillColor ("Fill Color", Vector) = (0.2,0.2,0.2,1)
		_GradientAxisParams ("Axis", Vector) = (1,0,0,0)
		_GradientParams ("Gradient", Vector) = (1,0,0,0)
		_EdgeRounding ("Edge Rounding", Vector) = (0,0,0,0)
		_BorderColor ("Border Color", Vector) = (0.8,0.8,0.8,1)
		_BorderGradientAxisParams ("Border Axis", Vector) = (1,0,0,0)
		_BorderGradientParams ("Border Gradient", Vector) = (1,0,0,0)
		_BorderSizeSoft ("Border Size,Soft", Vector) = (4,1,0,0)
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