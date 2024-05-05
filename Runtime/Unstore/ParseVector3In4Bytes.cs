using System;
using UnityEngine;

public class ParseVector3In4Bytes : MonoBehaviour {


    public Vector3 m_valueIn;
    public Vector3 m_valueOut;

    public byte m_droneState;
    public string m_droneStateString;

    public byte m_bX0;
    public byte m_bX1;
    public byte m_bX2;
    public byte m_bY0;
    public byte m_bY1;
    public byte m_bY2;
    public byte m_bZ0;
    public byte m_bZ1;
    public byte m_bZ2;

    public byte m_bx;
    public byte m_by;
    public byte m_bz;

    private void OnValidate()
    {


        m_bx = (byte)((m_droneState >> 0 & 0b00000011));
        m_by = (byte)((m_droneState >> 2 & 0b00000011));
        m_bz = (byte)((m_droneState >> 4 & 0b00000011));


        FloatToArray(m_valueIn.x, out m_bX0, out m_bX1, out m_bX2);
        FloatToArray(m_valueIn.y, out m_bY0, out m_bY1, out m_bY2);
        FloatToArray(m_valueIn.z, out m_bZ0, out m_bZ1, out m_bZ2);

        FloatToArray(out m_valueOut.x, m_bX0, m_bX1, m_bX2);
        FloatToArray(out m_valueOut.y, m_bY0, m_bY1, m_bY2);
        FloatToArray(out m_valueOut.z, m_bZ0, m_bZ1, m_bZ2);

        m_droneState = 0;
        m_droneState |= (byte)(m_bX0 << 4);
        m_droneState |= (byte)(m_bY0 << 2);
        m_droneState |= (byte)(m_bZ0 << 0);
        m_droneStateString = Convert.ToString(m_droneState, 2);


    }
    private static void FloatToArray(float numberFloat, out byte b0, out byte b1, out byte b2)
    {
        int number = (int)(numberFloat * 1000f);
        b0 = (byte)((number >> 16) & 0xFF);
        b1 = (byte)((number >> 8) & 0xFF);
        b2 = (byte)(number & 0xFF);
    }
    public const float m_maxDistance = 262.143f;
    private static void FloatToArray(out float numberFloat, in byte b0, in byte b1, in byte b2)
    {
        int value = (b0 << 16) | (b1 << 8) | (b2);
        numberFloat = Mathf.Clamp(value * 0.001f, 0f, m_maxDistance);
    }
}