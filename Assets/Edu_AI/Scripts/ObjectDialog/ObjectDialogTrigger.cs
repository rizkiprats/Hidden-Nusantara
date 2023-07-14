using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDialogTrigger : MonoBehaviour
{
    public ObjectDialog objectDialog;


    public void TriggerObjectDialog()
    {
        FindObjectOfType<DialogManager_Object>().StartObjectDialog(objectDialog);
    }
}
