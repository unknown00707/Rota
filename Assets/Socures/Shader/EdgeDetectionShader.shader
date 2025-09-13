// 2025-08-02 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

Shader "Custom/FixedLoopShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 offsets[4] = {
                    float2(-0.01, 0), float2(0.01, 0),
                    float2(0, -0.01), float2(0, 0.01)
                };

                // Precompute gradient-dependent value
                float4 baseColor = tex2D(_MainTex, i.uv);

                // Fixed iteration loop
                float4 result = 0;
                for (int j = 0; j < 4; j++)
                {
                    result += baseColor * 0.25; // Example operation
                }

                return result;
            }
            ENDCG
        }
    }
}