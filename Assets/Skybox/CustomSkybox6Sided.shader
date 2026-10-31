Shader "Custom/Skybox 6 Sided"
{
    Properties {
        _Tint ("Tint Color", Color) = (0.5, 0.5, 0.5, 0.5)
        [Gamma] _Exposure ("Exposure", Range(0, 8)) = 1.0
        _Rotation ("Rotation", Range(0, 360)) = 0.0
        
        [NoScaleOffset] _FrontTex ("Front [+Z] (HDR)", 2D) = "grey" {}
        [NoScaleOffset] _BackTex ("Back [-Z] (HDR)", 2D) = "grey" {}
        [NoScaleOffset] _LeftTex ("Left [+X] (HDR)", 2D) = "grey" {}
        [NoScaleOffset] _RightTex ("Right [-X] (HDR)", 2D) = "grey" {}
        [NoScaleOffset] _UpTex ("Up [+Y] (HDR)", 2D) = "grey" {}
        [NoScaleOffset] _DownTex ("Down [-Y] (HDR)", 2D) = "grey" {}
    }

    SubShader {
        Tags { "QUEUE"="Background" "RenderType"="Background" "PreviewType"="Skybox" }
        ZWrite Off
        Cull Off

        CGINCLUDE
        #define UNITY_PI 3.14159265359

        uniform sampler2D _FrontTex;
        uniform sampler2D _BackTex;
        uniform sampler2D _LeftTex;
        uniform sampler2D _RightTex;
        uniform sampler2D _UpTex;
        uniform sampler2D _DownTex;

        uniform half4 _FrontTex_HDR;
        uniform half4 _BackTex_HDR;
        uniform half4 _LeftTex_HDR;
        uniform half4 _RightTex_HDR;
        uniform half4 _UpTex_HDR;
        uniform half4 _DownTex_HDR;

        uniform half4 _Tint;
        uniform half _Exposure;
        uniform float _Rotation;

        struct appdata_t {
            float4 vertex : POSITION;
            float2 texcoord : TEXCOORD0;
        };

        struct v2f {
            float4 vertex : SV_POSITION;
            float2 texcoord : TEXCOORD0;
        };

        // 独自定義したY軸回転関数
        float3 RotateAroundYInDegrees (float3 vertex, float degrees)
        {
            float alpha = degrees * UNITY_PI / 180.0;
            float sina, cosa;
            sincos(alpha, sina, cosa);
            float2x2 m = float2x2(cosa, -sina, sina, cosa);
            vertex.xz = mul(m, vertex.xz);
            return vertex;
        }

        v2f vert (appdata_t v)
        {
            v2f o;
            float3 rotatedVertex = RotateAroundYInDegrees(v.vertex.xyz, _Rotation);
            // UnityObjectToClipPos の代わりに直接 MVP行列（unity_MatrixVP * ObjectToWorld）で計算
            float4 worldPos = mul(unity_ObjectToWorld, float4(rotatedVertex, 1.0));
            o.vertex = mul(unity_MatrixVP, worldPos);
            o.texcoord = v.texcoord;
            return o;
        }

        // HDR（RGBMなど）のデコード処理
        half4 DecodeHDRCustom(half4 data, half4 hdr)
        {
            half uc = data.a * hdr.w + 1.0;
            half l = log2(uc) * hdr.y;
            half mult = exp2(l) * hdr.x;
            return half4(data.rgb * mult, 1.0);
        }

        half4 fragCommon (float2 uv, sampler2D tex, half4 hdr)
        {
            half4 texCol = tex2D(tex, uv);
            half4 decodedCol = DecodeHDRCustom(texCol, hdr);
            
            // 色味と露出の調整
            half3 finalRgb = decodedCol.rgb * _Tint.rgb * _Exposure;
            return half4(finalRgb, 1.0);
        }
        ENDCG

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            half4 frag (v2f i) : SV_Target { return fragCommon(i.texcoord, _FrontTex, _FrontTex_HDR); }
            ENDCG
        }
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            half4 frag (v2f i) : SV_Target { return fragCommon(i.texcoord, _BackTex, _BackTex_HDR); }
            ENDCG
        }
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            half4 frag (v2f i) : SV_Target { return fragCommon(i.texcoord, _LeftTex, _LeftTex_HDR); }
            ENDCG
        }
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            half4 frag (v2f i) : SV_Target { return fragCommon(i.texcoord, _RightTex, _RightTex_HDR); }
            ENDCG
        }
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            half4 frag (v2f i) : SV_Target { return fragCommon(i.texcoord, _UpTex, _UpTex_HDR); }
            ENDCG
        }
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            half4 frag (v2f i) : SV_Target { return fragCommon(i.texcoord, _DownTex, _DownTex_HDR); }
            ENDCG
        }
    }
}