using UnityEngine;

public class NativeToHoldBytesForChunkShieldDroneCoreMono :
    NativeToHoldBytesForChunkGenericElementMono<STRUCT_ShieldDroneCoreInformation, StructParserJob_ShieldDroneCoreInformation, StructRandomizerJob_ShieldDroneCoreInformation>
{


    [ContextMenu("Create or Refresh Holder")]
    public new void CreateOrRefreshHolder()
    {
        base.CreateOrRefreshHolder();
    }

}
