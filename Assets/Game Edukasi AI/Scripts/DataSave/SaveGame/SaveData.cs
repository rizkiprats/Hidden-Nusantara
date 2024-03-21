using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public string playerName;
    public int playerPoint;
    public Vector3 playerPosition;
    public int SavedIndexScene;
    public ObjectData questProgressLevel1;
    public ObjectData questProgressLevel2;

    public SaveData()
    {
        playerName = "";
        playerPoint = 0;
        playerPosition = Vector3.zero;
        SavedIndexScene = 3;
    }
}