using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MateriTest : MonoBehaviour
{
    public MateriDataList materi;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(materi.materiListData.Length);
    }
}
