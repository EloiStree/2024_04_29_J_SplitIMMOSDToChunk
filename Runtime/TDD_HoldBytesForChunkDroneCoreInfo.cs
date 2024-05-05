using System.Collections;
using Unity.Collections;
using UnityEngine;

[System.Serializable]
public class TDD_HoldBytesForChunkDroneCoreInfo : TDD_HoldBytesForChunkGenericElement<STRUCT_ShieldDroneCoreInformation, StructParserJob_ShieldDroneCoreInformation, StructRandomizerJob_ShieldDroneCoreInformation>
{
    [ContextMenu("Push Ref")]
    public new void PushChunksAsBytesRef()
    {
        base.PushChunksAsBytesRef();
    }
    [ContextMenu("Push Copy")]
    public new void PushChunksAsBytesCopy()
    {
        base.PushChunksAsBytesCopy();
    }

    [ContextMenu("Push Random With Ref")]
    public void PushRandomWithRef()
    {

        base.RandomizeArrayWithForLoop();
        base.PushChunksAsBytesRef();
    }
    [ContextMenu("Push Random With copy")]
    public void PushRandomWithCopy()
    {

        base.RandomizeArrayWithForLoop();
        base.PushChunksAsBytesCopy();
    }
}


