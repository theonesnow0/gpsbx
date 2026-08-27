Shader "Custom/UVShaderWithLightingAndOffset"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _UVScaleX ("UV Scale X", Vector) = (1, 1, 0, 0)
        _UVScaleY ("UV Scale Y", Vector) = (1, 1, 0, 0)
        _UVScaleZ ("UV Scale Z", Vector) = (1, 1, 0, 0)
        _UVOffsetX ("UV Offset X", Vector) = (0, 0, 0, 0)
        _UVOffsetY ("UV Offset Y", Vector) = (0, 0, 0, 0)
        _UVOffsetZ ("UV Offset Z", Vector) = (0, 0, 0, 0)
        _Metallic ("Metallic", Range(0.0, 1.0)) = 0.0
        _Glossiness ("Glossiness", Range(0.0, 1.0)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        sampler2D _MainTex;
        float4 _UVScaleX;
        float4 _UVScaleY;
        float4 _UVScaleZ;
        float4 _UVOffsetX;
        float4 _UVOffsetY;
        float4 _UVOffsetZ;
        half _Metallic;
        half _Glossiness;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldNormal;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float2 uv = IN.uv_MainTex;

            // Определение грани куба по нормали и применение соответствующей UV-развертки и смещения
            if (abs(IN.worldNormal.x) > 0.5)
            {
                uv = uv * _UVScaleX.xy + _UVOffsetX.xy; // Грани с нормалью по оси X
            }
            else if (abs(IN.worldNormal.y) > 0.5)
            {
                uv = uv * _UVScaleY.xy + _UVOffsetY.xy; // Грани с нормалью по оси Y
            }
            else
            {
                uv = uv * _UVScaleZ.xy + _UVOffsetZ.xy; // Грани с нормалью по оси Z
            }

            fixed4 c = tex2D(_MainTex, uv);
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}