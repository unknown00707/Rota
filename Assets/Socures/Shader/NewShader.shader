// 2025-08-02 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

Shader "Custom/Outline"
{
    Properties
    {
        _Color ("Outline Color", Color) = (1,1,0,1)
        _Thickness ("Outline Thickness", Range(0.0, 0.1)) = 0.02
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            Name "Outline"
            Cull Front
            ZWrite On
            ZTest LEqual

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
                float4 pos : SV_POSITION;
                float4 color : COLOR;
            };

            float _Thickness;
            float4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                float3 norm = normalize(v.normal);
                o.pos = UnityObjectToClipPos(v.vertex + norm * _Thickness);
                o.color = _Color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return i.color;
            }
            ENDCG
        }
    }
}