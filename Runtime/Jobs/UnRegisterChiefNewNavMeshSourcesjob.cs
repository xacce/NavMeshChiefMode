using NavMeshDots.Runtime;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine.AI;

namespace NavMeshChiefMode.Runtime.Jobs
{
    [BurstCompile]
    [WithNone(typeof(NavMeshChiefRuntimeSource))]
    public partial struct UnRegisterChiefNewNavMeshSourcesjob : IJobEntity
    {
        public DynamicBuffer<NavMeshSourceElement> sources;
        public EntityCommandBuffer ecb;
        private int _index;
        public NativeArray<int> hasUpdates;

        [BurstCompile]
        public void Execute(NavMeshChiefSourceRegistered registered, Entity entity)
        {
            sources.RemoveAt(registered.index);
            ecb.RemoveComponent<NavMeshChiefSourceRegistered>(entity);
            hasUpdates[0]++;
        }
    }
}