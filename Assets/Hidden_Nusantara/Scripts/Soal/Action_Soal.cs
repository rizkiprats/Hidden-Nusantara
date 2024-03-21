using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class Action_Soal : MonoBehaviour
{
    [Header("Soal")]
    public Soal Soal;

    //[Header("ChoiceBox")]
    //public TMP_Text SoalText;
    //public Button[] ButtonChoice;

    [Header("Check All Result is True and Complete (Automatic) if Quiz Mode Is Squentials")]
    [SerializeField]private bool isComplete;

    [Header("Soal Complete Events")]
    [SerializeField] private UnityEvent TriggerComplete;
    [SerializeField] private UnityEvent TriggerUnComplete;

    [Header("Panel Hasil")]
    [SerializeField] private GameObject PanelBenar;
    [SerializeField] private GameObject PanelSalah;

    [Header("List ChoicesBox")]
    public List<GameObject> toRandom;
    [SerializeField] private List<GameObject> Randomizeobject;

    [Header("Randomize the Quiz")]
    public bool defineAsRandomize;

    [Header("Quiz Mode, Select One")]
    public bool defineAsRandomizeOnFalse;
    public bool defineAsSquentialsQuizMode;
    
    int startIndex = 0;
    [Header("Delay If Quiz Complete")]
    [SerializeField] private float Delay_EventComplete = 1f;

    int FaultAnsweringCount = 0;
    int RemainingChanceCount = 0;
    [Header("Fault Chance")]
    [SerializeField] private int MaxFault;
    [SerializeField] private UnityEvent TriggerMaxFault;
    [SerializeField] private TMP_Text ChanceRemainingText;
    [SerializeField] private int decreasePointOnFalse;
    [SerializeField] private int increasePointOnTrue;

    private PointProgress pointProgress;

    //private void Awake()
    //{
    //    RemainingChanceCount = MaxFault;

    //    if (defineAsRandomize)
    //    {
    //        Randomizeobject = RandomizeGameObjects(toRandom);

    //        if (Randomizeobject.Count != 0)
    //        {
    //            Randomizeobject[startIndex].SetActive(true);
    //        }
    //    }
    //    //RandomSoal();
    //}

    // Start is called before the first frame update
    void Start()
    {
        RemainingChanceCount = MaxFault;

        if (defineAsRandomize)
        {
            Randomizeobject = RandomizeGameObjects(toRandom);

            if (Randomizeobject.Count != 0)
            {
                Randomizeobject[startIndex].SetActive(true);
            }
        }
        //RandomSoal();

        pointProgress = FindObjectOfType<PointProgress>();
        increasePointOnTrue = 15;
        decreasePointOnFalse = 5;
    }

    private void Update()
    {
        isComplete = CheckAllAnswerTrue();
        ChanceRemainingText.text = "Tersisa " + RemainingChanceCount + " kesempatan untuk menjawab. " + 
            "Pointmu berkurang sebanyak "+decreasePointOnFalse * FaultAnsweringCount;
    }

    private void CheckEachAnswer(int soalNumber)
    {
        if (Soal.soal[soalNumber].jawaban == false)
        {
            FaultAnsweringCount += 1;
            RemainingChanceCount -= 1;

            if(pointProgress != null)
            {
                pointProgress.decreasePoint(decreasePointOnFalse * FaultAnsweringCount);
            }

            if (defineAsRandomizeOnFalse)
            {
                if (Randomizeobject.Count != 0)
                {
                    Randomizeobject = RandomizeGameObjects(toRandom);
                    
                    Randomizeobject[startIndex].SetActive(true);

                    if (Randomizeobject[startIndex].GetComponent<Action_ChoiceBox>() != null)
                    {
                        Randomizeobject[startIndex].GetComponent<Action_ChoiceBox>().RandomizeChoice();
                    }
                }
            }
            else if (defineAsSquentialsQuizMode)
            {
                if (Randomizeobject.Count != 0 && startIndex < (Randomizeobject.Count))
                {
                    Randomizeobject[startIndex].SetActive(true);
                }
            }
            else
            {
                if (toRandom.Count != 0)
                {
                    toRandom[soalNumber].SetActive(true);
                    if (toRandom[soalNumber].GetComponent<Action_ChoiceBox>() != null)
                    {
                        toRandom[soalNumber].GetComponent<Action_ChoiceBox>().RandomizeChoice();
                    }
                }
            }

            if (PanelSalah != null)
            {
                PanelSalah.SetActive(true);
            }

            Soal.soal[soalNumber].EventFalse.Invoke();
        }
        else
        {
            if (pointProgress != null)
            {
                pointProgress.increasePoint(increasePointOnTrue);
            }

            if (defineAsRandomizeOnFalse)
            {
                if (Randomizeobject.Count != 0)
                {
                    Randomizeobject[startIndex].SetActive(false);
                }
            }
            else if(defineAsSquentialsQuizMode)
            {
                if (Randomizeobject.Count != 0 && startIndex < (Randomizeobject.Count))
                {
                    Randomizeobject[startIndex].SetActive(false);
                    startIndex = +1;
                    Randomizeobject[startIndex].SetActive(true);
                }
                else
                {
                    Randomizeobject[startIndex].SetActive(false);
                }
            }else
            {
                if (toRandom.Count != 0)
                {
                    toRandom[soalNumber].SetActive(false);
                }
            }

            if (PanelBenar != null)
            {
                PanelBenar.SetActive(true);
            }

            Soal.soal[soalNumber].EventTrue.Invoke();
        }

        if(DataSaveManager.instance != null)
        {
            DataSaveManager.instance.SaveGame();
        }
    }

    private bool CheckAllAnswerTrue()
    {
        for (int i = 0; i < Soal.soal.Length; i++)
        {
            if (Soal.soal[i].jawaban == false)
            {
                return false;
            }
        }
        return true;
    }

    public void voidCheckComplete() {
        
        if (isComplete)
        {
            Invoke("InvokeTriggerComplete", Delay_EventComplete);
        }
        else
        {
            Invoke("InvokeTriggerUnComplete", Delay_EventComplete);
        }
    }

    public void InvokeTriggerComplete()
    {
        TriggerComplete.Invoke();
    }

    public void InvokeTriggerUnComplete()
    {
        TriggerUnComplete.Invoke();
    }

    public void CheckJawaban(int soalNumber)
    {
        CheckEachAnswer(soalNumber);
        
        Invoke("voidCheckComplete", 2);

        if (FaultAnsweringCount == MaxFault)
        {
            TriggerMaxFault.Invoke();
            FindObjectOfType<GameManager>().GameOver();
        }
    }

    public void SetJawaban(int NomorSoal, bool condition)
    {
        Soal.soal[NomorSoal].jawaban = condition;
    }

    private List<GameObject> RandomizeGameObjects(List<GameObject> gameObjects)
    {
        List<GameObject> Objects = gameObjects;
        List<GameObject> RandomizeObjects = new List<GameObject>();
        List<int> RandomIndex = new List<int>();

        for (int i = 0; i < Objects.Count; i++)
        {
            int randomIndex = Random.Range(0, Objects.Count);
            while (RandomIndex.Contains(randomIndex))
            {
                randomIndex = Random.Range(0, Objects.Count);
            }
            RandomizeObjects.Add(Objects[randomIndex]);
            RandomIndex.Add(randomIndex);
        }

        return RandomizeObjects;
    }

    //public void RandomSoal()
    //{
    //    if (Soal.soal.Length != 0)
    //    {
    //        int randomSoal = Random.Range(0, Soal.soal.Length);
    //        SoalText.text = Soal.soal[randomSoal].PertanyaanSentence;
    //        List<int> randomChoices = new List<int>();
    //        for (int i = 0; i < ButtonChoice.Length; i++)
    //        {
    //            int randomChoicesIndex = Random.Range(0, Soal.soal[randomSoal].choiceBoxes.Length);
    //            while (randomChoices.Contains(randomChoicesIndex))
    //            {
    //                randomChoicesIndex = Random.Range(0, Soal.soal[randomSoal].choiceBoxes.Length);
    //            }
    //            if (ButtonChoice[i].GetComponentInChildren<TMP_Text>() != null)
    //            {
    //                ButtonChoice[i].GetComponentInChildren<TMP_Text>().text = Soal.soal[randomSoal].choiceBoxes[randomChoicesIndex].JawabanSentence;
    //            }
    //            if (ButtonChoice[i].GetComponent<Add_SoalEntry>() != null)
    //            {
    //                ButtonChoice[i].GetComponent<Add_SoalEntry>().setJawaban(Soal.soal[randomSoal].choiceBoxes[randomChoicesIndex].addJawabanisBenar, randomSoal);
    //                //ButtonChoice[i].GetComponent<Add_SoalEntry>().SoalNumber = randomSoal;
    //            }
    //            randomChoices.Add(randomChoicesIndex);
    //        }
    //    }
    //}
}