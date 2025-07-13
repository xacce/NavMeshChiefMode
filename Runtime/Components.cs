using Unity.Entities;
using UnityEngine.AI;

namespace NavMeshChiefMode.Runtime
{
    public partial struct NavMeshChief : IComponentData
    {
    }


    public partial struct NavMeshChiefSourceRegistered :  ICleanupComponentData
    {
        
    }

    public partial struct NavMeshChiefRuntimeSource : IComponentData
    {
        public NavMeshBuildSource source; //Todo make blob asset!!!
    }
}