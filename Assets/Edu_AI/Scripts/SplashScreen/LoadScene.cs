using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [Header("Main Settings")]
    public string TargetScene;
    public float Delay;

    void LoadTheScene()
    {
        //Melakukan perpindahan antar scene. Catatan: Scene yang dipanggil sudah didaftarkan di Build Setting
        SceneManager.LoadScene(TargetScene);
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        Invoke("LoadTheScene", Delay);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
