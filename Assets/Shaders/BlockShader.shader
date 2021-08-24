Shader "Custom/BlockShader"
{
    Properties
    {
        _Up ("Up", 2D) = "white" {}
        _Down ("Down", 2D) = "white" {}
        _Front ("Front", 2D) = "white" {}
        _Back ("Back", 2D) = "white" {}
        _Left ("Left", 2D) = "white" {}
        _Right ("Right", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.5

        sampler2D _Up, _Down, _Front, _Back, _Left, _Right;

        struct Input
        {
            float2 uv_Up, uv_Down, uv_Front, uv_Back, uv_Left, uv_Right, uv_Default;
            float3 worldNormal;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D(_Up, IN.uv_Up);
            float3 up = float3(0.0f, 1.0f, 0.0f), down = -up;
            float3 front = float3(0.0f, 0.0f, 1.0f), back = -front;
            float3 left = float3(1.0f, 0.0f, 0.0f), right = -left;

            if(dot(IN.worldNormal, up) > 0)
                c = tex2D(_Up, IN.uv_Up);
            else if(dot(IN.worldNormal, down) > 0)
                c = tex2D(_Down, IN.uv_Down);
            else if(dot(IN.worldNormal, front) > 0)
                c = tex2D(_Front, IN.uv_Front);
            else if(dot(IN.worldNormal, back) > 0)
                c = tex2D(_Back, IN.uv_Back);
            else if(dot(IN.worldNormal, left) > 0)
                c = tex2D(_Left, IN.uv_Left);
            else if(dot(IN.worldNormal, right) > 0)
                c = tex2D(_Right, IN.uv_Right);

            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
