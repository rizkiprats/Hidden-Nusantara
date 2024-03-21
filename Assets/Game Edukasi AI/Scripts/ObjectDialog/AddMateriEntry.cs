using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMateriEntry : MonoBehaviour
{
    public bool addMateriEntry;
    
    public enum Materi { Materi0,Materi1,Materi2,Materi3,Materi4}
    
    public Materi materi;
    
    public int MateriNumber;

    public void AddMateri()
    {
        if (addMateriEntry)
        {
            switch (materi)
            {
                case Materi.Materi0:
                    GameObject.FindObjectOfType<Enum_Materi>().Materi.materiDataNumber[0].materiRow[MateriNumber] = true;
                    GameObject.FindObjectOfType<Enum_Materi>().materiList = Enum_Materi.MateriList.Materi0;
                    break;
                case Materi.Materi1:
                    GameObject.FindObjectOfType<Enum_Materi>().Materi.materiDataNumber[1].materiRow[MateriNumber] = true;
                    GameObject.FindObjectOfType<Enum_Materi>().materiList = Enum_Materi.MateriList.Materi1;
                    break;
                case Materi.Materi2:
                    GameObject.FindObjectOfType<Enum_Materi>().Materi.materiDataNumber[2].materiRow[MateriNumber] = true;
                    GameObject.FindObjectOfType<Enum_Materi>().materiList = Enum_Materi.MateriList.Materi2;
                    break;
                case Materi.Materi3:
                    GameObject.FindObjectOfType<Enum_Materi>().Materi.materiDataNumber[3].materiRow[MateriNumber] = true;
                    GameObject.FindObjectOfType<Enum_Materi>().materiList = Enum_Materi.MateriList.Materi3;
                    break;
                case Materi.Materi4:
                    GameObject.FindObjectOfType<Enum_Materi>().Materi.materiDataNumber[4].materiRow[MateriNumber] = true;
                    GameObject.FindObjectOfType<Enum_Materi>().materiList = Enum_Materi.MateriList.Materi4;
                    break;
            }
        }    
    }
}