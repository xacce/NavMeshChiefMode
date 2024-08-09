Pattern:

- Supports only single navmesh (u can use other navmesh patterns)
- U need automatic rebuilding
- U sources adding/removing sometime
- U not need static sources (Only dynamic)
- U sources not moved

Add `NavMeshChiefAuthoring` to some entity.
Edit bounds component

Add `NavMeshChiefSourceAuthoring` to sources in scene or prefabs.
Add `EntityNavMeshSourceFromPhysicsShape` for sample source from PhysicsShape or add EntityNavMeshBoxSource for raw source

U can manipulate updates manually (Bounds changed/bulk update/etc:
Modify buffer `NavMeshSourceElement` on navmeshentity
Add `RebuildNavMesh` component to navmeshentity



