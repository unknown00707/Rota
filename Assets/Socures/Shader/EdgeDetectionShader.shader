// 2025-08-02 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

Shader "Custom/EdgeDetectionOutline"
{
    Properties
    {
        _OutlineColor ("Outline Color", Color) = (1, 0, 0, 1) // 아웃라인 색상
        _Thickness ("Outline Thickness", Range(1, 10)) = 1 // 아웃라인 두께
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            Name "OutlinePass"
            Cull Off
            ZWrite Off
            ZTest Always

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float4 screenPos : TEXCOORD0;
            };

            float _Thickness;
            float4 _OutlineColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.screenPos = ComputeScreenPos(o.pos);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 현재 픽셀의 화면 좌표
                float2 screenUV = i.screenPos.xy / i.screenPos.w;

                // 주변 픽셀 샘플링
                float2 offset = _Thickness / _ScreenParams.xy;
                float depth = Linear01Depth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPos)));
                float depthLeft = Linear01Depth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPos + float4(-offset.x, 0, 0, 0))));
                float depthRight = Linear01Depth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPos + float4(offset.x, 0, 0, 0))));
                float depthUp = Linear01Depth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPos + float4(0, offset.y, 0, 0))));
                float depthDown = Linear01Depth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPos + float4(0, -offset.y, 0, 0))));

                // 깊이 차이를 기반으로 에지 감지
                float edge = step(0.01, abs(depth - depthLeft)) +
                             step(0.01, abs(depth - depthRight)) +
                             step(0.01, abs(depth - depthUp)) +
                             step(0.01, abs(depth - depthDown));

                // 에지가 감지되면 아웃라인 색상 반환
                return edge > 0 ? _OutlineColor : float4(0, 0, 0, 0);
            }
            ENDCG
        }
    }
}