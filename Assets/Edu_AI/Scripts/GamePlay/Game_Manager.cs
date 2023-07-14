using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject activePlayer;

    public bool[] QuestEntry;

    public GameObject respawnPlayerObject;

    public Vector3 respawnPoint;

    public GameObject PlayerPrefab;

    public GameObject PausePanel;
    public GameObject TugasPanel;

    public GameObject[] QuestTexts;

    void Start()
    {
        if(activePlayer == null)
        {

        }

        if(respawnPlayerObject != null)
        {
            respawnPoint = respawnPlayerObject.transform.position;
        }

        if (GameObject.Find("Player") == null)
        {
            activePlayer = Instantiate(PlayerPrefab, respawnPoint, Quaternion.identity);
            activePlayer.name = "Player";
            Debug.Log("Player Spawned");
        }

        if(PausePanel != null)
        {
            PausePanel.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
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
                QuestTexts[i].SetActive(true);
            }
        }
    }

    public void TutupTugas()
    {
        TugasPanel.SetActive(false);
    }
}
