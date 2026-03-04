Shader "Hidden/ChocDino/UIFX/ColorAdjust" {
	Properties {
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_BCPO ("BCPO", Vector) = (0,0,255,1)
		_BrightnessRGBA ("Brightness RGBA", Vector) = (0,0,0,0)
		_ContrastRGBA ("Contrast RGBA", Vector) = (0,0,0,0)
		_PosterizeRGBA ("Posterize RGBA", Vector) = (255,255,255,255)
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