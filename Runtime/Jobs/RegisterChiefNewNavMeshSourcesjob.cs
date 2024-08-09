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
    public partial struct RegisterChiefNewNavMeshSourcesjob : IJobEntity, IJobEntityChunkBeginEnd
    {
        public DynamicBuffer<NavMeshSourceElement> sources;
        public EntityCommandBuffer ecb;
        private int _index;
        public NativeArray<int> hasUpdates;

        [BurstCompile]
        public void Execute(NavMeshChiefRuntimeSource source, Entity entity)
        {
            ecb.AddComponent(entity, new NavMeshChiefSourceRegistered() { index = sources.Length });
            sources.Add(new NavMeshSourceElement() { primitive = source.source });
            hasUpdates[0]++;
        }

        public bool OnChunkBegin(in ArchetypeChunk chunk, int unfilteredChunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
        {
            _index = unfilteredChunkIndex;
            return true;
        }

        public void OnChunkEnd(in ArchetypeChunk chunk, int unfilteredChunkIndex, bool useEnabledMask, in v128 chunkEnabledMask, bool chunkWasExecuted)
        {
        }
    }
}