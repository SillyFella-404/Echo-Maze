Shader "Custom/BrightGlowBloom"
{
    Properties
    {
        [HDR] _Color("Glow Color (HDR)", Color) = (1,1,1,1)
        _GlowStrength("Glow Strength", Range(0,10)) = 10
    }

        SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 100

        Pass
        {
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
                float4 vertex : SV_POSITION;
            };

            fixed4 _Color;
            float _GlowStrength;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Output HDR values for Bloom
                return _Color * _GlowStrength;
            }
            ENDCG
        }
    }
}
