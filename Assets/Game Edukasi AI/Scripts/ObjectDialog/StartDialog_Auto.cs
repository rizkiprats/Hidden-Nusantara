using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialog_Auto : MonoBehaviour
{
    public void StartDialog()
    {
        if(this.GetComponent<ObjectDialogTrigger>() != null)
        {
            this.GetComponent<ObjectDialogTrigger>().TriggerObjectDialog();
        }

        if(this.GetComponent<DialogTrigger>() != null)
        {
            this.GetComponent<DialogTrigger>().TriggerDialog();
        }

        if(this.gameObject.GetComponent<DialogTrigger_Random>() != null)
        {
            this.GetComponent<DialogTrigger_Random>().TriggerDialog();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartDialog();
            if(collision.gameObject.GetComponent<PlayerControllers>() != null)
            {
                collision.gameObject.GetComponent<PlayerControllers>().PlayernotMove();
            }
        }
    }
}