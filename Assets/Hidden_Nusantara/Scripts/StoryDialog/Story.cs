using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Story
{
    public string name;

    [TextArea(2, 10)]
    public string[] sentences;

    public AudioClip[] StoryAudioClipsentences;

    [Header("Story Events")]
    public UnityEvent startStoryEvent;
    public UnityEvent endStoryEvent;
}