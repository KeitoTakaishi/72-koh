// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

shader "Custom/Sprite-Grayscale" {
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		[HDR]
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Fog { Mode Off }
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
// Upgrade NOTE: excluded shader from DX11, OpenGL ES 2.0 because it uses unsized arrays
#pragma exclude_renderers d3d11 gles
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile DUMMY PIXELSNAP_ON
			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				half2 texcoord  : TEXCOORD0;
			};
			
			fixed4 _Color;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			sampler2D _MainTex;

			fixed4 frag(v2f IN) : COLOR
			{
				half4 tex = tex2D(_MainTex, IN.texcoord) * IN.color;
				tex.rgb = _Color;
				
				//blur
				float weight[5];
				weight[0] = 0.382928;
				weight[1] = 0.241732;
				weight[2] = 0.060598;
				weight[3] = 0.005977;
				weight[4] = 0.000229;
				
				fixed3 color = fixed3(0.0, 0.0, 0.0);
				fixed2 uv = IN.texcoord;
				float strength = 0.01;
				color += tex2D(_MainTex, uv + fixed2(0.0, 0.0)).rgb * weight[0];
                
                
               
                for(int i = 1; i < 5; i++) {
                    color += tex2D(_MainTex, uv + fixed2(strength * i, 0.0)).rgb * weight[i];
                    color += tex2D(_MainTex, uv - fixed2(strength * i, 0.0)).rgb * weight[i];
                }
                
                for(int i = 1; i < 5; i++) {
                    color += tex2D(_MainTex, uv + fixed2(0.0, strength * i)).rgb * weight[i];
                    color += tex2D(_MainTex, uv - fixed2(0.0, strength * i)).rgb * weight[i];
                }
                
                
                fixed4 col =fixed4(color.r, color.g, color.b, 1.0);
                
                
                const float gamma = 2.2;
                tex += col;
                fixed3 result = fixed3(1.0, 1.0, 1.0) - fixed3(-tex.r,-tex.g,-tex.b);
                result = pow(result, fixed3(1.0 / gamma, 1.0 / gamma, 1.0 / gamma));
                
                return tex;
            }
            
		ENDCG
		}
	}
}