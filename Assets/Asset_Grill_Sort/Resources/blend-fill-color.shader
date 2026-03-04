Shader "Hidden/ChocDino/UIFX/Blend-Fill-Color" {
	Properties {
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		[PerRendererData] _ResultTex ("Sprite Texture", 2D) = "white" {}
		[PerRendererData] _SourceTex ("Source Texture", 2D) = "white" {}
		_StencilComp ("Stencil Comparison", Float) = 8
		_Stencil ("Stencil ID", Float) = 0
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask ("Stencil Read Mask", Float) = 255
		_FillColor ("Fill Color", Vector) = (1,1,1,1)
		_FillColorA ("Fill Color A", Vector) = (1,1,1,1)
		_FillColorB ("Fill Color B", Vector) = (0,0,0,1)
		_FillColorTL ("Fill Color Top Left", Vector) = (1,1,1,1)
		_FillColorTR ("Fill Color Top Right", Vector) = (1,1,1,1)
		_FillColorBL ("Fill Color Bottom Left", Vector) = (0,0,0,1)
		_FillColorBR ("Fill Color Bottom Right", Vector) = (0,0,0,1)
		_ColorScaleBias ("", Vector) = (1,0,0,0)
		_Strength ("Strength", Float) = 1
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