using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class ProgressManager : MonoBehaviour, InterfaceSave
{
    public string Username;
    public int UserPoint;
    public int SceneIndex;

    public TMP_InputField Input_Username;
    public TMP_Text Welcome_User;

    public TMP_Text WelcomeMenuUser;
    public TMP_Text Menu_Username;
    public TMP_Text Menu_UserPoint;

    [SerializeField] private UnityEvent TriggerHasSaveData;
    [SerializeField] private UnityEvent TriggerHasNotSaveData;
    [SerializeField] private UnityEvent TriggerHasSceneIndexSaved;
    [SerializeField] private UnityEvent TriggerHasNotSceneIndexSaved;

    //public QuestProgress questProgressLevel1;
    //public QuestProgress questProgressLevel2;


    void Start()
    {
        if (SaveManager.instance.HasSaveData())
        {
            TriggerHasSaveData.Invoke();
        }
        else
        {
            TriggerHasNotSaveData.Invoke();
        }
    }

    void Update()
    {   
        if (Welcome_User != null)
        {
            Welcome_User.text = "Halo " + Username;
        }

        if (WelcomeMenuUser != null)
        {
            WelcomeMenuUser.text = "Halo " + Username + ", selamat datang di permainan petualangan & belajar AI.";
        }
        if (Menu_Username != null)
        {
            Menu_Username.text = Username;
        }
        if (Menu_UserPoint != null)
        {
            Menu_UserPoint.text = UserPoint.ToString();
        }

        if(SceneIndex == 0 || SceneIndex == 1 || SceneIndex == 2)
        {
            TriggerHasNotSceneIndexSaved.Invoke();
        }
        else
        {
            TriggerHasSceneIndexSaved.Invoke();
        }
    }

    public void AddUserPoint(int Point)
    {
        UserPoint = UserPoint + Point;
    }

    public void ResetUserPoint()
    {
        UserPoint = 0;
    }

    public void InputUser_name()
    {
        Username = Input_Username.text;

    }

    public void SaveData()
    {
        FindObjectOfType<SaveManager>().SaveGame();
    }

    public void DeleteSave()
    {
        FindObjectOfType<SaveManager>().DeleteSaveData();
    }

    public void CreateNewData()
    {
        FindObjectOfType<SaveManager>().CreateNewSaveData();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ItfLoadData(SaveData data)
    {
        if (data != null)
        {
            Username = data.playerName;
        }
        if (SaveManager.instance.HasSaveData())
        {
            UserPoint = data.playerPoint;
            SceneIndex = data.SavedIndexScene;
        }
    }

    public void ItfSaveData(ref SaveData data)
    {
        if (Username != null)
        {
            data.playerName = Username;
        }
        if (SaveManager.instance.HasSaveData())
        {
            data.playerPoint = UserPoint;
        }
    }
}
