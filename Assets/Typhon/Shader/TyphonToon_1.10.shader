Shader "Typhon/TyphonToon 1.10"
{
    Properties
    {
        [Header(Basic Textures)]
        _MainTex ("Texture", 2D) = "white" {}
        _BaseColor("Color", Color) = (1,1,1)
        [Normal]_BumpMap    ("Normal Map"  , 2D         ) = "bump" {}        
        _OcclusionMap ("Occlusion Map", 2D) = "white" {}
        _EmissionMap ("Emission Map", 2D) = "black" {}
        [HDR] _EmissionColor ("Emission Color", Color) = (0,0,0)
        
        [Header(Shading Config)]
        _Ramp ("Ramp", 2D) = "white" {}
        _DiffuseMask("Diffuse Mask", 2D) = "white" {}
        _DiffBoost("Diffuse Magnification", Range(.0, 5)) = 1
        _AmbGain("AmbientLight Gain", Color) = (0,0,0,1)
        _AmbBoost("AmbientLight Magnification", Range(.0, 5)) = 1

        [Header(Rim Light Config)]
        [Toggle(RIMLIGHT_ON)]_rimFlag ("Use Rim Light", int) = 0
        _RmlightIntensity("Rim Light Intensity", Range(0, 5)) = 0.5
        [PowerSlider(5)]_RimlightThickness("Rim Light Thickness", Range(0, 5)) = 0.2
        _RimlightMap("Rim Light Color", 2D) = "white" {}
        [Toggle(RIMSHADOW_ON)]_rimshadowFlag ("Rim Shadow", int) = 0
        [PowerSlider(3)]_RimShadowIntensity("Rim Shadow Intensity", Range(0, 5)) = 1
        [PowerSlider(5)]_RimshadowThickness("Rim Shadow Thickness", Range(0, 5)) = 1
        [Toggle]_rimshadowAmbient ("Affects AmbientLight", int) = 0
        [Toggle]_rimshadowRamp ("Use Ramp", int) = 0


        [Header(Outline Config)]
        _OutlineMap("Outline Map(r=outer, g=inner)", 2D) = "white" {}
        _OutlineColor("Outline Color", Color) = (0,0,0,1)
		_Outline("Outline Width", Range(0, 0.1)) = .002
        [PowerSlider(5)]_MaxBoldOutlineDistance("Max Outline Width Distance", Range(0, 1000)) = 1

        [Header(Highlight And Reflection Config)]
        [Toggle(SPECULAR_ON)]_SpecularFlag ("Use Specular Highlight", int) = 0
        [Toggle(REFLECTION_ON)]_Flag ("Use Reflection", int) = 0
        _MetallicMap ("Metallic Map", 2D) = "white" {}
        _Metallic("Metallic", Range(0, 1)) = 0
        _SmoothnessMap ("Smoothness Map", 2D) = "white" {}
        _Smoothness("Smoothness", Range(0, 1)) = 0.5
        _MinimumRefrection("Rim Refrection", Range(0, 1)) = .0015
        _SpecularMap("Specular Map", 2D) = "white" {}
        [HDR] _SpecularColor ("Specular Color", Color) = (1,1,1)
        
        [Header(Shadow Config)]
        [Toggle(SHADOW_ADJUST_ON)]_ShadowAdjustment ("Shadow Adjustment", int) = 0
        [Toggle(SHADOW_RAMP_ON)]_ShadowRamped ("Shadow with RampMap", int) = 0
        _ShadowThreshold("Shadow Adjustment Threshold", Range(.0, 1)) = 0.5

        [Header(Omake 1)]
        [Toggle(FillBlack_ON)]_BlackFill_Flag ("Use Solid Black Area", int) = 0
        _BlackMix("Color Mix", Range(0, 1)) = 0
        _BlackThreshold("Threshold", Range(-1, 1)) = 0
        _BlackSmooth("Smoothness", Range(0, 1)) = 0.05
        [Toggle(FillWhite_ON)]_WhiteFill_Flag ("Use Solid White Area", int) = 0
        _WhiteMix("Color Mix", Range(0, 1)) = 0
        _WhiteThreshold("Threshold", Range(-1, 1)) = 0.5
        _WhiteSmooth("Smoothness", Range(0, 1)) = 0.05

        [Header(Omake 2)]
        [Toggle(DITHER_ON)]_Dither ("Use Dither Shade", int) = 0
        _MatrixWidth("Dither Matrix Width/Height", int) = 4
		_MatrixTex("Dither Matrix", 2D) = "black" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass {
            Cull Front

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
            #pragma target 3.0
            #include "UnityCG.cginc"

            static const float ipFov = atan(1.0f / unity_CameraProjection._m11 ) * 6 / UNITY_PI;
            static const float3 scale = float3(length(unity_ObjectToWorld._m00_m10_m20),length(unity_ObjectToWorld._m01_m11_m21),length(unity_ObjectToWorld._m02_m12_m22));

            float _Outline;
            float _MaxBoldOutlineDistance;
            fixed4 _OutlineColor;
            sampler2D _OutlineMap;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                UNITY_FOG_COORDS(1)
            };

            v2f vert (appdata v)
            {
                v2f o;
                
                //カメラとの距離でアウトラインの幅変更
                float distance = -UnityObjectToViewPos(v.vertex).z;
                distance = min(_MaxBoldOutlineDistance,distance);

                //テクスチャによって太さを変える外側なのでR
                distance *= tex2Dlod(_OutlineMap, float4(v.texcoord.xy,0,0)).r * ipFov;
                v.vertex += float4(v.normal * _Outline * distance / scale, 0);
                
                
                o.vertex = UnityObjectToClipPos(v.vertex); 

                UNITY_TRANSFER_FOG(o, o.vertex);

                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {           
                fixed4 col = _OutlineColor;
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // ForwardBase
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        Pass
        {
            Tags { "LightMode"="ForwardBase" }
            Cull Back
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog //FOG
            #pragma shader_feature _ DITHER_ON //Dither
            #pragma shader_feature _ RIMLIGHT_ON //RIMLIGHT
            #pragma shader_feature _ RIMSHADOW_ON //RIMSHADOW
            #pragma shader_feature _ SHADOW_ADJUST_ON //Shadow
            #pragma shader_feature _ SHADOW_RAMP_ON //Shadow
            #pragma shader_feature _ SPECULAR_ON //Specular
            #pragma shader_feature _ REFLECTION_ON //Reflection
            #pragma shader_feature _ FillBlack_ON //FillBlack
            #pragma shader_feature _ FillWhite_ON //FillBlack
             
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"

            static const float ipFov = atan(1.0f / unity_CameraProjection._m11 ) * 6 / UNITY_PI;
            static const float3 scale = float3(length(unity_ObjectToWorld._m00_m10_m20),length(unity_ObjectToWorld._m01_m11_m21),length(unity_ObjectToWorld._m02_m12_m22));

            struct appdata
            {
                float4 vertex : POSITION;
                half3 normal : NORMAL;
                half2 texcoord : TEXCOORD0;
                float4 tangent : TANGENT;
            };
            
            struct v2f
            {
                float4 pos : SV_POSITION;
                half2 uv : TEXCOORD0;
                half3 normal: TEXCOORD1;
                half3 ambient: TEXCOORD2;
                float3 worldPos: TEXCOORD3;
                LIGHTING_COORDS(4, 5)
                //法線マップ用
                half3 tspace0 : TEXCOORD6; // tangent.x, bitangent.x, normal.x
                half3 tspace1 : TEXCOORD7; // tangent.y, bitangent.y, normal.y
                half3 tspace2 : TEXCOORD8; // tangent.z, bitangent.z, normal.z
                
                UNITY_FOG_COORDS(9)//FOG

                #ifdef DITHER_ON //Dither
                    float4 screenPos : TEXCOORD10;
                #endif
            };

            float _Outline;
            float _MaxBoldOutlineDistance;
            float _AmbBoost;
            fixed4 _AmbGain;
            float _DiffBoost;
            sampler2D _MainTex;
            half4 _BaseColor;
            sampler2D _OcclusionMap;
            sampler2D _Ramp;
            sampler2D _BumpMap;
            sampler2D _OutlineMap;
            half4 _MainTex_ST;
            half4 _LightColor0;
            sampler2D _EmissionMap;
            half4 _EmissionColor;

            float3 boxProjection(float3 normalizedDir, float3 worldPosition, float4 probePosition, float3 boxMin, float3 boxMax)
            {
               #if UNITY_SPECCUBE_BOX_PROJECTION
                    if (probePosition.w > 0) {
                        float3 magnitudes = ((normalizedDir > 0 ? boxMax : boxMin) - worldPosition) / normalizedDir;
                        float magnitude = min(min(magnitudes.x, magnitudes.y), magnitudes.z);
                        normalizedDir = normalizedDir* magnitude + (worldPosition - probePosition);
                    }
               #endif

                return normalizedDir;
            }
            
            v2f vert (appdata v)
            {
                v2f o = (v2f)0;
                
                float distance = -UnityObjectToViewPos(v.vertex).z;
                distance = min(_MaxBoldOutlineDistance,distance);

                //テクスチャによって太さを変える内側なのでG
                distance *= tex2Dlod(_OutlineMap, float4(v.texcoord.xy,0,0)).g * ipFov;
                v.vertex -= float4(v.normal * _Outline * distance / scale, 0);
                

                o.pos = UnityObjectToClipPos(v.vertex);

                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);

                half3 wTangent = UnityObjectToWorldDir(v.tangent.xyz);
                half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
                half3 wBitangent = cross(o.normal, wTangent) * tangentSign;

                o.tspace0 = half3(wTangent.x, wBitangent.x, o.normal.x);
                o.tspace1 = half3(wTangent.y, wBitangent.y, o.normal.y);
                o.tspace2 = half3(wTangent.z, wBitangent.z, o.normal.z);
            
                #if UNITY_SHOULD_SAMPLE_SH
             
                    #if defined(VERTEXLIGHT_ON)
             
                         float4 toLightX = unity_4LightPosX0 - o.worldPos.x;
                         float4 toLightY = unity_4LightPosY0 - o.worldPos.y;
                         float4 toLightZ = unity_4LightPosZ0 - o.worldPos.z;

                         float4 lengthSq = 0;
                         lengthSq += toLightX * toLightX;
                         lengthSq += toLightY * toLightY;
                         lengthSq += toLightZ * toLightZ;
                         lengthSq = max(lengthSq, 0.000001);

                         float4 ndotl = 0;
                         ndotl += toLightX * o.normal.x;
                         ndotl += toLightY * o.normal.y;
                         ndotl += toLightZ * o.normal.z;

                         float4 corr = rsqrt(lengthSq);
                         ndotl = max(float4(0,0,0,0), ndotl * corr);

                         float4 atten = 1.0 / (1.0 + lengthSq * unity_4LightAtten0);
                         float4 diff = ndotl * atten * _DiffBoost ;
                         float3 col = 0;
                         col += unity_LightColor[0].rgb * diff.x;
                         col += unity_LightColor[1].rgb * diff.y;
                         col += unity_LightColor[2].rgb * diff.z;
                         col += unity_LightColor[3].rgb * diff.w;

                         o.ambient = col;
             
                    #endif
             
                     o.ambient += max(0, (ShadeSH9(float4(o.normal, 1))+_AmbGain)*_AmbBoost);
                #else
             
                 o.ambient = 0;
             
                #endif
                 
                TRANSFER_SHADOW(o)
                UNITY_TRANSFER_FOG(o, o.pos);
 
                #ifdef DITHER_ON //Dither
                    o.screenPos = ComputeScreenPos(UnityObjectToClipPos (v.vertex));
                #endif

                return o;
            }


            sampler2D _DiffuseMask;

            #ifdef DITHER_ON //Dither
                sampler2D _MatrixTex;
                int _MatrixWidth;
            #endif

            #ifdef RIMSHADOW_ON //Rim Shadow
                half _RimshadowThickness;
                half _RimShadowIntensity;
                int _rimshadowAmbient;
                int _rimshadowRamp;
            #endif

            #ifdef RIMLIGHT_ON //Rim Light
                sampler2D _RimlightMap;
                half _RmlightIntensity;
                half _RimlightThickness;
            #endif

            #ifdef SHADOW_ADJUST_ON
                half _ShadowThreshold;
            #endif
            
            float _MinimumRefrection;
            sampler2D _SpecularMap;
            sampler2D _MetallicMap;
            sampler2D _SmoothnessMap;
            half3 _SpecularColor;
            half _Smoothness;
            half _Metallic;
            float _BlackMix;
            float _BlackThreshold;
            float _BlackSmooth;
            float _WhiteMix;
            float _WhiteThreshold;
            float _WhiteSmooth;
            
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // ForwardBase Fragment
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
            fixed4 frag (v2f i) : SV_Target
            {
                half4 col = tex2D(_MainTex, i.uv) * _BaseColor;
                half smoothness = saturate(_Smoothness * tex2D(_SmoothnessMap, i.uv).r);
                half metallic = saturate(_Metallic * tex2D(_MetallicMap, i.uv).r);
                half3 viewDir = normalize(_WorldSpaceCameraPos -i.worldPos.xyz);
                half3 tnormal = UnpackNormal(tex2D(_BumpMap, i.uv));
                half3 worldNormal;
                worldNormal.x = dot(i.tspace0, tnormal);
                worldNormal.y = dot(i.tspace1, tnormal);
                worldNormal.z = dot(i.tspace2, tnormal);
                worldNormal = normalize(worldNormal);
                half3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                if(_LightColor0.w == 0){//ディレクショナルライトがないときとりあえず適当な方向に設定
                    lightDir = normalize(half3(1,0.5,0));
                }

                //リフレクション
                #ifdef REFLECTION_ON
                    half3 reflDir = reflect(-viewDir, worldNormal);

                    half3 reflDir0 = boxProjection(reflDir, i.worldPos, unity_SpecCube0_ProbePosition, unity_SpecCube0_BoxMin, unity_SpecCube0_BoxMax);
                    half3 reflDir1 = boxProjection(reflDir, i.worldPos, unity_SpecCube1_ProbePosition, unity_SpecCube1_BoxMin, unity_SpecCube1_BoxMax);
                    half roughness = (1-smoothness)*8;
                    half4 refColor0 = UNITY_SAMPLE_TEXCUBE_LOD(unity_SpecCube0, reflDir0, roughness);
                    refColor0.rgb = DecodeHDR(refColor0, unity_SpecCube0_HDR);
                    // SpecCube1のサンプラはSpecCube0のものを使う
                    half4 refColor1 = UNITY_SAMPLE_TEXCUBE_SAMPLER_LOD(unity_SpecCube1, unity_SpecCube0, reflDir1, roughness);
                    refColor1.rgb = DecodeHDR(refColor1, unity_SpecCube1_HDR);

                    half3 reflection = lerp(refColor1, refColor0, unity_SpecCube0_BoxMin.w);
                #endif
                //リフレクションここまで



                //影と距離減衰による光の強さ（影）ForwardBaseはディレクショナルライトのはずなので距離減衰はない…はず
                float attenuation = SHADOW_ATTENUATION(i);
                attenuation *= tex2D(_DiffuseMask, i.uv);

                

                #ifdef SHADOW_ADJUST_ON
                    attenuation = smoothstep(0,_ShadowThreshold,attenuation);
                #endif

                #ifdef SHADOW_RAMP_ON
                    attenuation = tex2D(_Ramp, float2(attenuation,0.5));
                #endif
                
                //光源方向と法線による光の当たり具合（陰）
                float nl = (dot(worldNormal, lightDir)+1)/2;
                float attenuation2 = min(nl,attenuation);
                nl = tex2D(_Ramp, float2(nl,0.5));

                

                //陰と影を重ねる
                attenuation = min(nl,attenuation);

                //スペキュラ、黒ベタの計算
                #if defined(SPECULAR_ON) || defined(FillBlack_ON) || defined(FillWhite_ON)
                    #if defined(SPECULAR_ON)
                        half shininess = lerp(1,50,smoothness);
                        float3 specular = attenuation2 * _LightColor0.rgb * lerp(tex2D(_SpecularMap, i.uv),tex2D(_MainTex, i.uv),metallic) * pow( saturate( dot(reflect(-lightDir, worldNormal), viewDir)), shininess) * smoothness;
                    #endif

                    #if defined(FillBlack_ON)
                        float3 blackPower = smoothstep(_BlackThreshold-_BlackSmooth,_BlackThreshold+_BlackSmooth,dot(reflect(-lightDir, worldNormal), viewDir));
                    #endif

                    #if defined(FillWhite_ON)
                        float3 whitePower = smoothstep(_WhiteThreshold-_WhiteSmooth,_WhiteThreshold+_WhiteSmooth,max(0, dot(normalize(lightDir-viewDir), worldNormal)));
                    #endif
                #endif

                //陰影のディザリング
                #ifdef DITHER_ON
                    float2 uv_MatrixTex = i.screenPos.xy / i.screenPos.w * _ScreenParams.xy / _MatrixWidth;
			        float threshold = tex2D(_MatrixTex, uv_MatrixTex).r;
			        attenuation = ceil(max(0,attenuation - threshold));
                #endif

                //リムシャドウ追加(WIP)
                #ifdef RIMSHADOW_ON
                    half rims = 1 - saturate(dot(viewDir, worldNormal));
                    rims = 1 - saturate(_RimShadowIntensity*pow(rims, 1/_RimshadowThickness));
                    if(_rimshadowRamp){
                        rims = tex2D(_Ramp, float2(rims,0.5));
                    }
                    attenuation *= rims;
                    if(_rimshadowAmbient){
                        i.ambient *= rims;
                    }
                #endif

                //ライトの色の追加
                half3 diff = _LightColor0.rgb * attenuation * _DiffBoost;
                half occlusion = tex2D(_OcclusionMap, i.uv);
                half3 ambient = i.ambient * occlusion;
                col.rgb *= lerp(diff + ambient,1,metallic);

                //リフレクション追加
                #ifdef REFLECTION_ON
                    reflection *= occlusion;
                    col.rgb = lerp(col.rgb,reflection * col,metallic);//リフレクション
                    //フレネル反射もどき追加
                    half fresnel = 1 - saturate(dot(viewDir, worldNormal));
                    fresnel = lerp(0,saturate(smoothness+_MinimumRefrection),pow(fresnel, smoothness*5));
                    col.rgb = lerp(col.rgb,reflection,fresnel);

				#endif

                //リムライト追加
                #ifdef RIMLIGHT_ON
                    half3 rimcolor = tex2D(_RimlightMap, i.uv) * (diff + ambient);
                    half rim = 1 - saturate(dot(viewDir, worldNormal));
                    col.rgb += rimcolor * _RmlightIntensity * pow(rim, 1/_RimlightThickness);
                #endif

                //黒ベタ/ホワイト追加
                #if defined(FillBlack_ON) || defined(FillWhite_ON)
                    //if(_LightColor0.w != 0){
                        #if defined(FillBlack_ON)
                            blackPower = saturate(blackPower + _BlackMix);
                            col.rgb *= blackPower;
                        #endif
                        #if defined(FillWhite_ON)
                            whitePower = saturate(whitePower - _WhiteMix);
                            col.rgb += whitePower;
                        #endif
                    //}
                #endif
                
                col += tex2D(_EmissionMap, i.uv) * _EmissionColor;//Emission追加

                //Specular追加
                #ifdef SPECULAR_ON
                    col.rgb += specular* _SpecularColor;
                #endif

                UNITY_APPLY_FOG(i.fogCoord, col);//FOG
                return col;
            }
             ENDCG
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // ForwardAdd
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        Pass {

            Tags { "LightMode"="ForwardAdd" }

            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog //FOG
            #pragma shader_feature _ SPECULAR_ON //Specular
            #pragma shader_feature _ DITHER_ON //Dither
         
            #include "UnityCG.cginc"
            #include "AutoLight.cginc" 

            static const float ipFov = atan(1.0f / unity_CameraProjection._m11 ) * 6 / UNITY_PI;
            static const float3 scale = float3(length(unity_ObjectToWorld._m00_m10_m20),length(unity_ObjectToWorld._m01_m11_m21),length(unity_ObjectToWorld._m02_m12_m22));
         
            struct appdata 
            {
                float4 vertex : POSITION;
                half3 normal : NORMAL;
                half2 texcoord : TEXCOORD0;
                float4 tangent : TANGENT;
            };
            
            struct v2f
            {
                float4 pos : SV_POSITION;
                half2 uv : TEXCOORD0;
                half3 normal: TEXCOORD1;
                half3 ambient: TEXCOORD2;
                float3 worldPos: TEXCOORD3;
                LIGHTING_COORDS(4, 5)
                UNITY_FOG_COORDS(6)//FOG
                half3 tspace0 : TEXCOORD7; // tangent.x, bitangent.x, normal.x
                half3 tspace1 : TEXCOORD8; // tangent.y, bitangent.y, normal.y
                half3 tspace2 : TEXCOORD9; // tangent.z, bitangent.z, normal.z
                #if defined(DITHER_ON)
                    float4 screenPos : TEXCOORD10;
                #endif
            };
            
            float _Outline;
            float _MaxBoldOutlineDistance;
            float _DiffBoost;
            sampler2D _MainTex;
            half4 _BaseColor;
            sampler2D _Ramp;
            sampler2D _OutlineMap;
            sampler2D _BumpMap;
            half4 _MainTex_ST;
            half4 _LightColor0;
         
            v2f vert (appdata v) 
            {
                v2f o = (v2f)0;
                //アウトライン用の頂点位置調整
                float distance = -UnityObjectToViewPos(v.vertex).z;
                distance = min(_MaxBoldOutlineDistance,distance);
                distance *= tex2Dlod(_OutlineMap, float4(v.texcoord.xy,0,0)).g * ipFov;
                v.vertex -= float4(v.normal * _Outline * distance / scale, 0); 
                //基本
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                //ノーマル用
                half3 wTangent = UnityObjectToWorldDir(v.tangent.xyz);
                half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
                half3 wBitangent = cross(o.normal, wTangent) * tangentSign;
                o.tspace0 = half3(wTangent.x, wBitangent.x, o.normal.x);
                o.tspace1 = half3(wTangent.y, wBitangent.y, o.normal.y);
                o.tspace2 = half3(wTangent.z, wBitangent.z, o.normal.z);
                
                TRANSFER_SHADOW(o)
                UNITY_TRANSFER_FOG(o, o.pos); //FOG
         
                #ifdef DITHER_ON //Dither
                    o.screenPos = ComputeScreenPos(UnityObjectToClipPos (v.vertex));
                #endif

                return o;
            }

            sampler2D _DiffuseMask;

            #ifdef SHADOW_ADJUST_ON
                half _ShadowThreshold;
            #endif

            #ifdef DITHER_ON //Dither
                sampler2D _MatrixTex;
                int _MatrixWidth;
            #endif

            half _SpecularPower;
            sampler2D _SpecularMap;
            half3 _SpecularColor;
            sampler2D _MetallicMap;
            sampler2D _SmoothnessMap;
            half _Metallic;
            half _Smoothness;


            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
            void frag (in v2f i, out fixed4 col : SV_Target){

                float3 baseColor = tex2D(_MainTex, i.uv)* _BaseColor;
                half smoothness = saturate(_Smoothness * tex2D(_SmoothnessMap, i.uv).r);
                half metallic = saturate(_Metallic * tex2D(_MetallicMap, i.uv).r);
                
                //ノーマル用
                half3 tnormal = UnpackNormal(tex2D(_BumpMap, i.uv));
                half3 worldNormal;
                worldNormal.x = dot(i.tspace0, tnormal);
                worldNormal.y = dot(i.tspace1, tnormal);
                worldNormal.z = dot(i.tspace2, tnormal);
                worldNormal = normalize(worldNormal);

                #ifndef USING_DIRECTIONAL_LIGHT
                    fixed3 lightDir = normalize(UnityWorldSpaceLightDir(i.worldPos));
                #else
                    fixed3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                #endif
                
                //影と距離減衰による光の強さ（影）
                UNITY_LIGHT_ATTENUATION(attenuation, i, i.worldPos);

                attenuation *= tex2D(_DiffuseMask, i.uv);

                #ifdef SHADOW_ADJUST_ON
                    //attenuation = smoothstep(0,_ShadowThreshold,attenuation);
                #endif

                //光源方向と法線による光の当たり具合（陰）
                float nl = (dot(worldNormal, lightDir)+1)/2;
                half attenuation2 = nl*attenuation;
                nl = tex2D (_Ramp, float2(nl,0));

                //陰と影を重ねる
                attenuation = nl*attenuation;


                #ifdef DITHER_ON //ディザリング
                    float2 uv_MatrixTex = i.screenPos.xy / i.screenPos.w * _ScreenParams.xy / _MatrixWidth;
			        float threshold = tex2D(_MatrixTex, uv_MatrixTex).r;
			        attenuation = ceil(max(0,attenuation - threshold));
                #endif

                col = lerp(fixed4(baseColor * _LightColor0.rgb * attenuation * _DiffBoost, 0),0,metallic);

                //スペキュラの計算
                #if defined(SPECULAR_ON)
                    half shininess = lerp(1,50,smoothness);
                    float3 viewDir = normalize(_WorldSpaceCameraPos -i.worldPos.xyz);
                    float3 specular = attenuation2 * _LightColor0.rgb * lerp(tex2D(_SpecularMap, i.uv),tex2D(_MainTex, i.uv),metallic) * pow( saturate( dot(reflect(-lightDir, worldNormal), viewDir)), shininess) * smoothness;
                    col.rgb += specular * tex2D(_SpecularMap, i.uv)* _SpecularColor;
                #endif

                UNITY_APPLY_FOG(i.fogCoord, col);//FOG
            }
            ENDCG
        }

        Pass {
            Name "ShadowCaster"
            Tags { "LightMode" = "ShadowCaster" }

            ZWrite On ZTest LEqual

            CGPROGRAM
            #pragma target 3.0

            #pragma vertex vertShadowCaster
            #pragma fragment fragShadowCaster


            #include "UnityStandardShadow.cginc"


            ENDCG

        }
    }
    FallBack "Legacy Shaders/VertexLit"
}