using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PointProgress : MonoBehaviour, InterfaceDataSave
{
    [Header("Player Name & Point")]
    [SerializeField] private int playerPoint;
    [SerializeField] private string playerName;
    [SerializeField] TMP_Text playerPointText;

    [Header("Flag")]
    [SerializeField] GameObject Flagpoint;
    [SerializeField] Sprite GoldFlag;
    [SerializeField] Sprite SilverFlag;
    [SerializeField] Sprite BronzeFlag;
    [SerializeField] Sprite defaultFlag;

    [Header("FlagpointConfig")]
    [SerializeField] int goldflagpoint;
    [SerializeField] int silverflagpoint;
    [SerializeField] int bronzeflagpoint;

    // Start is called before the first frame update
    void Start()
    {
        if (playerPointText != null)
        {
            playerPointText.text = playerPoint.ToString();
        }

        checkplayerPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerPointText != null)
        {
            playerPointText.text = playerPoint.ToString();
        }

        checkplayerPoint();
    }

    private void checkplayerPoint()
    {
        if (playerPoint >= bronzeflagpoint)
        {
            Flagpoint.gameObject.SetActive(true);
            Flagpoint.gameObject.GetComponent<Image>().enabled = true;
            Flagpoint.GetComponent<Image>().sprite = BronzeFlag;
        }
        if (playerPoint >= silverflagpoint)
        {
            Flagpoint.gameObject.SetActive(true);
            Flagpoint.gameObject.GetComponent<Image>().enabled = true;
            Flagpoint.GetComponent<Image>().sprite = SilverFlag;
        }
        if (playerPoint >= goldflagpoint)
        {
            Flagpoint.gameObject.SetActive(true);
            Flagpoint.gameObject.GetComponent<Image>().enabled = true;
            Flagpoint.GetComponent<Image>().sprite = GoldFlag;
        }
        if (playerPoint < bronzeflagpoint)
        {
            Flagpoint.gameObject.GetComponent<Image>().enabled = true;
            Flagpoint.GetComponent<Image>().sprite = defaultFlag;
        }
    }

    public void increasePoint(int point)
    {
        playerPoint += point;
        
        if(DataSaveManager.instance != null)
        {
            DataSaveManager.instance.SaveGame();
        }
    }

    public void decreasePoint(int point)
    {
        playerPoint -= point;
        
        if(DataSaveManager.instance != null)
        {
            DataSaveManager.instance.SaveGame();
        }
    }
    
    public int getPlayerPoint()
    {
        return playerPoint;
    }

    public string getPlayerName()
    {
        return playerName;
    }

    public void LoadData(GameDataSave data)
    {
        if(data != null)
        {
            playerPoint = data.playerPoint;
            playerName = data.playerName;
        }
    }

    public void SaveData(ref GameDataSave data)
    {
        data.playerPoint = playerPoint;
    }
}