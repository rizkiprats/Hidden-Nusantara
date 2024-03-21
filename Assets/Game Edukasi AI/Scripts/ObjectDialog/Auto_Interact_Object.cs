using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auto_Interact_Object : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(this.gameObject.GetComponent<ObjectDialogTrigger>() != null)
            {
                this.gameObject.GetComponent<ObjectDialogTrigger>().TriggerObjectDialog();
            }
        }
    }
}
