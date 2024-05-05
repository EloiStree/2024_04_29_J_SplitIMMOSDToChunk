
using UnityEngine;

public class BigByteWrapperSetGetChunkableBytesMono : MonoBehaviour, IChunkableBytesSourceArray
{

    public BigByteArrayCompressedDrone16KMono m_target;

    public byte[] GetBytesArray()
    {
        return m_target.GetBytesArray();
    }

    public int GetBytesArrayLenght()
    {
        return m_target.GetBytesArray().Length;
    }

    public int GetHowManyElementMaxAreStoreInCurrentArray()
    {
        return (128*128);
    }

    public int GetSizeOfElementInByteStored()
    {
        return 11;
    }
}