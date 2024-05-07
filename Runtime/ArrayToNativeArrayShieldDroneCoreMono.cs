public class ArrayToNativeArrayShieldDroneCoreMono : SplitDebugArrayToNativeArrayGenericMono<STRUCT_ShieldDroneCoreInformation>
{
    public StructRandomizerJob_ShieldDroneCoreInformation r = new StructRandomizerJob_ShieldDroneCoreInformation();

    public override STRUCT_ShieldDroneCoreInformation GetRandomValue()
    {
        r.GetRandom(out STRUCT_ShieldDroneCoreInformation t);
        return t;
    }
}
