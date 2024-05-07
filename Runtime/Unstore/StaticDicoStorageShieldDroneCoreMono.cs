using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticDicoStorageHoldbytesChunkGenericMono<T, J, D> : 
    StaticDicoStorageGenericMono
    <HoldBytesForChunkGenericElement<T, J>,
        CreateDefaultValue<HoldBytesForChunkGenericElement<T, J>>>
     where T : struct where J : struct, I_HowToParseElementInByteNativeArray<T>
    where D : struct, I_ProvideRandomAndDefaultElementInJob<T>
    
{


    [ContextMenu("Reset Guid ID")]
    private new void ResetGuidId()
    {
        base.ResetGuidId();
    }
}
