Shader "Custom/HolyLightShockwave"
{
    Properties
    {
        _Progress ("Shockwave Radius", Float) = 0
        _MaxRadius ("Max Radius", Float) = 12
        _QuadSize ("Quad Size", Float) = 64
        _RingWidth ("Ring Width", Float) = 0.35
        _TrailWidth ("Trail Width", Float) = 3.5
        _RayCount ("God Ray Count", Float) = 20
        _RayIntensity ("God Ray Intensity", Range(0, 2)) = 0.9
        _CoreColor ("Core Color", Color) = (1, 0.98, 0.85, 1)
        _GlowColor ("Glow Color", Color) = (1, 0.8, 0.35, 1)
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "RenderType" = "Transparent"
            "IgnoreProjector" = "True"
            "PreviewType" = "Plane"
        }

        Blend SrcAlpha One
        ZWrite Off
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            float _Progress;
            float _MaxRadius;
            float _QuadSize;
            float _RingWidth;
            float _TrailWidth;
            float _RayCount;
            float _RayIntensity;
            fixed4 _CoreColor;
            fixed4 _GlowColor;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 local : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.local = (v.uv - 0.5) * _QuadSize;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float d = length(i.local);
                float R = max(_Progress, 0.001);
                float angle = atan2(i.local.y, i.local.x);

                // edge
                float edge = (d - R) / max(_RingWidth, 0.001);
                float ring = exp(-edge * edge);

                // trail shimmer
                float trail = 0.0;
                float behind = R - d;
                if (behind > 0.0)
                {
                    trail = exp(-behind / max(_TrailWidth, 0.001));
                    trail *= 0.8 + 0.2 * sin(behind * 5.0 - _Time.y * 6.0);
                }
                
                // rays
                float rayField = sin(angle * _RayCount + _Time.y * 0.6) * 0.5 + 0.5;
                rayField = pow(rayField, 8.0);
                float reach = R * 1.75 + 2.0;
                float rayFade = saturate(1.0 - d / reach);
                float rays = rayField * rayFade * _RayIntensity;
                rays *= 0.85 + 0.15 * sin(_Time.y * 9.0 + d * 2.5 + angle * 3.0);

                float fadeIn = saturate(R * 4.0);
                float fadeOut = saturate(1.1 - R / max(_MaxRadius, 0.001));

                float intensity = ring * 1.5 + trail * 0.9 + rays;

                fixed4 col;
                col.rgb = _CoreColor.rgb * (ring * 2.0 + rays * 0.5) + _GlowColor.rgb * (trail + rays);
                col.a = saturate(intensity * fadeIn * fadeOut);
                return col;
            }
            ENDCG
        }
    }
}
