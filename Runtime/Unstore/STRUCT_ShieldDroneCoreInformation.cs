using System.Runtime.InteropServices;
using UnityEngine;
/// <summary>
/// This class is use to parse information from one computer to an other in chunk of data.
/// It represent the minimum needed for the drone game.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[System.Serializable]
public struct STRUCT_ShieldDroneCoreInformation
{
    /// <summary>
    /// Is the player connected or playing the game. Does it need to be computed.
    /// </summary>
    public bool m_isDroneConnectedAndInGame;
    /// <summary>
    /// Is the player is currently in collision with anything in the game
    /// </summary>
    public bool m_isInCollision;
    /// <summary>
    /// What is the current shield state from 0-65535 in game and  0-255 on the network
    /// </summary>
    public float m_ushortShield;

    /// <summary>
    /// Position of the drone between 0-256200 mm   256.200 in unity
    /// </summary>
    public Vector3 m_position;
    /// <summary>
    /// Rotation of the current drone store in 255 255 255 euler value when split.
    /// </summary>
    public Quaternion m_rotation;
}



