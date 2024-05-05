using System;
using UnityEngine;

public class VerifiedStructParserMono_DroneShieldCoreInformation : VerifiedStructParserMono<STRUCT_ShieldDroneCoreInformation, StructParserJob_ShieldDroneCoreInformation>
{
    [ContextMenu("Tick")]
    public override void CallParseCheck()
    {
        base.ParseCheck();
    }


}
