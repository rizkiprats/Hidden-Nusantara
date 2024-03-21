using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestTrigger : MonoBehaviour
{
    public Quest quest;
    public bool addQuestEntry;
    public int QuestNumber;

    //public GameObject nextObject;

    [SerializeField] private UnityEvent TriggerEventQuest;

    public void TriggerQuest()
    {
        FindObjectOfType<DialogManager_Quest>().StartQuestDialog(quest);

        InvokeTriggerQuest();
  
        if (addQuestEntry)
        {
            GameObject.FindObjectOfType<Game_Manager>().QuestEntry[QuestNumber] = true;
        }
    }

    public void AddQuest()
    {
        if (addQuestEntry)
        {
            GameObject.FindObjectOfType<Game_Manager>().QuestEntry[QuestNumber] = true;
        }
    }

    public void InvokeTriggerQuest()
    {
        TriggerEventQuest.Invoke();
    }
}