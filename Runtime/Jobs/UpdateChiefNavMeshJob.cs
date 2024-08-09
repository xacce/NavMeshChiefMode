using NavMeshDots.Runtime;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

namespace NavMeshChiefMode.Runtime.Jobs
{
    [BurstCompile]
    public partial struct UpdateChiefNavMeshJob : IJob
    {
        public EntityCommandBuffer ecb;
        public Entity entity;
        public NativeArray<int> hasUpdates;

        [BurstCompile]
        public void Execute()
        {
            if (hasUpdates[0] > 0)
            {
                ecb.AddComponent<ReBuildNavMesh>(entity);
                Debug.Log("Nav mesh updates");
            }
        }
    }
}