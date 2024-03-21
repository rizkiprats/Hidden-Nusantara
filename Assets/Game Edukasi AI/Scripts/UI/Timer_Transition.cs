using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Timer_Transition : MonoBehaviour
{
    //[SerializeField]private float timer;
    public float Duration;
    
    private Image TargetImage;
    public float FadeSpeed;

    public GameObject gameObject;

    public float Delay;
    bool StartFadeOut = false;
    bool StartFadeIn = false;

    [SerializeField]private UnityEvent TriggerEvent;

    void Awake()
    {
        TargetImage = gameObject.GetComponent<Image>();
        TargetImage.enabled = false;
        TargetImage.color = Color.clear;
    }

    void Start()
    {
        gameObject.SetActive(false);
        Invoke("SetTrue_StartFadeOut", Delay);
    }

    void FadeOut()
    {
        TargetImage.color = Color.Lerp(TargetImage.color, Color.white, FadeSpeed * Time.deltaTime);
    }

    void FadeIn()
    {
        TargetImage.color = Color.Lerp(TargetImage.color, Color.clear, FadeSpeed * Time.deltaTime);
    }

    void SetTrue_StartFadeOut()
    {
        gameObject.SetActive(true);
        
        StartFadeOut = true;
        
        TargetImage.enabled = true;
    }

    void SetTrue_StartFadeIn()
    {
        StartFadeIn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (StartFadeOut)
        {
            FadeOut();
            Invoke("SetTrue_StartFadeIn", Duration);
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