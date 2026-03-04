Shader "Findout/UITextEffect/UGUITextEffect" {
	Properties {
		_Color ("Tint", Vector) = (1,1,1,1)
		_ShadowColor ("Shadow Color", Vector) = (0,0,0,0.5)
		_Input_MainTex_TexelSize ("_Input_MainTex_TexelSize", Vector) = (0,0,0,0)
		_ShadowX ("Shadow Offset X", Float) = 5
		_ShadowY ("Shadow Offset Y", Float) = 5
		_ShadowExtend ("Shadow Extend", Float) = 0
		_OutlineWidth ("Outline Width", Float) = 1
		_OutlineColor ("Outline Color", Vector) = (0,0,0,0.5)
		_ShadowSoftness ("Shadow Softness", Range(0, 1)) = 0
		_UIScale ("UIScale", Float) = 1
		_StencilComp ("Stencil Comparison", Float) = 8
		_Stencil ("Stencil ID", Float) = 0
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask ("Stencil Read Mask", Float) = 255
		_ColorMask ("Color Mask", Float) = 15
		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = _Color.rgb;
			o.Alpha = _Color.a;
		}
		ENDCG
	}
}