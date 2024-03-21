using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class GameManager : MonoBehaviour, InterfaceDataSave
{
    public static GameManager instance;

    [Header("Save Manager Prefabs")]
    public GameObject SaveManagerPrefab;

    [Header("Player Prefabs")]
    public GameObject PlayerPrefab;

    [Header("Respawn Point Of Player")]
    public GameObject respawnPlayerPointObject;
    public Vector3 respawnPoint;
    
    [Header("Active Player (Get Automatic While Playing)")]
    public GameObject activePlayer;

    [Header("Active SaveManager (Get Automatic While Playing)")]
    public GameObject SaveManager;

    [Header("Timer")]
    public float timer;
    public float timerdefault;

    [Header("Journals")]
    public bool[] journalEntry;
    
    [Header("JournalsList")]
    public JournalsList JournalsList;
   
    [Header("Automatic Get From JournalsList")]
    public List<GameObject> JournalList;

    [Header("Boxes")]
    public List<GameObject> Boxes;
    [SerializeField]private Vector3[] BoxesPos;

    [Header("In Game UI")]
    public TextMeshProUGUI timerTxt;
    public GameObject buttonAds;
    public GameObject PausePanel;
    public GameObject JournalPanel;
    public GameObject CutscenePanel;
    public GameObject DimmerDialog;
    public GameObject GameOverPanel;

    [Header("Animation Before Game Over")]
    public GameObject Runtuh;

    private bool reset;

    [Header("URP Lighting")]
    [SerializeField] bool enableURPLighting;
    public Material AllSpriteMaterial;

    private PointProgress pointProgress;

    private void Awake()
    {
        instance = this;
    }

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

        if(respawnPlayerPointObject != null)
        {
            if (!DataSaveManager.instance.HasGameData())
            {
                respawnPoint = respawnPlayerPointObject.transform.position;
            }else 
            if(SaveManager == null)
            {
                respawnPoint = respawnPlayerPointObject.transform.position;
            }
        }

        timer = timerdefault;

        reset = false;

        Time.timeScale = 1;

        JournalTextFinder();
        
        if (DataSaveManager.instance != null)
        {
            DataSaveManager.instance.LoadGame();
        }

        SetBoxesPos();

        if (DataSaveManager.instance != null)
        {
            DataSaveManager.instance.SaveGame();
        }

        if(enableURPLighting && AllSpriteMaterial != null)
        {
            foreach(SpriteRenderer sr in Resources.FindObjectsOfTypeAll(typeof(SpriteRenderer)))
            {
                if(sr.gameObject.name != "Player")
                {
                    sr.material = AllSpriteMaterial;
                }
            }
        }

        pointProgress = FindObjectOfType<PointProgress>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) == true)
        {
            PauseGame();
        }

        if(timer > 0)
        {
            Runtuh.SetActive(false);
        }

        if (activePlayer.GetComponent<Rigidbody2D>().velocity.y < -12)
        {
            GameOver();
        }

        if (GameOverPanel.activeInHierarchy)
        {
            timerTxt.text = "00:00";

            PlayerDisabled();
        }

        if (timer > 0f && CutscenePanel != null)
        {
            
            if (DimmerDialog.gameObject.activeInHierarchy == true || CutscenePanel.gameObject.activeInHierarchy == true)
            {
                timerDisable();
            }
            else
            {
                timerEnable();
            }
            float minutes = Mathf.FloorToInt(timer / 60);
            float seconds = Mathf.FloorToInt(timer % 60);
            timerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        }else
        {

            if (DimmerDialog.gameObject.activeInHierarchy == true)
            {
                timerDisable();
            }
            else
            {
                timerEnable();
            }
            float minutes = Mathf.FloorToInt(timer / 60);
            float seconds = Mathf.FloorToInt(timer % 60);
            timerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        }

        if (timer <= 1f)
        {
            if(buttonAds != null)
                buttonAds.SetActive(true);
        }

        if (timer <= 0f)
        {
            if (buttonAds != null)
                buttonAds.SetActive(true);

            timerTxt.text = "00:00";

            Runtuh.gameObject.SetActive(true);
            
            if (Runtuh.gameObject.activeInHierarchy)
            {
                if (FindObjectOfType<CameraShake>() != null)
                {
                    FindObjectOfType<CameraShake>().doShake = true;
                }
            }
            
            if(timer <= -7f)
            {
                GameOver();
            }
            
            if(GameOverPanel.activeInHierarchy)
            {
                if (FindObjectOfType<CameraShake>() != null)
                {
                    FindObjectOfType<CameraShake>().doShake = false;
                }

                PlayerDisabled();
            }
        }
        journalChecker();
    }

    public void AddTimer(float aTime)
    {
        timer += aTime;
        Time.timeScale = 1;
        PlayerEnabled();
        GameOverPanel.SetActive(false);
        if (buttonAds != null) 
            buttonAds.SetActive(false);
        print("timer + " + timer);
    }

    public void OpenJournal()
    {
        PlayerDisabled();

        JournalPanel.SetActive(true);

        Time.timeScale = 0;
    }
    public void journalChecker()
    {
        for (int i = 0; i < journalEntry.Length; i++)
        {
            if (journalEntry[i])
            {
                JournalList[i].SetActive(true);
            }
            else
            {
                JournalList[i].SetActive(false);
            }
        }
    }

    public void JournalTextFinder()
    {
        if(JournalsList != null)
        {
            JournalList = JournalsList.journalTexts;
        }   
    }

    private void SetBoxesPos()
    {
        if(BoxesPos.Length != 0)
        {
            for(int i = 0; i < BoxesPos.Length; i++)
            {
                Boxes[i].transform.position = BoxesPos[i];
            }
        }
    }

    private Vector3[] GetBoxPos()
    {
        BoxesPos = new Vector3[Boxes.Count];
        for (int i = 0; i < BoxesPos.Length; i++)
        {
            BoxesPos[i] = Boxes[i].transform.position;
        }
        return BoxesPos;
    }

    public void CloseJournal()
    {
        PlayerEnabled();

        JournalPanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void PauseGame()
    {
        if(FindObjectOfType<DialogManager>() != null)
        {
            FindObjectOfType<DialogManager>().OnGamePause();
        }

        PlayerDisabled();

        PausePanel.SetActive(true);

        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        PlayerEnabled();

        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void startgame()
    {
        PlayerEnabled();

        if(CutscenePanel != null)
        {
            CutscenePanel.SetActive(false);
        }

        Time.timeScale = 1;
    }

    public void Startbtn()
    {
        if (FindObjectOfType<MenuSetting>() != null)
        {
            FindObjectOfType<MenuSetting>().FadeMusicOff();
        }

        Invoke("Restart", 2);
    }

    public void Restart()
    {
        PlayerEnabled();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        timer = timerdefault;

        UnaddJournalEntry();

        if (FindObjectOfType<LevelSystem>() != null)
        {
            FindObjectOfType<LevelSystem>().ResetProgress();
        }

        if (FindObjectOfType<ObjekCount>() != null)
        {
            FindObjectOfType<ObjekCount>().ResetobtainedCount();
        }

        if (Box_PresurePlate.instance != null)
        {
            Box_PresurePlate.instance.Pressed = false;
        }

        if (pointProgress != null)
        {
            pointProgress.decreasePoint(5);
        }

        ResetLevel();

        startgame();
    }

    public void UnaddJournalEntry()
    {
        foreach (DialogTrigger dt in Resources.FindObjectsOfTypeAll(typeof(DialogTrigger)))
        {
            if(dt.addJournalEntry == true)
            {
                UnsetJurnalEntry(dt.jurnalPoint);
            }
        }
    }

    public void ResetLevel()
    {
        if (activePlayer)
        { 
            reset = true;
            if(DataSaveManager.instance != null)
            {
                DataSaveManager.instance.SaveGame();
                DataSaveManager.instance.NewGame();
            }   
        }
    }

    public void setTimer()
    {
        timer = timerdefault;
    }

    public void ToMainMenu()
    {
        Time.timeScale = 1;

        if (DataSaveManager.instance != null)
        {
            DataSaveManager.instance.SaveGame();
        }

        if (GameObject.Find("FadeOut") != null)
        {
            GameObject.Find("FadeOut").SetActive(true);
        }

        if (FindObjectOfType<MenuSetting>() != null)
        {
            FindObjectOfType<MenuSetting>().FadeMusicOff();
        }
           
        Invoke("ToMainMenuScene", 1);
    }

    public void LoadData(GameDataSave data)
    {
        if(data != null)
        {
            journalEntry = data.JurnalItem_EnabledSaved;
            
            this.timer = data.timerSaved;
            this.respawnPoint = data.playerPositionSaved;

            if(data.boxPressurePlate_PosSaved.Length != 0)
                BoxesPos = data.boxPressurePlate_PosSaved;
        } 
    }

    public void SaveData(ref GameDataSave data)
    {
        Scene scene = SceneManager.GetActiveScene();

        data.sceneNumberSaved = scene.buildIndex;

        Debug.Log("Scene Number = " + scene.buildIndex);

        data.timerSaved = this.timer;

        data.JurnalItem_EnabledSaved = this.journalEntry;

        if (PlayerPrefs.HasKey("playerName"))
        {
            data.playerName = PlayerPrefs.GetString("playerName");
        }

        if (activePlayer)
        {
            if (reset)
            {
                data.isOn_PressurePlateSaved = false;
                data.playerPositionSaved = Vector3.zero;
                data.isFollowing_KeySaved = false;
                data.doorOpenSaved = false;
                data.cameraPos = Vector3.zero;
                data.boxPressurePlate_PosSaved = new Vector3[0];
            }
            else
            {
                data.JurnalItem_EnabledSaved = journalEntry;
                data.playerPositionSaved = this.activePlayer.transform.position;

                if (PresurePlate.instance != null)
                {
                    data.isOn_PressurePlateSaved = PresurePlate.instance.isOn;
                }
                    
                if (Key.instance != null)
                {
                    data.isFollowing_KeySaved = Key.instance.isFollowing;
                }
                    
                if (Door.instance != null)
                {
                    data.doorOpenSaved = Door.instance.doorOpen;
                }

                if (Camera_Following.instance != null)
                {
                    data.cameraPos = Camera_Following.instance.getPosCam();
                }

                data.boxPressurePlate_PosSaved = GetBoxPos();
            } 
        }
    }

    public void setJurnalEntry(int index){
        journalEntry[index] = true;
        
        if(pointProgress != null)
        {
            pointProgress.increasePoint(15);
        }
    }

    public void UnsetJurnalEntry(int index)
    {
        journalEntry[index] = false;
    }

    public void PlayerDisabled()
    {
        activePlayer.gameObject.GetComponent<PlayerControllers>().setPlayerNotMove = true;
    }

    public void PlayerEnabled()
    {
        activePlayer.gameObject.GetComponent<PlayerControllers>().setPlayerNotMove = false;
    }

    public void timerEnable()
    {
        timer -= Time.deltaTime;
    }

    public void timerDisable()
    {
        timer -= 0;
    }

    public void GameOver()
    {
        timerTxt.text = "00:00";
        GameOverPanel.SetActive(true);
    }

    public void TimeScaleDisabled()
    {
        Time.timeScale = 0;
    }

    public void TimeScaleEnabled()
    {
        Time.timeScale = 1;
    }

    private void ToMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}