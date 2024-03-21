using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_Quest : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            this.gameObject.GetComponent<QuestTrigger>().TriggerQuest();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            GameObject.FindObjectOfType<DialogManager_Story>().isStoryEnd = false;
            GameObject.FindObjectOfType<DialogManager_Quest>().isquestEnd = false;
        }
    }

    public void ResetQuest()
    {
        GameObject.FindObjectOfType<DialogManager_Story>().isStoryEnd = false;
        GameObject.FindObjectOfType<DialogManager_Quest>().isquestEnd = false;
    }
}