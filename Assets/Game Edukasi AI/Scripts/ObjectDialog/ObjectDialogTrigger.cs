using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectDialogTrigger : MonoBehaviour
{
    public ObjectDialog objectDialog;

    public bool addQuestEntry;
    public int QuestNumber;

    [SerializeField] private UnityEvent TriggerEventObject;

    public void TriggerObjectDialog()
    {
        FindObjectOfType<DialogManager_Object>().StartObjectDialog(objectDialog);
 
        InvokeTriggerObject();

        if(this.gameObject.GetComponent<AddJurnalEntry>() != null)
        {
            this.gameObject.GetComponent<AddJurnalEntry>().AddJurnal(); 
        }

        if(this.gameObject.GetComponent<AddMateriEntry>() != null)
        {
            this.gameObject.GetComponent<AddMateriEntry>().AddMateri();
        }

        if (addQuestEntry)
        {
            GameObject.FindObjectOfType<Game_Manager>().QuestEntry[QuestNumber] = true;
        }
    }

    public void InvokeTriggerObject()
    {
        TriggerEventObject.Invoke();
    }
}