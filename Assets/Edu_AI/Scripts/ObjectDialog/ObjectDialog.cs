using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ObjectDialog
{
    public Sprite objectImage;
    public string objectName;

    [TextArea(2, 10)]
    public string[] sentences;
   
}
