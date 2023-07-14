using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    public Quest quest;
    public bool addQuestEntry;
    public int QuestNumber;

    public void TriggerQuest()
    {
        FindObjectOfType<DialogManager_Quest>().StartQuestDialog(quest);

        if (addQuestEntry)
        {
            GameObject.FindObjectOfType<Game_Manager>().QuestEntry[QuestNumber] = true;
        }
    }
}
