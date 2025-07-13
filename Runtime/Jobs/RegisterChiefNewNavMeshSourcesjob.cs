using NavMeshDots.Runtime;
using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using Unity.Entities;
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
        public void Execute(NavMeshChiefRuntimeSource source, Entity entity)
        {
            ecb.AddComponent(entity, new NavMeshChiefSourceRegistered() { });
            sources.Add(new NavMeshSourceElement() { primitive = source.source, binded = entity });
            hasUpdates[0]++;
        }

      
    }
}