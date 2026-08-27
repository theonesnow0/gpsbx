Shader "Custom/ChromaKeyShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ChromaKeyColor ("Chroma Key Color", Color) = (0,1,0,1)
        _Tolerance ("Tolerance", Range(0,1)) = 0.1
        _Smoothing ("Smoothing", Range(0,1)) = 0.1
    }
    SubShader
    {
        Tags {"Queue" = "Transparent" "RenderType"="Transparent"}
        LOD 100

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float2 texcoord : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _ChromaKeyColor;
            float _Tolerance;
            float _Smoothing;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 texColor = tex2D(_MainTex, i.texcoord);

                // Calculate distance between chroma key color and texture color
                float diff = distance(texColor.rgb, _ChromaKeyColor.rgb);

                // Apply tolerance and smoothing
                float alpha = smoothstep(_Tolerance, _Tolerance + _Smoothing, diff);

                // Output final color with modified alpha
                texColor.a = alpha;
                return texColor;
            }
            ENDCG
        }
    }
}