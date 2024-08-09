#if UNITY_EDITOR
using NavMeshChiefMode.Runtime;
using NavMeshDots.Hybrid;
using Unity.Entities;
using UnityEngine;

namespace NavMeshChiefMode.Hybrid.Authoring
{
    public class NavMeshChiefSourceAuthoring : MonoBehaviour
    {
        private class NavMeshChiefSourceBaker : Baker<NavMeshChiefSourceAuthoring>
        {
            public override void Bake(NavMeshChiefSourceAuthoring authoring)
            {
                if (authoring.TryGetComponent(out IEntityNavMeshSourceProvider provider))
                {
                    if (provider.TryGetSource(out var source))
                    {
                        var e = GetEntity(TransformUsageFlags.Dynamic);
                        AddComponent(e, new NavMeshChiefRuntimeSource() { source = source.AsNative() });
                    }
                    else
                    {
                        Debug.LogWarning($"Invalid source provider. Cant get source for: {authoring}", authoring.gameObject);
                    }
                }
                else
                {
                    Debug.LogWarning($"Invalid source provider. Source provider not found for: {authoring}", authoring.gameObject);
                }
            }
        }
    }
}
#endif