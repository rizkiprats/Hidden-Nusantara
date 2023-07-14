using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    public Sprite karakterImage;
    public string name;
    [TextArea(2, 10)]
    public string[] sentences;
}