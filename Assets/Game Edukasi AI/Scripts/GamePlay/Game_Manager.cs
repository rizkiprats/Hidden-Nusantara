using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour, InterfaceSave
{
    public static Game_Manager instance;

    public GameObject activePlayer;
    //public GameObject activeLevel;

    public GameObject respawnPlayerObject;
    //public GameObject respawnLevel;

    public Vector3 respawnPoint;

    public GameObject PlayerPrefab;
    //public GameObject LevelPrefab;

    public GameObject PausePanel;
    public GameObject TugasPanel;
    public GameObject JurnalPanel;

    public bool[] QuestEntry;
    public GameObject[] QuestTexts;

    public bool[] JurnalEntry;
    public GameObject[] JurnalButton;

    [Header("ProgressGame")]
    public QuestProgress questProgressLevel1;
    public ObjectData questDataLv1;
    public QuestProgress questProgressLevel2;
    public ObjectData questDataLv2;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        if(respawnPlayerObject != null)
        {
            if(!SaveManager.instance.HasSaveData())  
                respawnPoint = respawnPlayerObject.transform.position;
        }

        if (GameObject.Find("Player") == null)
        {
            activePlayer = Instantiate(PlayerPrefab, respawnPoint, Quaternion.identity);
            activePlayer.name = "Player";
            Debug.Log("Player Spawned");
        }

        if (PausePanel != null)
        {
            PausePanel.SetActive(false);
        }

        QuestSetter();
    }

    // Update is called once per frame
    void Update()
    {
        QuestGetter();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
    }

    public void LihatTugas()
    {
        TugasPanel.SetActive(true);

        for(int i=0; i < QuestEntry.Length; i++)
        {
            if (QuestEntry[i])
            {
                if(QuestTexts[i] != null)
                    QuestTexts[i].SetActive(true);
            }
        }
    }

    public void TutupTugas()
    {
        TugasPanel.SetActive(false);
    }

    public void LihatJurnal()
    {
        JurnalPanel.SetActive(true);

        for (int i = 0; i < JurnalEntry.Length; i++)
        {
            if (JurnalEntry[i])
            {
                if(JurnalButton[i] != null)
                    JurnalButton[i].SetActive(true);
            }
        }
    }

    public void TutupJurnal()
    {
        JurnalPanel.SetActive(false);
    }

    public void QuestGetter()
    {
        questDataLv1.ObjectDataNumber = new ObjectData.ObjectProgress[questProgressLevel1.GetLength()];

        for (int i = 0; i < questProgressLevel1.GetLength(); i++)
        { 
            questDataLv1.ObjectDataNumber[i].QuestObject = new bool[questProgressLevel1.QuestDataNumber[i].QuestObject.Length];

            for (int j = 0; j < questProgressLevel1.QuestDataNumber[i].QuestObject.Length; j++)
            {
                if (questProgressLevel1.QuestDataNumber[i].QuestObject[j] != null)
                {
                    if (questProgressLevel1.QuestDataNumber[i].QuestObject[j].activeSelf)
                    {
                        questDataLv1.ObjectDataNumber[i].QuestObject[j] = true;
                    }
                    else
                    {
                        questDataLv1.ObjectDataNumber[i].QuestObject[j] = false;
                    }
                }
            }
        }

        questDataLv2.ObjectDataNumber = new ObjectData.ObjectProgress[questProgressLevel2.GetLength()];

        for (int k = 0; k < questProgressLevel2.GetLength(); k++)
        {  
            questDataLv2.ObjectDataNumber[k].QuestObject = new bool[questProgressLevel2.QuestDataNumber[k].QuestObject.Length];

            for (int l = 0; l < questProgressLevel2.QuestDataNumber[k].QuestObject.Length; l++)
            {
                if (questProgressLevel2.QuestDataNumber[k].QuestObject[l] != null)
                {
                    if (questProgressLevel2.QuestDataNumber[k].QuestObject[l].activeSelf)
                    {
                        questDataLv2.ObjectDataNumber[k].QuestObject[l] = true;
                    }
                    else
                    {
                        questDataLv2.ObjectDataNumber[k].QuestObject[l] = false;
                    }
                }
            }
        }
    }

    public void QuestSetter()
    {
        for (int i = 0; i < questDataLv1.GetLength(); i++)
        {
            for (int j = 0; j < questDataLv1.ObjectDataNumber[i].QuestObject.Length; j++)
            {
                if (questDataLv1.ObjectDataNumber[i].QuestObject[j])
                {
                    if (questProgressLevel1.QuestDataNumber[i].QuestObject[j] != null)
                        questProgressLevel1.QuestDataNumber[i].QuestObject[j].SetActive(true);
                }
                else
                {
                    if (questProgressLevel1.QuestDataNumber[i].QuestObject[j] != null)
                        questProgressLevel1.QuestDataNumber[i].QuestObject[j].SetActive(false);
                }
            }
        }

        for (int k = 0; k < questDataLv2.GetLength(); k++)
        {
            for (int l = 0; l < questDataLv2.ObjectDataNumber[k].QuestObject.Length; l++)
            {
                if (questDataLv2.ObjectDataNumber[k].QuestObject[l])
                {
                    if (questProgressLevel2.QuestDataNumber[k].QuestObject[l] != null)
                        questProgressLevel2.QuestDataNumber[k].QuestObject[l].SetActive(true);

                }
                else
                {
                    if (questProgressLevel2.QuestDataNumber[k].QuestObject[l] != null)
                        questProgressLevel2.QuestDataNumber[k].QuestObject[l].SetActive(false);
                }
            }
        }
    }

    public void ItfLoadData(SaveData data)
    {
        if (data != null)
            respawnPoint = data.playerPosition;
        questDataLv1 = data.questProgressLevel1;
        questDataLv2 = data.questProgressLevel2;
    }

    public void ItfSaveData(ref SaveData data)
    {
        Scene scene = SceneManager.GetActiveScene();
        data.SavedIndexScene = scene.buildIndex;
        
        if(activePlayer != null)
            data.playerPosition = activePlayer.transform.position;
        data.questProgressLevel1 = questDataLv1;
        data.questProgressLevel2 = questDataLv2;
    }
}