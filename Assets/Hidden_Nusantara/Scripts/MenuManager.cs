using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour, InterfaceDataSave
{
    public static MenuManager instance;

    
    [SerializeField] Button buttonLoad;


    public int scenenumber ;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        if (!DataSaveManager.instance.HasGameData())
        {
            buttonLoad.interactable = false;
        }
    

}

    // Update is called once per frame
    void Update(){

    }

    public void setButton()
    {
        buttonLoad.interactable = false;
    }

    public void Startbtn()
    {
        DataSaveManager.instance.NewGame();
        SceneManager.LoadScene(3);
    }

    public void LoadGame() 
    {
        
        SceneManager.LoadScene(scenenumber);
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadData(GameDataSave data)
    {
        if (!DataSaveManager.instance.HasGameData())
        {
            scenenumber = 1;
        }
        else
        {
            scenenumber = data.sceneNumber;
        }
       

       
    }
    public void SaveData(ref GameDataSave data)
    {
        data.sceneNumber = scenenumber;

    }

}
