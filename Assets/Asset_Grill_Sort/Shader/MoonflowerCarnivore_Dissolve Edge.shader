Shader "MoonflowerCarnivore/Dissolve Edge" {
	Properties {
		[Enum(Off,0,Front,1,Back,2)] _CullMode ("Culling Mode", Float) = 0
		[Enum(Off,0,On,1)] _ZWrite ("ZWrite", Float) = 0
		_Progress ("Progress", Range(0, 1)) = 0
		_MainTex ("Main Texture", 2D) = "white" {}
		_DissolveTex ("Dissolve Texture", 2D) = "white" {}
		_Edge ("Edge", Range(0.01, 0.5)) = 0.01
		[Header(Edge Color)] [Toggle(EDGE_COLOR)] _UseEdgeColor ("Edge Color?", Float) = 1
		[HideIfDisabled(EDGE_COLOR)] [NoScaleOffset] _EdgeAroundRamp ("Edge Ramp", 2D) = "white" {}
		[HideIfDisabled(EDGE_COLOR)] _EdgeAround ("Edge Color Range", Range(0, 0.5)) = 0
		[HideIfDisabled(EDGE_COLOR)] _EdgeAroundPower ("Edge Color Power", Range(1, 5)) = 1
		[HideIfDisabled(EDGE_COLOR)] _EdgeAroundHDR ("Edge Color HDR", Range(0, 10)) = 1
		[HideIfDisabled(EDGE_COLOR)] _EdgeDistortion ("Edge Distortion", Range(0, 1)) = 0
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