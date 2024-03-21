using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    public SaveData savedata;

    private List<InterfaceSave> savedataObjects;

    private DataHandler dataHandler;

    public static SaveManager instance { get; private set; }


    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found more than one Data Persistent Manager in the scene.");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);

        this.dataHandler = new DataHandler(Application.persistentDataPath, fileName);
    }


    public void CreateNewSaveData()
    {
        this.savedata = new SaveData();
    }

    public void LoadGame()
    {
        this.savedata = dataHandler.Handler_LoadData();

        if (this.savedata == null)
        {
            Debug.Log("No data was found. A New Game needs to be started before data can be loaded");
        }

        foreach (InterfaceSave savedataObj in savedataObjects)
        {
            savedataObj.ItfLoadData(savedata);
        }

        Debug.Log("Game Loaded");
    }

    public void SaveGame()
    {
        if (this.savedata == null)
        {
            Debug.Log("No data was found. A New Game needs to be started before data can be loaded");
            CreateNewSaveData();
        }

        foreach (InterfaceSave savedataObj in savedataObjects)
        {
            savedataObj.ItfSaveData(ref savedata);
        }

        dataHandler.Handler_SaveData(savedata);

        Debug.Log("Game Saved");
    }

    public void DeleteSaveData()
    {
        dataHandler.Handler_DeleteData();
        Debug.Log("Save Data Has Removed");
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnScene Loaded Called");
        this.savedataObjects = FindAllsavedataObjects();
        LoadGame();
    }

    public void OnSceneUnLoaded(Scene scene)
    {
        Debug.Log("OnScene UnLoaded Called");
        //SaveGame();
    }

    private void OnApplicationQuit()
    {
        //SaveGame();
    }

    private List<InterfaceSave> FindAllsavedataObjects()
    {
        IEnumerable<InterfaceSave> savedataObjects = FindObjectsOfType<MonoBehaviour>().OfType<InterfaceSave>();
        return new List<InterfaceSave>(savedataObjects);
    }

    public bool HasSaveData()
    {
        return savedata != null;
    }
}