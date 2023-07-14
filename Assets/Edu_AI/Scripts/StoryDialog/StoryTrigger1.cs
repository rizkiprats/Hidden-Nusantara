using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTrigger1 : MonoBehaviour
{
    // Start is called before the first frame update
    public Story story;
    
    public void TriggerStory()
    {
        this.gameObject.GetComponent<DialogManager_Story1>().StartStoryDialog(story);
    }
}
