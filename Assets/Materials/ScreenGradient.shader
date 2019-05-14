//Shader "Custom/ScreenGradient" { 
//    SubShader { 
//       Pass { 
//          CGPROGRAM 
//  
//          #include "UnityCG.cginc"
// 
//          #pragma vertex vert  
//          #pragma fragment frag 
//  
//          struct vertexInput {
//             float4 vertex : POSITION;
//          };
//          struct vertexOutput {
//             float4 pos : SV_POSITION;
//             float4 vertPos : TEXCOORD5; 
//          };
//  
//          vertexOutput vert(vertexInput input) {
//             vertexOutput output;
// 
//             output.pos = mul(UNITY_MATRIX_MVP, input.vertex);
// 
//             // We're now in clipspace, ranging from -1,-1,-1, to 1,1,1
//             float4 clipSpace =  mul(UNITY_MATRIX_MVP, input.vertex);
//		     clipSpace.xy /= clipSpace.w;
//		     clipSpace.xy = clipSpace.xy * 0.5f + 0.5f;
//
// 
//             output.vertPos = clipSpace;
//
//             return output;
//          }
//  
//          float4 frag(vertexOutput input) : COLOR {
//             return float4(input.vertPos.x, input.vertPos.x, input.vertPos.x, 1.0); 
//          }
//  
//          ENDCG  
//       }
//    }
// }
//Shader "Custom/NewShader" {
//        Properties {
//            _MainTex ("Base (RGB)", 2D) = "white" {}
//		_Color("Color1", Color) = (1.000000,1.000000,1.000000,1.000000)
//		_Color2("Color2", Color) = (1.000000,1.000000,1.000000,1.000000)
//        }
//        SubShader {
//            Tags { "RenderType" = "Opaque" }
//            LOD 200
//            Pass{
//            CGPROGRAM
//			#pragma vertex vert
//            #pragma fragment frag
//            #pragma target 2.0
//            	#include "UnityCG.cginc"
//            sampler2D _MainTex;
//
//            struct Input {
//                float2 uv_MainTex;
//            };
//
//        		float4 _MainTex_ST;
//			// vertex shader input data
//			struct appdata {
//				float3 pos : POSITION;
//				float3 uv0 : TEXCOORD0;
//			};
//            		// vertex-to-fragment interpolators
//				struct v2f {
//					fixed4 color : COLOR0;
//					float2 uv0 : TEXCOORD0;
//					#if USING_FOG
//						fixed fog : TEXCOORD1;
//					#endif
//					float4 pos : SV_POSITION;
//					float4 screenPos: TEXCOORD2;
//				};
//
//			// vertex shader
//			v2f vert(appdata IN) {
//				v2f o;
//				half4 color = half4(0,0,0,1.1);
//				float3 eyePos = mul(UNITY_MATRIX_MV, float4(IN.pos,1)).xyz;
//				half3 viewDir = 0.0;
//				o.color = saturate(color);
//				// compute texture coordinates
//				o.uv0 = IN.uv0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
//
////				float2 screenPosition = (IN.screenPos.xy / IN.screenPos.w);
////				fixed4 c = lerp(_Color, _Color2, screenPosition.y);
////				o.color = saturate(c);
//
//				// fog
//				#if USING_FOG
//					float fogCoord = length(eyePos.xyz); // radial fog distance
//					UNITY_CALC_FOG_FACTOR(fogCoord);
//					o.fog = saturate(unityFogFactor);
//				#endif
//				// transform position
//				o.pos = UnityObjectToClipPos(IN.pos);
//				o.screenPos = ComputeScreenPos(o.pos);
//				return o;
//			}
//
//			struct SurfaceOutput
//			{
//			    fixed3 Albedo;  // diffuse color
//			    fixed3 Normal;  // tangent space normal, if written
//			    fixed3 Emission;
//			    half Specular;  // specular power in 0..1 range
//			    fixed Gloss;    // specular intensity
//			    fixed Alpha;    // alpha for transparencies
//			};
//
//			// textures
//			fixed4 _Color;
//			fixed4 _Color2;
//
//			// fragment shader
//			fixed4 frag(v2f IN) : SV_Target{
//				fixed4 col;
//				fixed4 tex, tmp0, tmp1, tmp2;
//
//				tex = tex2D(_MainTex, IN.uv0.xy);
//
//				float2 screenPosition = (IN.screenPos.xy / IN.screenPos.w);
//
//				//float2 screenUV = IN.screenPos.xy / IN.screenPos.w;
//				fixed4 color = lerp(_Color, _Color2, screenPosition.y);
//
//				col.rgb = tex * color;
//				//col.a = tex.a * color.a;
//
//					// fog
//				#if USING_FOG
//					col.rgb = lerp(unity_FogColor.rgb, col.rgb, IN.fog);
//				#endif
//				return col;
//			}
//
//
//            void surf (Input IN, inout SurfaceOutput o) {
//                half4 c = tex2D (_MainTex, IN.uv_MainTex);
//                o.Albedo = c.rgb;
//              //  o.Alpha = c.a;
//            }
//
//            ENDCG
//        }
//        }
//        FallBack "Diffuse"
//    }

 Shader "Custom/Transparent Color Gradient" {
	Properties{
		_MainTex("Base (RGB) Trans (A)", 2D) = "white" { }
		_Color("Color1", Color) = (1.000000,1.000000,1.000000,1.000000)
		_Color2("Color2", Color) = (1.000000,1.000000,1.000000,1.000000)
	}

		SubShader{
			LOD 100
			Tags{ "QUEUE" = "Transparent" "IGNOREPROJECTOR" = "true" "RenderType" = "Transparent" }
			Pass{
			Tags{ "QUEUE" = "Transparent" "IGNOREPROJECTOR" = "true" "RenderType" = "Transparent" }
			Blend SrcAlpha OneMinusSrcAlpha
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0
			#include "UnityCG.cginc"
		      #include "UnityLightingCommon.cginc" // for _LightColor0
	           #include "AutoLight.cginc"
			// uniforms
			float4 _MainTex_ST;

			// vertex shader input data
//			struct appdata {
//				float3 pos : POSITION;
//				float3 uv0 : TEXCOORD0;
//				fixed4 normal: ;
//			};

			// vertex-to-fragment interpolators
			struct v2f {
				fixed4 color : COLOR0;
				float2 uv0 : TEXCOORD0;
				float4 pos : SV_POSITION;
		      	fixed4 diff : COLOR1; // diffuse lighting color
				float4 screenPos: TEXCOORD2;
				    LIGHTING_COORDS(0,1)
			};

			// vertex shader
			v2f vert(appdata_base IN) {
				v2f o;
				half4 color = half4(0,0,0,1);
				float3 eyePos = mul(UNITY_MATRIX_MV, float4(IN.texcoord)).xyz;
				half3 viewDir = 0.0;
				o.color = saturate(color);
				// compute texture coordinates
				o.uv0 = IN.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				// transform position
				o.pos = UnityObjectToClipPos(IN.vertex);
				o.screenPos = ComputeScreenPos(o.pos);
		      half3 worldNormal = UnityObjectToWorldNormal(IN.normal);
                // dot product between normal and light direction for
                // standard diffuse (Lambert) lighting
                half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
                // factor in the light color
                o.diff = nl * _LightColor0;
				return o;
			}

			// textures
			sampler2D _MainTex;
			fixed4 _Color;
			fixed4 _Color2;

			// fragment shader
			fixed4 frag(v2f IN) : SV_Target{
				fixed4 col;
				fixed4 tex;
				// SetTexture #0
				tex = tex2D(_MainTex, IN.uv0.xy);

				float2 screenPosition = (IN.screenPos.xy / IN.screenPos.w);
		    	fixed4 c = lerp(_Color, _Color2, screenPosition.y);

			    col = tex2D(_MainTex, IN.uv0); 
		      	col = IN.diff;
			    col *= c;
		    	col.a = 1;
			
			     return col;
				
			}

		

			// texenvs
			//! TexEnv0: 01010102 01050106 [_MainTex] [_Color]
			ENDCG
		}
	}
}
