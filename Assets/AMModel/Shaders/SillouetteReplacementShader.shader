﻿Shader "AMShaders/SillouetteReplacementShader"
{
	Properties
	{
		_AM_Sharpness("Sharpness", Range(0.0, 10.0)) = 2.5
	}
	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			//"RenderType" = "Transparent"
			"RenderType" = "Opaque"
			"XRay" = "ColoredOutline"
		}

		LOD 200

		//ZWrite Off
		//ZTest Always
		Blend One One

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
				float3 viewDir : TEXCOORD1;
			};
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.normal = UnityObjectToWorldNormal(v.normal);
				o.viewDir = normalize(_WorldSpaceCameraPos.xyz - mul(_Object2World, v.vertex).xyz);
				return o;
			}

			float4 _AM_NSOutline;
			float4 _AM_HATint;
			float _AM_Sharpness;

			fixed4 frag (v2f i) : SV_Target
			{
				//_AM_Sharpness = 2.5f;
				float NdotV = dot(i.normal, i.viewDir) * _AM_Sharpness;
				float invNdotV = 1 - dot(i.normal, i.viewDir) * _AM_Sharpness;
				return _AM_NSOutline * invNdotV + NdotV * _AM_HATint;
			}
			ENDCG
		}
	}
	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			//"RenderType" = "Transparent"
			"RenderType" = "Opaque"
			"XRay" = "ColoredOutline"
		}

		LOD 200

		//ZWrite Off
		//ZTest Always
		Blend One One

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
				float3 viewDir : TEXCOORD1;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.normal = UnityObjectToWorldNormal(v.normal);
				o.viewDir = normalize(_WorldSpaceCameraPos.xyz - mul(_Object2World, v.vertex).xyz);
				return o;
			}

			float4 _AM_NSOutline;
			float4 _AM_HATint;
			float _AM_Sharpness;

			fixed4 frag(v2f i) : SV_Target
			{
				//_AM_Sharpness = 2.5f;
				float NdotV = dot(i.normal, i.viewDir) * _AM_Sharpness;
			float invNdotV = 1 - dot(i.normal, i.viewDir) * _AM_Sharpness;
			return _AM_NSOutline * invNdotV + NdotV * _AM_HATint;
			}
				ENDCG
		}
	}
}
