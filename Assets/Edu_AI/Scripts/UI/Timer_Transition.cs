using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Timer_Transition : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField]private float timer;

    private Image TargetImage;
    public float FadeSpeed;

    public GameObject gameObject;

    public float Delay;
    bool StartFadeOut = false;
    bool StartFadeIn = false;

    [SerializeField]private UnityEvent TriggerEvent;

    void Awake()
    {
        //Membuat TargetImage sesuai dengan ukuran layar
        //TargetImage.rectTransform.localScale = new Vector2(Screen.width, Screen.height);
        //Membuat game object dan TargetImage 'menghilang' dari canvas
        TargetImage = gameObject.GetComponent<Image>();
        TargetImage.enabled = false;
        TargetImage.color = Color.clear;
    }

    void Start()
    {
        //mematikan game object sebelum akan ditampilkan
        gameObject.SetActive(false);
        //Memanggil fungsi SetTrue_StartFadeOut berdasarkan delay
        Invoke("SetTrue_StartFadeOut", Delay);
    }

    void FadeOut()
    {
        //Membuat warna TargetImage pakai transisi Lerp dari transparan ke warna dasar gambar
        TargetImage.color = Color.Lerp(TargetImage.color, Color.white, FadeSpeed * Time.deltaTime);
    }

    void FadeIn()
    {
        //Membuat warna TargetImage pakai transisi Lerp dari warna awal ke warna transparan
        TargetImage.color = Color.Lerp(TargetImage.color, Color.clear, FadeSpeed * Time.deltaTime);
       
    }

    void SetTrue_StartFadeOut()
    {
        //mengaktifkan game object yang akan ditampilkan
        gameObject.SetActive(true);
        //Mengubah nilai StartFadeOut menjadi true
        StartFadeOut = true;
        //Membuat TargetImage 'muncul' dari canvas
        TargetImage.enabled = true;
    }

    void SetTrue_StartFadeIn()
    {
        //Mengubah nilai StartFadeIn menjadi true
        StartFadeIn = true;

    }


    // Update is called once per frame
    void Update()
    {
        //Eksekusi fungsi FadeOut jika nilai StartFadeOut = true
        if (StartFadeOut)
        {
            FadeOut();
            Invoke("SetTrue_StartFadeIn", 5);
        }

        


        if (StartFadeIn)
        {
            FadeIn();
            InvokeTrigger();

        }
        



    }

    public void InvokeTrigger()
    {
        TriggerEvent.Invoke();
    }


}
