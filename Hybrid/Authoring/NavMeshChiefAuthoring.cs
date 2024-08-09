#if UNITY_EDITOR
using NavMeshChiefMode.Runtime;
using NavMeshDots.Hybrid;
using Unity.Entities;
using UnityEngine;

namespace NavMeshChiefMode.Hybrid.Authoring
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(EntityDynamicNavMeshAuthoring))]
    [RequireComponent(typeof(EntityDynamicNavMeshBoundsAuthoring))]
    [RequireComponent(typeof(EntityNavMeshSourcesDynamicAuthoring))]
    public class NavMeshChiefAuthoring : MonoBehaviour
    {
        private class NavMeshChiefBaker : Baker<NavMeshChiefAuthoring>
        {
            public override void Bake(NavMeshChiefAuthoring authoring)
            {
                AddComponent<NavMeshChief>(GetEntity(TransformUsageFlags.None));
            }
        }
    }
}
#endif