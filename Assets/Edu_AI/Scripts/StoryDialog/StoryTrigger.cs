using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public Story story;
    
    public void TriggerStory()
    {
        FindObjectOfType<DialogManager_Story>().StartStoryDialog(story);
    }
}
