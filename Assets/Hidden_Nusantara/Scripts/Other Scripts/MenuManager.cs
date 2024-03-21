using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class MenuManager : MonoBehaviour, InterfaceDataSave
{
    public static MenuManager instance;

    [SerializeField] Button buttonContinue;

    [SerializeField] private UnityEvent TriggerHasSaveData;
    [SerializeField] private UnityEvent TriggerHasNotSaveData;

    public int startscenenumber;
    private int continuescenenumber;

    [Header("Player Name")]
    [SerializeField]private string playerName;
    [SerializeField] TMP_Text playerNameText;

    [Header("Input Player Name Layout")]
    [SerializeField] TMP_InputField playernameInput;
    [SerializeField] Button setPlayerName;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        if (DataSaveManager.instance != null)
        {
            if (!DataSaveManager.instance.HasGameData())
            {
                buttonContinue.interactable = false;
                TriggerHasNotSaveData.Invoke();
            }
            else
            {
                TriggerHasSaveData.Invoke();
            }
        }

        if(playerNameText != null && playerName != "Raden")
        {
            playerNameText.text = playerName;
        }
        else
        {
            playerNameText.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) == true)
        {
            QuitGame();
        }

        if(playernameInput.text == "")
        {
            setPlayerName.interactable = false;
        }
        else
        {
            setPlayerName.interactable = true;
        }
    }


    public void StartGame()
    {

        if (FindObjectOfType<DataSaveManager>() != null)
        {
            FindObjectOfType<DataSaveManager>().DeleteSaveGame();
            FindObjectOfType<DataSaveManager>().NewGame();
        }

        SetNamePlayer();
        
        //DataSaveManager.instance.DeleteSaveGame();
        //DataSaveManager.instance.NewGame();
        
        Invoke("StartFirstLevelScene", 2);
    }

    public void SetNamePlayer()
    {
        PlayerPrefs.SetString("playerName", playernameInput.text);
    }

    public void SetNamePlayer(string name)
    {
        playernameInput.text = name;
        PlayerPrefs.SetString("playerName", playernameInput.text);
    }

    public void StartFirstLevelScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(startscenenumber); 
    }

    public void StartContinueLevelScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(continuescenenumber);
    }

    public void LoadGame() 
    {
        Invoke("StartContinueLevelScene", 2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadData(GameDataSave data)
    {
        if(DataSaveManager.instance != null)
        {
            if (!DataSaveManager.instance.HasGameData())
            {
                continuescenenumber = startscenenumber;
            }
            else
            {
                continuescenenumber = data.sceneNumberSaved;
                playerName = data.playerName;
            }
        }
    }
    public void SaveData(ref GameDataSave data)
    {
        data.sceneNumberSaved = continuescenenumber;
    }
}