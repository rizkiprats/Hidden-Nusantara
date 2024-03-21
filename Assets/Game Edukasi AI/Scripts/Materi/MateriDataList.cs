using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MateriDataList
{
    [System.Serializable]
    public struct MateriListData
    {
        public bool[] materiData;
    }

    public MateriListData[] materiListData = new MateriListData[5];

    public int GetLength()
    {
        return materiListData.Length;
    }
}
