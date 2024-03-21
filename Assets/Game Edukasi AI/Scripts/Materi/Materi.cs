using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Materi
{
    [System.Serializable]
    public struct materiData
    {
        public bool[] materiRow;
    }

    public materiData[] materiDataNumber = new materiData[5];

    public int GetLength()
    {
        return materiDataNumber.Length;
    }
}
