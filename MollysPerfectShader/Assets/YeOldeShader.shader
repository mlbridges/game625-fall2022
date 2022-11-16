Shader "Unlit/YeOldeShader"
{
    //show values to edit in inspector
    Properties{
        //how big the checkerboard is
        _Scale("Pattern Size", Range(0,10)) = 1
        //what color the first checker shade is
        //remember: there is no vector 3, there are 4 inputs and the 4th one is always 1!
        _EvenColor("Color 1", Color) = (0,0,0,1)
        //what color the second shader shade is
        _OddColor("Color 2", Color) = (1,1,1,1)
        //trying to interpolate between a color and a picture
        //_MainTex ("Texture", 2D) = "white" {}
        //trying to see if we can make a third color 
        //_ThirdColor("Color3", Color) = (0,0,0,1)
    }
    SubShader
    {
        //the material is completely non-transparent and is rendered at the same time as the other opaque geometry
		Tags{ "RenderType"="Opaque" "Queue"="Geometry"}

		Pass{
			CGPROGRAM
			#include "UnityCG.cginc"

			#pragma vertex vert
			#pragma fragment frag

			float _Scale;

			float4 _EvenColor;
			float4 _OddColor;
            //sampler2D _MainTex;
            //float4 _MainTex_ST;
            //fixed4 _Color;
            //float4 _ThirdColor;

			struct appdata{
				float4 vertex : POSITION;
			};

			struct v2f{
				float4 position : SV_POSITION;
				float3 worldPos : TEXCOORD0;
			};

			v2f vert(appdata v){
				v2f o;
				//calculate the position in clip space to render the object
				o.position = UnityObjectToClipPos(v.vertex);
				//calculate the position of the vertex in the world
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);
				return o;
			}

            //everything before was setup - this is where we actually make the checkerboard 
            fixed4 frag (v2f i) : SV_Target
            {
                //step 1: converting the position of the object into numbers we can use
                //we accomplish this by taking the world position and scale
                float3 adjustedWorldPos = floor(i.worldPos / _Scale);
                //this part makes the checherboard: every dimension adds a layer of complexity
                //1d is stripes, 2d is boxes, 3d is cubes ig
                float stripes = adjustedWorldPos.x;
                //divide by 2 and get the fractional part (whether it ends in 0 or 0.5) for even and odd values 
                stripes = frac(stripes * 0.5);
                //multiply by 2 to make odd values brighter - idk why this works but it does
                stripes *= 2;

                //interpolate between color for even and odd fields depending on the frac from earlier
                float4 color = lerp(_EvenColor, _OddColor, stripes);
                return color;
            }
            ENDCG
        }
    }
    FallBack "Standard" //to create shadows
}
