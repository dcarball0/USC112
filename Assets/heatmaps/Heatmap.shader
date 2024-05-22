// Upgrade NOTE: commented out 'float4x4 _Object2World', a built-in variable

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Alan Zucconi
// www.alanzucconi.com
Shader "Hidden/Heatmap" {
    Properties {
        _HeatTex("Texture", 2D) = "white" {}
    }
    SubShader {
        Tags { "Queue" = "Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha // Alpha blend

        Pass {
            CGPROGRAM
            #pragma vertex vert             
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct vertInput {
                float4 pos : POSITION;
            };

            struct vertOutput {
                float4 pos : POSITION;
                float3 worldPos : TEXCOORD1;
            };

            // uniform float4x4 _Object2World; // matrix to convert object to world coordinates

            vertOutput vert(vertInput input) {
                vertOutput o;
                o.pos = UnityObjectToClipPos(input.pos);
                o.worldPos = mul(unity_ObjectToWorld, input.pos).xyz;
                return o;
            }

            uniform int _Points_Length;
            uniform float4 _Points[100];        // (x, y, z) = position
            uniform float4 _Properties[100];    // x = radius, y = intensity

            sampler2D _HeatTex;

            half4 frag(vertOutput output) : COLOR {
                // Get the scale of the object
                float3 scale = float3(
                    length(unity_ObjectToWorld._m00_m01_m02),
                    length(unity_ObjectToWorld._m10_m11_m12),
                    length(unity_ObjectToWorld._m20_m21_m22)
                );

                // Loops over all the points
                half h = 0;
                for (int i = 0; i < _Points_Length; i++) {
                    // Scale the point position
                    float3 scaledPoint = _Points[i].xyz * scale;

                    // Calculates the contribution of each point
                    half di = distance(output.worldPos, scaledPoint);

                    // Scale the radius
                    half ri = _Properties[i].x * scale.x;
                    half hi = 1 - saturate(di / ri);

                    h += hi * _Properties[i].y;
                }

                // Converts (0-1) according to the heat texture
                h = saturate(h);
                half4 color = tex2D(_HeatTex, fixed2(h, 0.5));
                return color;
            }
            ENDCG
        }
    }
    Fallback "Diffuse"
}
