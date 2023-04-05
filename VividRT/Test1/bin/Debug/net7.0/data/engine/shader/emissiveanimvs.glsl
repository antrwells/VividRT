#version 410
layout(location = 0) in vec3 vPos;
layout(location = 1) in vec3 vUv;
layout(location = 2) in vec3 vNorm;
layout(location = 3) in vec3 vBiNorm;
layout(location = 4) in vec3 vTan;
layout(location = 5) in vec4 in_BoneIds;
layout(location = 6) in vec4 in_Weights;



uniform mat4 mProj;
uniform mat4 mView;
uniform mat4 mModel;
// See kMaxBonesCount.
uniform mat4 bone_transforms[100];

out vec2 tuv;
out vec3 v_Position;
out mat3 v_TBN;

void main()
{
    mat4 S = mat4(0.f);
    for (int i = 0; i < 4; ++i)
    {
        if (in_BoneIds[i] >= 0)
        {
            S += (bone_transforms[int(in_BoneIds[i])] * in_Weights[i]);
        }
    }
    mat3 S_ = transpose(inverse(mat3(S)));
    mat4 MVP = mProj * mView * mModel;
    gl_Position = MVP * S * vec4(vPos, 1.f);
    tuv = vUv.xy;
    
  
}