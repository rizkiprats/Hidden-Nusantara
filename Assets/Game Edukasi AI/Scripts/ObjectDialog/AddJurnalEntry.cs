using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddJurnalEntry : MonoBehaviour
{
    public bool addJurnalEntry;
    public int JurnalNumber;
    public void AddJurnal()
    {
        if (addJurnalEntry)
            GameObject.FindObjectOfType<Game_Manager>().JurnalEntry[JurnalNumber] = true;
    }
}
