using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DroneIMMO;
using Unity.Collections;
using Eloi.WatchAndDate;
using System.Runtime.InteropServices;
using Unity.Jobs;
using Unity.Burst;


public class DemoJob_ShieldDroneToSNAM16KMono : MonoBehaviour
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
    public WatchAndDateTimeActionResult m_droneToSlice;


    [ContextMenu("Slice Drone Slice")]
    public void SliceDroneSlice()
    {



        m_sliceToDrone.StartCounting();

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

        }.Schedule(IMMO16K.ARRAY_MAX_SIZE, 64).Complete();

        m_sliceToDrone.StopCounting();

        m_droneToSlice.StartCounting();

        new STRUCT_JOB_ShieldDroneToSlices()
        {
            m_drones = m_droneCoreInformation.GetNativeArray(),
            m_isConnected = m_isDroneConnected.GetNativeArray(),
            m_isInCollision = m_isDroneInCollision.GetNativeArray(),
            m_rotation = m_droneRotation.GetNativeArray(),
            m_position = m_dronePosition.GetNativeArray(),
            m_ushortShield = m_droneShield.GetNativeArray(),
            m_currentActiveDrone = m_activeDrone,
            m_useDefaut = m_useDefault,

        }.Schedule(IMMO16K.ARRAY_MAX_SIZE, 64).Complete();

        m_droneToSlice.StopCounting();



    }


    [ContextMenu("Drone Slice Drone")]
    public void DroneSliceDrone()
    {
        m_droneToSlice.StartCounting();

        new STRUCT_JOB_ShieldDroneToSlices()
        {
            m_drones = m_droneCoreInformation.GetNativeArray(),
            m_isConnected = m_isDroneConnected.GetNativeArray(),
            m_isInCollision = m_isDroneInCollision.GetNativeArray(),
            m_rotation = m_droneRotation.GetNativeArray(),
            m_position = m_dronePosition.GetNativeArray(),
            m_ushortShield = m_droneShield.GetNativeArray(),
            m_currentActiveDrone = m_activeDrone,
            m_useDefaut = m_useDefault,

        }.Schedule(IMMO16K.ARRAY_MAX_SIZE, 64).Complete();

        m_droneToSlice.StopCounting();


        m_sliceToDrone.StartCounting();

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

        }.Schedule(IMMO16K.ARRAY_MAX_SIZE, 64).Complete();

        m_sliceToDrone.StopCounting();

   



    }
}


[BurstCompile(CompileSynchronously = true)]
public struct STRUCT_JOB_SlicesToShieldDrone: IJobParallelFor
{
    public int m_currentActiveDrone;
    public bool m_useDefault;
   
    public STRUCT_ShieldDroneCoreInformation m_emptyDefault;

    [ReadOnly]
    public NativeArray<bool> m_isConnected;
    [ReadOnly]
    public NativeArray<bool> m_isInCollision;
    [ReadOnly]
    public NativeArray<float> m_ushortShield;
    [ReadOnly]
    public NativeArray<Vector3> m_position;
    [ReadOnly]
    public NativeArray<Quaternion> m_rotation;


    [WriteOnly]
    public NativeArray<STRUCT_ShieldDroneCoreInformation> m_drones;

    public void Execute(int index)
    {
        if (index < m_currentActiveDrone)
        {
            STRUCT_ShieldDroneCoreInformation d = new STRUCT_ShieldDroneCoreInformation();
            d.m_position = m_position[index];
            d.m_rotation = m_rotation[index];
            d.m_ushortShield = m_ushortShield[index];
            d.m_isInCollision = m_isInCollision[index];
            d.m_isDroneConnectedAndInGame = m_isConnected[index];
            m_drones[index] = d;
        }
        else {
            if (m_useDefault) { 
                m_drones[index] = m_emptyDefault;
            }
        }
    }
}
[BurstCompile(CompileSynchronously = true)]
public struct STRUCT_JOB_ShieldDroneToSlices : IJobParallelFor
{

