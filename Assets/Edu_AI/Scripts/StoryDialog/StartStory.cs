using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStory : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<StoryTrigger1>().TriggerStory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
