using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialog;
    public bool addJournalEntry;
    public int jurnalPoint;

    public void TriggerDialog()
    {
        FindObjectOfType<DialogManager>().StartDialog(dialog);
        if (addJournalEntry){
            GameManager.instance.journalEntry[jurnalPoint] = true;
        }
        DataSaveManager.instance.SaveGame();
    }
}
