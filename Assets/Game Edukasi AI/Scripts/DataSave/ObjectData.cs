using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectData
{
    [System.Serializable]
    public struct ObjectProgress
    {
        public bool[] QuestObject;
    }

    public ObjectProgress[] ObjectDataNumber = new ObjectProgress[1];

    public int GetLength()
    {
        return ObjectDataNumber.Length;
    }
}