    public int m_currentActiveDrone;
    public bool m_useDefaut;
    [ReadOnly]
    public NativeArray<STRUCT_ShieldDroneCoreInformation> m_drones;

    [WriteOnly]
    public NativeArray<bool> m_isConnected;
    [WriteOnly]
    public NativeArray<bool> m_isInCollision;
    [WriteOnly]
    public NativeArray<float> m_ushortShield;
    [WriteOnly]
    public NativeArray<Vector3> m_position;
    [WriteOnly]
    public NativeArray<Quaternion> m_rotation;


    public void Execute(int index)
    {
        if (index < m_currentActiveDrone)
        {
            m_position[index] = m_drones[index].m_position;
            m_rotation[index] = m_drones[index].m_rotation;
            m_ushortShield[index] = m_drones[index].m_ushortShield;
            m_isInCollision[index] = m_drones[index].m_isInCollision;
            m_isConnected[index] = m_drones[index].m_isDroneConnectedAndInGame;
        }
        else
        {
            if (m_useDefaut)
            {
                m_position[index] = Vector3.zero;
                m_rotation[index] = Quaternion.identity;
                m_ushortShield[index] = 0;
                m_isInCollision[index] = false;
                m_isConnected[index] = false;
            }
        }
    }

}



    public struct StructParserJob_ShieldDroneCoreInformation : I_HowToParseElementInByteNativeArray<STRUCT_ShieldDroneCoreInformation>, I_HowToParseByteNativeArrayToElement<STRUCT_ShieldDroneCoreInformation>, I_ProvideRandomAndDefaultElementInJob<STRUCT_ShieldDroneCoreInformation>
    {
        public static int m_elementSize = 11;
        public const float m_shieldMaxSize = ushort.MaxValue;
        public const float m_maxDistance = 262.143f;
        public const float m_minDistance = 0f;

        public STRUCT_ShieldDroneCoreInformation m_default;


        public int GetSizeOfElementInBytesCount()
        {
            return m_elementSize;
        }

        public void ParseBytesFromElement(NativeArray<byte> source, in int indexElement, in STRUCT_ShieldDroneCoreInformation drone)
        {
            //STRUCT_Job_TurnDroneUnityIntoDroneBits

            Vector3 euler = drone.m_rotation.eulerAngles;
            byte b0 = 0, b1 = 0, b2 = 0;

            byte m_droneState = 0;



            int offsetStart = indexElement * m_elementSize;


            FloatToArray(drone.m_position.x, out b0, out b1, out b2);
            m_droneState |= (byte)(b0 << 4);
            //bits.m_positionUShortX0 = b1;
            source[offsetStart + 1] = b1;
            //bits.m_positionUShortX1 = b2;
            source[offsetStart + 2] = b2;

            FloatToArray(drone.m_position.y, out b0, out b1, out b2);
            m_droneState |= (byte)(b0 << 2);
            //bits.m_positionUShortY0 = b1;
            source[offsetStart + 3] = b1;
            //bits.m_positionUShortY1 = b2;
            source[offsetStart + 4] = b2;


            FloatToArray(drone.m_position.z, out b0, out b1, out b2);
            m_droneState |= (byte)(b0 << 0);
            //bits.m_positionUShortZ0 = b1;
            source[offsetStart + 5] = b1;
            //bits.m_positionUShortZ1 = b2;
            source[offsetStart + 6] = b2;

            //bits.m_rotationEulerX = (byte)(((euler.x % 360f) / 360f) * 255f);
            source[offsetStart + 7] = (byte)(((euler.x % 360f) / 360f) * 255f);
            //bits.m_rotationEulerY = (byte)(((euler.y % 360f) / 360f) * 255f);
            source[offsetStart + 8] = (byte)(((euler.y % 360f) / 360f) * 255f);
            //bits.m_rotationEulerZ = (byte)(((euler.z % 360f) / 360f) * 255f);
            source[offsetStart + 9] = (byte)(((euler.z % 360f) / 360f) * 255f);

            if (drone.m_isDroneConnectedAndInGame)
                m_droneState |= 0b10000000;
            if (drone.m_isInCollision)
                m_droneState |= 0b01000000;
            source[offsetStart + 0] = m_droneState;
            //bits.m_shieldState = (byte)((drone.m_shield / m_shieldSizeInGame) * 255f);

            source[offsetStart + 10] = (byte)(((drone.m_ushortShield / m_shieldMaxSize) * 255f));


        }


        public void ParseBytesToElement(NativeArray<byte> source, in int indexElement, out STRUCT_ShieldDroneCoreInformation drone)
        {
            //STRUCT_Job_TurnDroneBitsIntoDroneUnity
            drone = new STRUCT_ShieldDroneCoreInformation();
            int offsetStart = indexElement * m_elementSize;

            byte dronestate = source[offsetStart + 0];

            drone.m_isDroneConnectedAndInGame = (dronestate & (0b10000000)) > 0;
            drone.m_isInCollision = (dronestate & (0b01000000)) > 0;

            FloatToArray(out drone.m_position.x, (byte)((dronestate >> 4 & 0b00000011)), source[offsetStart + 1], source[offsetStart + 2]);
            FloatToArray(out drone.m_position.y, (byte)((dronestate >> 3 & 0b00000011)), source[offsetStart + 3], source[offsetStart + 4]);
            FloatToArray(out drone.m_position.z, (byte)((dronestate >> 0 & 0b00000011)), source[offsetStart + 5], source[offsetStart + 6]);

            drone.m_rotation = Quaternion.Euler(
                (source[offsetStart + 7] / 255f) * 360f,
                (source[offsetStart + 8] / 255f) * 360f,
                (source[offsetStart + 9] / 255f) * 360f);
            drone.m_ushortShield = (source[offsetStart + 10] / 255f) * (float)m_shieldMaxSize;
        }




        public void GetDefault(out STRUCT_ShieldDroneCoreInformation element) =>
            element = new STRUCT_ShieldDroneCoreInformation();

        public void GetRandom(out STRUCT_ShieldDroneCoreInformation element)
        {
            System.Random r = new System.Random();
            element = new STRUCT_ShieldDroneCoreInformation();
            element.m_ushortShield = (float)(r.NextDouble() * ushort.MaxValue);
            element.m_isInCollision = r.NextDouble() > 0.5;
            element.m_isDroneConnectedAndInGame = r.NextDouble() > 0.5;
            element.m_position = new Vector3((float)r.NextDouble() * m_maxDistance, (float)r.NextDouble() * m_maxDistance, (float)r.NextDouble() * m_maxDistance);
            element.m_rotation = Quaternion.Euler((float)r.NextDouble() * 360f, (float)r.NextDouble() * 360f, (float)r.NextDouble() * 360f);

        }

        public void SetWithDefault(NativeArray<STRUCT_ShieldDroneCoreInformation> source, in int indexElement)
        {
            GetDefault(out STRUCT_ShieldDroneCoreInformation v);
            source[indexElement] = v;
        }

        public void SetWithRandom(NativeArray<STRUCT_ShieldDroneCoreInformation> source, in int indexElement)
        {

            GetRandom(out STRUCT_ShieldDroneCoreInformation v);
            source[indexElement] = v;
        }



        private static void FloatToArray(float numberFloat, out byte b0, out byte b1, out byte b2)
        {
            int number = (int)(numberFloat * 1000f);
            b0 = (byte)((number >> 16) & 0xFF);
            b1 = (byte)((number >> 8) & 0xFF);
            b2 = (byte)(number & 0xFF);
        }
        private static void FloatToArray(out float numberFloat, in byte b0, in byte b1, in byte b2)
        {
            int value = (b0 << 16) | (b1 << 8) | (b2);
            numberFloat = Mathf.Clamp(value * 0.001f, 0f, m_maxDistance);
        }
    
}

