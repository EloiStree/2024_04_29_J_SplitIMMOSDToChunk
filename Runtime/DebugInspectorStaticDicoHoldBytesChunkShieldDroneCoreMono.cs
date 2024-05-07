using UnityEngine;

public class DebugInspectorStaticDicoHoldBytesChunkShieldDroneCoreMono : DebugInspectorStaticDicoValueGeneric<
    HoldBytesForChunkGenericElement<STRUCT_ShieldDroneCoreInformation, StructParserJob_ShieldDroneCoreInformation>,
    CreateDefaultValue<HoldBytesForChunkGenericElement<STRUCT_ShieldDroneCoreInformation, StructParserJob_ShieldDroneCoreInformation>>>
{
    [ContextMenu("Update Ref")]
    public new void UpdateReference()
    {
        base.UpdateReference();
    }
}
