using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameDataSave
{
    public float timer;
    public Vector3 playerPosition;
    public int sceneNumber;
    public bool isOn;
    public bool doorOpen;
    public bool isFollowing;
    public Vector3 Boxlv3Pos;
    public Vector3 platelv3Pos;
    public bool boxpressedlv3;
    public bool answeredquestion;
    public bool[] Jurnal;


    public GameDataSave()
    {
        this.isOn = false;
        this.sceneNumber = 1;
        this.timer = 60;
        playerPosition = Vector3.zero;
        this.doorOpen = false;
        this.isFollowing = false;
        this.Boxlv3Pos = Vector3.zero;
        this.boxpressedlv3 = false;
        this.platelv3Pos = Vector3.zero;
        this.answeredquestion = false;
        this.Jurnal = new bool[3]{
            false,
            false,
            false
        };
    }
}
