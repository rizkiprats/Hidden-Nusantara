using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enum_Materi : MonoBehaviour
{
    public enum MateriList { NoneMateri,Materi0,Materi1, Materi2, Materi3, Materi4}

    public MateriList materiList;

    public Materi Materi;

    public GameObject[] MateriPanel0;
    public GameObject[] MateriPanel1;
    public GameObject[] MateriPanel2;
    public GameObject[] MateriPanel3;
    public GameObject[] MateriPanel4;

    void Start()
    {
        materiList = MateriList.NoneMateri;
    }

    // Update is called once per frame
    void Update()
    {
        switch (materiList)
        {
            case MateriList.NoneMateri:
                break;
            case MateriList.Materi0:
                for (int i = 0; i < Materi.materiDataNumber[0].materiRow.Length; i++)
                {
                    if (Materi.materiDataNumber[0].materiRow[i])
                    {
                        if(MateriPanel0[i] != null)
                        {
                            MateriPanel0[i].SetActive(true);
                        }
                    }
                }
                break;
            case MateriList.Materi1:
                for (int j = 0; j < Materi.materiDataNumber[1].materiRow.Length; j++)
                {
                    if (Materi.materiDataNumber[1].materiRow[j])
                    {
                        if (MateriPanel1[j] != null)
                        {
                            MateriPanel1[j].SetActive(true);
                        }
                    }
                }
                break;
            case MateriList.Materi2:
                for (int k = 0; k < Materi.materiDataNumber[2].materiRow.Length; k++)
                {
                    if (Materi.materiDataNumber[2].materiRow[k])
                    {
                        if (MateriPanel2[k] != null)
                        {
                            MateriPanel2[k].SetActive(true);
                        }
                    }
                }
                break;
            case MateriList.Materi3:
                for (int l = 0; l < Materi.materiDataNumber[3].materiRow.Length; l++)
                {
                    if (Materi.materiDataNumber[3].materiRow[l])
                    {
                        if (MateriPanel3[l] != null)
                        {
                            MateriPanel3[l].SetActive(true);
                        }
                    }
                }
                break;
            case MateriList.Materi4:
                for (int m = 0; m < Materi.materiDataNumber[4].materiRow.Length; m++)
                {
                    if (Materi.materiDataNumber[4].materiRow[m])
                    {
                        if (MateriPanel4[m] != null)
                        {
                            MateriPanel4[m].SetActive(true);
                        }
                    }
                }
                break;
        }  
    }
}