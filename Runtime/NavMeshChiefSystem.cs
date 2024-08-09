using NavMeshChiefMode.Runtime.Jobs;
using NavMeshDots.Runtime;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

namespace NavMeshChiefMode.Runtime
{
    [BurstCompile]
    [UpdateAfter(typeof(NavMeshDotsManagedSystem))]
    [UpdateInGroup(typeof(NavMeshDotsSystemGroup))]
    public partial struct NavMeshChiefSystem : ISystem, ISystemStartStop
    {
        private Entity _chiefEntity;

        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndInitializationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<NavMeshChief>();
        }

        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var chiefSources = SystemAPI.GetBuffer<NavMeshSourceElement>(_chiefEntity);
            var ecbSingleton = SystemAPI.GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>();
            var hasUpdates = new NativeArray<int>(1, Allocator.TempJob);
            hasUpdates[0] = 0;

            var unreg = new UnRegisterChiefNewNavMeshSourcesjob()
            {
                ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged),
                sources = chiefSources,
                hasUpdates = hasUpdates,
            }.Schedule(state.Dependency);
            var reg = new RegisterChiefNewNavMeshSourcesjob()
            {
                sources = chiefSources,
                ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged),
                hasUpdates = hasUpdates,
            }.Schedule(unreg);
            var upd = new UpdateChiefNavMeshJob()
            {
                hasUpdates = hasUpdates,
                ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged),
                entity = _chiefEntity,
            }.Schedule(reg);
            state.Dependency = upd;
            hasUpdates.Dispose(state.Dependency);
        }

        public void OnStartRunning(ref SystemState state)
        {
            _chiefEntity = SystemAPI.GetSingletonEntity<NavMeshChief>();
        }

        public void OnStopRunning(ref SystemState state)
        {
        }
    }
}