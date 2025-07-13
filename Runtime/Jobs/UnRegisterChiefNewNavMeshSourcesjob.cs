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
            for (int i = 0; i < sources.Length; i++)
            {
                var source = sources[i];
                if (source.binded.Equals(entity))
                {
                    sources.RemoveAt(i);
                    break;
                }
            }

            ecb.RemoveComponent<NavMeshChiefSourceRegistered>(entity);
            hasUpdates[0]++;
        }
    }
}