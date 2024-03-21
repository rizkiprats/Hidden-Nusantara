using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InterfaceSave
{
    void ItfLoadData(SaveData data);
    void ItfSaveData(ref SaveData data);
}
    
