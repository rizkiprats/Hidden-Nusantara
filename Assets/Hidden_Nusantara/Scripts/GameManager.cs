using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour, InterfaceDataSave
{
    public static GameManager instance;

    public GameObject activePlayer;

    public GameObject SaveManager;

    public float timer;

    public float timerdefault;

    public Vector3 respawnPoint;

    public bool[] journalEntry;

    [Header("InGame UI")]
    public TextMeshProUGUI timerTxt;
    public GameObject buttonAds;
    [SerializeField] Button _showAdButton;
    public GameObject PausePanel;
    public GameObject JournalPanel;
    public GameObject SavePanel;
    public GameObject CutscenePanel;
    public GameObject DimmerDialog;
    float addedtimer;
    public GameObject PlayerPrefab;
    public GameObject SaveManagerPrefab;
    public GameObject DeleteConfirmDialog;
    public GameObject GameOverPanel;
    private bool reset;
    public GameObject[] journalTexts;

    private void Awake()
    {
        instance = this;

    }


    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("Player") == null)
        {
            activePlayer = Instantiate(PlayerPrefab, respawnPoint, Quaternion.identity);
            activePlayer.name = "Player";
            SaveManager = Instantiate(SaveManagerPrefab, respawnPoint, Quaternion.identity);
            SaveManager = GameObject.Find("DataSaveManager");
            Debug.Log("Player Muncul");
        }

        reset = false;

        Scene scene = SceneManager.GetActiveScene();

        if (scene.buildIndex == 3)
        {
            Time.timeScale = 1; //defaultnya 0 ini buat testing sajah kalau nonaktifkan canvas
            journalEntry = new bool[3]{
                false,
                false,
                false
            };
        }
        else
        {
            Time.timeScale = 1;
        }

        if(timer < 60)
        {
            CutscenePanel.SetActive(false);
            Time.timeScale = 1;
        }

       
       
    }
    public void AddTimer(float aTime)
    {
        buttonAds.SetActive(false);
        timer += aTime;
        print("timer + " + timer);
        GameOverPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void OpenJournal()
    {
        JournalPanel.SetActive(true);

        Time.timeScale = 0;
        Debug.Log("journal entry" + journalEntry[0]+ journalEntry[1]+ journalEntry[2]);
        
        for (int i = 0; i < 3; i++)
        {
            if (journalEntry[i])
            {
                journalTexts[i].SetActive(true);
            }
        }
    }

    public void CloseJournal()
    {
        JournalPanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void PauseGame()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void startgame()
    {
        CutscenePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Startbtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        timer = 30;
        Reset();
        //PresurePlate.instance.isOn = false;
        //Key.instance.isFollowing = false;
        //Door.instance.doorOpen = false;
        //DialogManager.instance.answered = false;
        Box_PresurePlate.instance.Boxpressed = false;
    }

    public void Reset()
    {
        if (activePlayer)
        { 
            reset = true;
            DataSaveManager.instance.SaveGame();
        }
    }

    public void setTimer(float waktu)
    {
        timer = waktu;
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");

    }
    public void SaveSystemPanel()
    {
        SavePanel.SetActive(true);
        PausePanel.SetActive(false);
    }
    public void Back()
    {
        SavePanel.SetActive(false);
        PausePanel.SetActive(true);
    }

    public void deletesave()
    {
        DeleteConfirmDialog.SetActive(true);
    }

    public void canceldelete()
    {
        DeleteConfirmDialog.SetActive(false);
    }
    void Update()
    {

        if (Input.GetKey(KeyCode.Escape) == true)
        {
            PauseGame();
        }



        if (timer > 0f)
        {
            if (DimmerDialog.gameObject.activeInHierarchy == true)
            {
                timer -= 0;
            }
            else
            {
                timer -= Time.deltaTime;
            }
            float minutes = Mathf.FloorToInt(timer / 60);
            float seconds = Mathf.FloorToInt(timer % 60); //modulus
            timerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            //if (seconds % 20 == 0)
            //{
            //  
            //}


        }
        if (timer <= 30f)
        {
            buttonAds.SetActive(true);
        }

        if (timer <= 0f /*&& !isOver*/)
        {

            timerTxt.text = "00:00";
            Time.timeScale = 0;
            GameOverPanel.SetActive(true);
        }
    }

    public void LoadData(GameDataSave data)
    {
        journalEntry = data.Jurnal;
        PresurePlate.instance.isOn = data.isOn;
        DialogManager.instance.answered = data.answeredquestion;
        Debug.Log("answer: " + data.answeredquestion);
        this.timer = data.timer;
        this.respawnPoint = data.playerPosition;
        Key.instance.isFollowing = data.isFollowing;
        Door.instance.doorOpen = data.doorOpen;
        

    }

    public void SaveData(ref GameDataSave data)
    {
        Scene scene = SceneManager.GetActiveScene();

        data.sceneNumber = scene.buildIndex;

        Debug.Log("Scene Number = " + scene.buildIndex);

        data.timer = this.timer;
        if (activePlayer)
        {
            if (reset)
            {
                data.isOn = false;
                data.playerPosition = Vector3.zero;
                DialogManager.instance.answered = false;
                data.isFollowing = false;
                data.doorOpen = false;
            }
            else
            {
                data.Jurnal = journalEntry;
                if(PresurePlate.instance != null)
                    data.isOn = PresurePlate.instance.isOn;
                data.playerPosition = this.activePlayer.transform.position;
                if (Key.instance != null)
                    data.isFollowing = Key.instance.isFollowing;
                if (Door.instance != null)
                data.doorOpen = Door.instance.doorOpen;
                // data.answeredquestion = DialogManager.instance.answered;
            }
            
        }
        

    }

    public void setJurnalEntry(int index,ref GameDataSave data){
        journalEntry[index] = true;
        data.Jurnal[index] = journalEntry[index];
    }



}
