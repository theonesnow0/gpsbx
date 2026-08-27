Shader "Custom/ShadowOnly" {
    SubShader {
        Tags { "RenderType"="Opaque" "Queue"="Geometry-1" }
        Pass {
            ColorMask 0   // Отключаем отрисовку цвета
            ZWrite On     // Включаем запись в Z-буфер
            Offset 0, 0

            // Программный код для отбрасывания тени
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_shadowcaster

            #include "UnityCG.cginc"

            struct v2f {
                float4 pos : SV_POSITION;
            };

            v2f vert(appdata_base v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            float4 frag(v2f i) : SV_Target {
                return 0;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
