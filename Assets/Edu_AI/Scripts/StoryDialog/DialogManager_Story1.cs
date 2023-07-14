using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager_Story1 : MonoBehaviour
{
    // Start is called before the first frame update
    public Text StoryDialog;

    private Queue<string> storysentences;






    void Start()
    {
        storysentences = new Queue<string>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartStoryDialog(Story dialog)
    {

        Debug.Log("Start Conversation from " + dialog.name);

        storysentences.Clear();

        foreach(string sentence in dialog.sentences)
        {
            storysentences.Enqueue(sentence);
        }

        DisplayNextStory();
    }

    public void DisplayNextStory()
    {

        if(storysentences.Count == 0)
        {
            EndStory();
            return;
        }

        string sentence = storysentences.Dequeue();

        StartCoroutine(TypeStory(sentence));
    }

    IEnumerator TypeStory(string sentence)
    {
        StoryDialog.text = "";

        foreach(char letter in sentence.ToCharArray())
        {
            StoryDialog.text += letter;
            yield return new WaitForSeconds(0.02f);

        }
       
    }

    void EndStory()
    {
       
        Debug.Log("End Of Conversation.");

    }
}
