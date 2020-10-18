// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:Dissolve,iptp:0,cusa:False,bamd:0,cgin:,cpap:False,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:32781,y:32222,varname:node_4795,prsc:2|emission-2393-OUT,alpha-4520-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:32235,y:32602,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:63af7bc53f399b54e8995e30d2dfdde8,ntxv:0,isnm:False|UVIN-1476-OUT;n:type:ShaderForge.SFN_Multiply,id:2393,x:32495,y:32793,varname:node_2393,prsc:2|A-6074-RGB,B-2053-RGB,C-797-RGB,D-4373-OUT;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32235,y:32772,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:32235,y:32930,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0,c3:0.007668495,c4:1;n:type:ShaderForge.SFN_Append,id:5445,x:30367,y:32763,varname:node_5445,prsc:2|A-150-OUT,B-6676-OUT;n:type:ShaderForge.SFN_Time,id:8708,x:30367,y:32919,varname:node_8708,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:150,x:30018,y:32747,ptovrint:False,ptlb:U speed,ptin:_Uspeed,varname:node_150,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.7;n:type:ShaderForge.SFN_ValueProperty,id:6676,x:30018,y:32835,ptovrint:False,ptlb:V speed,ptin:_Vspeed,varname:node_6676,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:164,x:30554,y:32763,varname:node_164,prsc:2|A-5445-OUT,B-8708-T;n:type:ShaderForge.SFN_TexCoord,id:5205,x:30554,y:32909,varname:node_5205,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:6873,x:30747,y:32763,varname:node_6873,prsc:2|A-164-OUT,B-5205-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:4313,x:30935,y:32763,varname:node_4313,prsc:2,tex:652d6bdb13e519444a0db1537ff3652a,ntxv:0,isnm:False|UVIN-6873-OUT,TEX-8597-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:8597,x:30747,y:32947,ptovrint:False,ptlb:Noise,ptin:_Noise,varname:node_8597,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:652d6bdb13e519444a0db1537ff3652a,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Append,id:5853,x:30374,y:33120,varname:node_5853,prsc:2|A-3216-OUT,B-3599-OUT;n:type:ShaderForge.SFN_Time,id:5383,x:30374,y:33275,varname:node_5383,prsc:2;n:type:ShaderForge.SFN_Multiply,id:6829,x:30554,y:33120,varname:node_6829,prsc:2|A-5853-OUT,B-5383-T;n:type:ShaderForge.SFN_TexCoord,id:6308,x:30554,y:33265,varname:node_6308,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_ValueProperty,id:3216,x:29989,y:33123,ptovrint:False,ptlb:2U speed,ptin:_2Uspeed,varname:node_3216,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:-0.5;n:type:ShaderForge.SFN_ValueProperty,id:3599,x:29989,y:33277,ptovrint:False,ptlb:2V speed,ptin:_2Vspeed,varname:node_3599,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.51;n:type:ShaderForge.SFN_Add,id:1807,x:30747,y:33120,varname:node_1807,prsc:2|A-6829-OUT,B-6308-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:7138,x:30953,y:33144,varname:node_7138,prsc:2,tex:652d6bdb13e519444a0db1537ff3652a,ntxv:0,isnm:False|UVIN-1807-OUT,TEX-8597-TEX;n:type:ShaderForge.SFN_Slider,id:5286,x:30326,y:32608,ptovrint:False,ptlb:Dissolve,ptin:_Dissolve,varname:node_5286,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.2730698,max:1;n:type:ShaderForge.SFN_OneMinus,id:6551,x:30747,y:32589,varname:node_6551,prsc:2|IN-5286-OUT;n:type:ShaderForge.SFN_RemapRange,id:1238,x:30935,y:32581,varname:node_1238,prsc:2,frmn:0,frmx:1,tomn:-0.65,tomx:0.65|IN-6551-OUT;n:type:ShaderForge.SFN_Add,id:6851,x:31115,y:32581,varname:node_6851,prsc:2|A-1238-OUT,B-4313-R;n:type:ShaderForge.SFN_Add,id:8325,x:31115,y:32763,varname:node_8325,prsc:2|A-1238-OUT,B-7138-R;n:type:ShaderForge.SFN_Multiply,id:1178,x:31284,y:32581,varname:node_1178,prsc:2|A-6851-OUT,B-8325-OUT;n:type:ShaderForge.SFN_RemapRange,id:4342,x:31464,y:32581,varname:node_4342,prsc:2,frmn:0,frmx:1,tomn:-10,tomx:10|IN-1178-OUT;n:type:ShaderForge.SFN_Clamp01,id:4349,x:31665,y:32581,varname:node_4349,prsc:2|IN-4342-OUT;n:type:ShaderForge.SFN_OneMinus,id:2124,x:31853,y:32581,varname:node_2124,prsc:2|IN-4349-OUT;n:type:ShaderForge.SFN_Append,id:1476,x:32048,y:32601,varname:node_1476,prsc:2|A-2124-OUT,B-9834-OUT;n:type:ShaderForge.SFN_Vector1,id:9834,x:31853,y:32754,varname:node_9834,prsc:2,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:4373,x:32235,y:33109,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_4373,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:4520,x:32488,y:32501,varname:node_4520,prsc:2|A-2299-OUT,B-6074-R;n:type:ShaderForge.SFN_ValueProperty,id:2299,x:32235,y:32462,ptovrint:False,ptlb:Strench,ptin:_Strench,varname:node_2299,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.8;proporder:6074-797-150-6676-8597-5286-3216-3599-4373-2299;pass:END;sub:END;*/

