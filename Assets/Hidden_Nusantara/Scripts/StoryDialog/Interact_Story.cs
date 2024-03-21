using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_Story : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            this.gameObject.GetComponent<StoryTrigger>().TriggerStory();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            if(FindObjectOfType<DialogManager_Story>() != null)
            {
                GameObject.FindObjectOfType<DialogManager_Story>().isStoryEnd = false;
            }
            if(FindObjectOfType<DialogManager_Quest>() != null)
            {
                GameObject.FindObjectOfType<DialogManager_Quest>().isquestEnd = false;
            }
        }
    }

    public void ResetStory()
    {
        if (FindObjectOfType<DialogManager_Story>() != null)
        {
            GameObject.FindObjectOfType<DialogManager_Story>().isStoryEnd = false;
        }
        if (FindObjectOfType<DialogManager_Quest>() != null)
        {
            GameObject.FindObjectOfType<DialogManager_Quest>().isquestEnd = false;
        }
    }
}
