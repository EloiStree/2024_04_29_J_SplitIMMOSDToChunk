
using UnityEngine;

public class SNAM16KSet_ShieldDroneCoreInformation : SNAM_SetDebugGeneric16K<STRUCT_ShieldDroneCoreInformation>
{
    public static StructParserJob_ShieldDroneCoreInformation m_parse= new StructParserJob_ShieldDroneCoreInformation();
    public override STRUCT_ShieldDroneCoreInformation GenerateRandomValue()
    {
        m_parse.GetRandom(out STRUCT_ShieldDroneCoreInformation element);
        return element;
    }

    [ContextMenu("Push Random Value ")]
    public new  void PushRandomValueWithAllTheArraySize() {

        base.PushRandomValueWithAllTheArraySize();
    }
}

