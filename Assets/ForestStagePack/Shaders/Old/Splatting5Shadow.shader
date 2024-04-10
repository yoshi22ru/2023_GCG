// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.32 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.32;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:32719,y:32712,varname:node_4013,prsc:2|diff-1631-OUT;n:type:ShaderForge.SFN_Lerp,id:7994,x:31382,y:31930,varname:node_7994,prsc:2|A-31-RGB,B-8284-RGB,T-4787-OUT;n:type:ShaderForge.SFN_Tex2d,id:31,x:30929,y:31738,ptovrint:False,ptlb:DetailR,ptin:_DetailR,varname:node_7024,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:8284,x:30929,y:31952,ptovrint:False,ptlb:DetailG,ptin:_DetailG,varname:_node_7024_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:4575,x:31382,y:32153,ptovrint:False,ptlb:DetailB,ptin:_DetailB,varname:_node_7024_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:9640,x:31676,y:32503,ptovrint:False,ptlb:DetailA,ptin:_DetailA,varname:_node_7024_copy_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:5766,x:30683,y:32760,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:_node_7024_copy_copy_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Lerp,id:2876,x:31691,y:32292,varname:node_2876,prsc:2|A-7994-OUT,B-4575-RGB,T-8370-OUT;n:type:ShaderForge.SFN_Lerp,id:3422,x:31947,y:32478,varname:node_3422,prsc:2|A-2876-OUT,B-9640-RGB,T-2766-OUT;n:type:ShaderForge.SFN_ComponentMask,id:1776,x:30946,y:32798,varname:node_1776,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-5766-RGB;n:type:ShaderForge.SFN_ComponentMask,id:4787,x:30946,y:32955,varname:node_4787,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-5766-RGB;n:type:ShaderForge.SFN_ComponentMask,id:8370,x:30946,y:33109,varname:node_8370,prsc:2,cc1:2,cc2:-1,cc3:-1,cc4:-1|IN-5766-RGB;n:type:ShaderForge.SFN_OneMinus,id:2766,x:30946,y:33267,varname:node_2766,prsc:2|IN-5766-A;n:type:ShaderForge.SFN_Add,id:6577,x:31399,y:32947,varname:node_6577,prsc:2|A-1776-OUT,B-4787-OUT,C-8370-OUT;n:type:ShaderForge.SFN_OneMinus,id:1050,x:31728,y:32947,varname:node_1050,prsc:2|IN-349-OUT;n:type:ShaderForge.SFN_Tex2d,id:2644,x:31828,y:32720,ptovrint:False,ptlb:DetailBk,ptin:_DetailBk,varname:node_4734,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Lerp,id:3987,x:32164,y:32670,varname:node_3987,prsc:2|A-3422-OUT,B-2644-RGB,T-1050-OUT;n:type:ShaderForge.SFN_Clamp01,id:349,x:31566,y:32947,varname:node_349,prsc:2|IN-6577-OUT;n:type:ShaderForge.SFN_Power,id:1631,x:32403,y:32807,varname:node_1631,prsc:2|VAL-3987-OUT,EXP-1063-OUT;n:type:ShaderForge.SFN_Slider,id:6564,x:31945,y:33121,ptovrint:False,ptlb:Exp,ptin:_Exp,varname:node_831,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0,max:1;n:type:ShaderForge.SFN_OneMinus,id:1063,x:32294,y:33067,varname:node_1063,prsc:2|IN-6564-OUT;proporder:31-8284-4575-9640-5766-2644-6564;pass:END;sub:END;*/

Shader "Fatty/Splatting5S" {
    Properties {
        _DetailR ("DetailR", 2D) = "white" {}
        _DetailG ("DetailG", 2D) = "white" {}
        _DetailB ("DetailB", 2D) = "white" {}
        _DetailA ("DetailA", 2D) = "white" {}
        _Mask ("Mask", 2D) = "white" {}
        _DetailBk ("DetailBk", 2D) = "white" {}
        _Exp ("Exp", Range(-1, 1)) = 0
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            //#pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _DetailR; uniform float4 _DetailR_ST;
            uniform sampler2D _DetailG; uniform float4 _DetailG_ST;
            uniform sampler2D _DetailB; uniform float4 _DetailB_ST;
            uniform sampler2D _DetailA; uniform float4 _DetailA_ST;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            uniform sampler2D _DetailBk; uniform float4 _DetailBk_ST;
            uniform float _Exp;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 _DetailR_var = tex2D(_DetailR,TRANSFORM_TEX(i.uv0, _DetailR));
                float4 _DetailG_var = tex2D(_DetailG,TRANSFORM_TEX(i.uv0, _DetailG));
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(i.uv0, _Mask));
                float node_4787 = _Mask_var.rgb.g;
                float4 _DetailB_var = tex2D(_DetailB,TRANSFORM_TEX(i.uv0, _DetailB));
                float node_8370 = _Mask_var.rgb.b;
                float4 _DetailA_var = tex2D(_DetailA,TRANSFORM_TEX(i.uv0, _DetailA));
                float4 _DetailBk_var = tex2D(_DetailBk,TRANSFORM_TEX(i.uv0, _DetailBk));
                float3 diffuseColor = pow(lerp(lerp(lerp(lerp(_DetailR_var.rgb,_DetailG_var.rgb,node_4787),_DetailB_var.rgb,node_8370),_DetailA_var.rgb,(1.0 - _Mask_var.a)),_DetailBk_var.rgb,(1.0 - saturate((_Mask_var.rgb.r+node_4787+node_8370)))),(1.0 - _Exp));
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            //#pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _DetailR; uniform float4 _DetailR_ST;
            uniform sampler2D _DetailG; uniform float4 _DetailG_ST;
            uniform sampler2D _DetailB; uniform float4 _DetailB_ST;
            uniform sampler2D _DetailA; uniform float4 _DetailA_ST;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            uniform sampler2D _DetailBk; uniform float4 _DetailBk_ST;
            uniform float _Exp;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 _DetailR_var = tex2D(_DetailR,TRANSFORM_TEX(i.uv0, _DetailR));
                float4 _DetailG_var = tex2D(_DetailG,TRANSFORM_TEX(i.uv0, _DetailG));
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(i.uv0, _Mask));
                float node_4787 = _Mask_var.rgb.g;
                float4 _DetailB_var = tex2D(_DetailB,TRANSFORM_TEX(i.uv0, _DetailB));
                float node_8370 = _Mask_var.rgb.b;
                float4 _DetailA_var = tex2D(_DetailA,TRANSFORM_TEX(i.uv0, _DetailA));
                float4 _DetailBk_var = tex2D(_DetailBk,TRANSFORM_TEX(i.uv0, _DetailBk));
                float3 diffuseColor = pow(lerp(lerp(lerp(lerp(_DetailR_var.rgb,_DetailG_var.rgb,node_4787),_DetailB_var.rgb,node_8370),_DetailA_var.rgb,(1.0 - _Mask_var.a)),_DetailBk_var.rgb,(1.0 - saturate((_Mask_var.rgb.r+node_4787+node_8370)))),(1.0 - _Exp));
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    //CustomEditor "ShaderForgeMaterialInspector"
}
