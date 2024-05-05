using UnityEngine;
using Eloi.WatchAndDate;
using Unity.Jobs;
using UnityEngine.Events;
using Unity.Collections;

public class ParsingMono_SNAM16KToShieldDrone : MonoBehaviour
{

    public int m_activeDrone = 128 * 128;
    public bool m_useDefault;
    public STRUCT_ShieldDroneCoreInformation m_storeElement;
    public SNAM16K_ObjectBool m_isDroneConnected;
    public SNAM16K_ObjectBool m_isDroneInCollision;
    public SNAM16K_ObjectFloat m_droneShield;
    public SNAM16K_ObjectVector3 m_dronePosition;
    public SNAM16K_ObjectQuaternion m_droneRotation;


    public SNAM16K_ShieldDroneCoreInformation m_droneCoreInformation;

    public WatchAndDateTimeActionResult m_sliceToDrone;


    public UnityEvent<NativeArray<STRUCT_ShieldDroneCoreInformation>> m_onShieldDroneUpdated;

    [ContextMenu("Collection To Shield Drone")]
    public void CollectionsToShieldDrone()
    {



        m_sliceToDrone.StartCounting();

        if (m_activeDrone > IMMO16K.ARRAY_MAX_SIZE)
            m_activeDrone = IMMO16K.ARRAY_MAX_SIZE;
        new STRUCT_JOB_SlicesToShieldDrone()
        {
            m_drones = m_droneCoreInformation.GetNativeArray(),
            m_isConnected = m_isDroneConnected.GetNativeArray(),
            m_isInCollision = m_isDroneInCollision.GetNativeArray(),
            m_rotation = m_droneRotation.GetNativeArray(),
            m_position = m_dronePosition.GetNativeArray(),
            m_ushortShield = m_droneShield.GetNativeArray(),
            m_currentActiveDrone = m_activeDrone,
            m_emptyDefault = m_storeElement,
            m_useDefault = m_useDefault,

        }.Schedule(m_activeDrone, 64).Complete();

        m_sliceToDrone.StopCounting();
        m_onShieldDroneUpdated.Invoke(m_droneCoreInformation.GetNativeArray());

    }
}

