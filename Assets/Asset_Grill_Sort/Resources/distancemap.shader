Shader "Hidden/ChocDino/UIFX/DistanceMap" {
	Properties {
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		[PerRendererData] _InsideTex ("Inside Texture", 2D) = "white" {}
		_StepSize ("Step Size", Vector) = (1,1,0,0)
		_DownSample ("Down Sample", Float) = 1
		[KeywordEnum(Square,Diamond,Circle)] Dist ("Dist", Float) = 0
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