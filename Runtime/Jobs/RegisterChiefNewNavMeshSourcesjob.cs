using NavMeshDots.Runtime;
using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine.AI;

namespace NavMeshChiefMode.Runtime.Jobs
{
    [BurstCompile]
    [WithNone(typeof(NavMeshChiefSourceRegistered))]
    public partial struct RegisterChiefNewNavMeshSourcesjob : IJobEntity
    {
        public DynamicBuffer<NavMeshSourceElement> sources;
        public EntityCommandBuffer ecb;
        public NativeArray<int> hasUpdates;

        [BurstCompile]
        public void Execute(NavMeshChiefRuntimeSource source, in LocalToWorld ltw, Entity entity)
        {
            ecb.AddComponent(entity, new NavMeshChiefSourceRegistered() { });
            NavMeshBuildSource dyn;
            if (source.dynamic)
            {
                dyn = source.source;
                dyn.transform = math.mul(ltw.Value, dyn.transform);
            }
            else
            {
                dyn = source.source;
            }

            sources.Add(new NavMeshSourceElement() { primitive = dyn, binded = entity });
            hasUpdates[0]++;
        }
    }
}