Shader "Shader Forge/Electricity" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _TintColor ("Color", Color) = (1,0,0.007668495,1)
        _Uspeed ("U speed", Float ) = 0.7
        _Vspeed ("V speed", Float ) = 0.5
        _Noise ("Noise", 2D) = "white" {}
        _Dissolve ("Dissolve", Range(0, 1)) = 0.2730698
        _2Uspeed ("2U speed", Float ) = -0.5
        _2Vspeed ("2V speed", Float ) = 0.51
        _Opacity ("Opacity", Float ) = 1
        _Strench ("Strench", Float ) = 0.8
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float4, _TintColor)
                UNITY_DEFINE_INSTANCED_PROP( float, _Uspeed)
                UNITY_DEFINE_INSTANCED_PROP( float, _Vspeed)
                UNITY_DEFINE_INSTANCED_PROP( float, _2Uspeed)
                UNITY_DEFINE_INSTANCED_PROP( float, _2Vspeed)
                UNITY_DEFINE_INSTANCED_PROP( float, _Dissolve)
                UNITY_DEFINE_INSTANCED_PROP( float, _Opacity)
                UNITY_DEFINE_INSTANCED_PROP( float, _Strench)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
////// Lighting:
////// Emissive:
                float _Dissolve_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Dissolve );
                float node_1238 = ((1.0 - _Dissolve_var)*1.3+-0.65);
                float _Uspeed_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Uspeed );
                float _Vspeed_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Vspeed );
                float4 node_8708 = _Time;
                float2 node_6873 = ((float2(_Uspeed_var,_Vspeed_var)*node_8708.g)+i.uv0);
                float4 node_4313 = tex2D(_Noise,TRANSFORM_TEX(node_6873, _Noise));
                float _2Uspeed_var = UNITY_ACCESS_INSTANCED_PROP( Props, _2Uspeed );
                float _2Vspeed_var = UNITY_ACCESS_INSTANCED_PROP( Props, _2Vspeed );
                float4 node_5383 = _Time;
                float2 node_1807 = ((float2(_2Uspeed_var,_2Vspeed_var)*node_5383.g)+i.uv0);
                float4 node_7138 = tex2D(_Noise,TRANSFORM_TEX(node_1807, _Noise));
                float2 node_1476 = float2((1.0 - saturate((((node_1238+node_4313.r)*(node_1238+node_7138.r))*20.0+-10.0))),0.0);
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_1476, _MainTex));
                float4 _TintColor_var = UNITY_ACCESS_INSTANCED_PROP( Props, _TintColor );
                float _Opacity_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Opacity );
                float3 emissive = (_MainTex_var.rgb*i.vertexColor.rgb*_TintColor_var.rgb*_Opacity_var);
                float3 finalColor = emissive;
                float _Strench_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Strench );
                return fixed4(finalColor,(_Strench_var*_MainTex_var.r));
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
