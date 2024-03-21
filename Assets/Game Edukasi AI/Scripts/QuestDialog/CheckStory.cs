using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckStory : MonoBehaviour
{
    [Header("Define While Story has Ended (Automatic)")]
    public bool isstoryEnd;

    [Header("Story End Events")]
    [SerializeField] private UnityEvent TriggerEventStoryEnd;
    
    private bool isquestEnd;
    private UnityEvent TriggerEventQuestEnd;

    private GameObject QuestTrigger;

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<DialogManager_Story>() != null)
        {
            isstoryEnd = GameObject.FindObjectOfType<DialogManager_Story>().isStoryEnd;
        }
        if (FindObjectOfType<DialogManager_Quest>() != null)
        {
            isquestEnd = GameObject.FindObjectOfType<DialogManager_Quest>().isquestEnd;
        }

        if (isstoryEnd)
        {
            ActivateQuest();
        }

        if (isquestEnd)
        {
            DisableQuest();
        }
    }

    public void ActivateQuest()
    {
        if (QuestTrigger != null)
        {
            QuestTrigger.gameObject.SetActive(true);
        }
        
        TriggerEventStoryEnd.Invoke();

        if (FindObjectOfType<DialogManager_Story>() != null)
        {
            GameObject.FindObjectOfType<DialogManager_Story>().isStoryEnd = false;
        }
    }

    public void DisableQuest()
    {
        if (QuestTrigger != null)
        {
            QuestTrigger.gameObject.SetActive(false);
        }
        
        TriggerEventQuestEnd.Invoke();

        if (FindObjectOfType<DialogManager_Quest>() != null)
        {
            GameObject.FindObjectOfType<DialogManager_Quest>().isquestEnd = false;
        }
    }
}