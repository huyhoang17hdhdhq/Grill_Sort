Shader "Hidden/ChocDino/UIFX/Extrude" {
	Properties {
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		[PerRendererData] _GradientTex ("Gradient Texture", 2D) = "grey" {}
		_Length ("Length", Float) = 32
		_PixelStep ("Pixel Step", Vector) = (0.01,0.01,0,0)
		_VanishingPoint ("Vanishing Point", Vector) = (0.5,0.5,0,0)
		_Ratio ("Ratio", Vector) = (1,1,0,0)
		_ColorFront ("Color Front", Vector) = (0.5,0.5,0.5,1)
		_ColorBack ("Color Back", Vector) = (0,0,0,0)
		_ReverseFill ("Reverse Fill", Float) = 0
		_Scroll ("Scroll", Float) = 0
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