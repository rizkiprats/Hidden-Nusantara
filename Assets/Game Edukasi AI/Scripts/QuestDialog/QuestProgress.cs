using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestProgress
{
    [System.Serializable]
    public struct QuestData
    {
        public GameObject[] QuestObject;
    }

    public QuestData[] QuestDataNumber = new QuestData[1];

    public int GetLength()
    {
        return QuestDataNumber.Length;
    }
}