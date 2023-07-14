using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager_Story : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI StoryDialog;
    public Animator StoryPanelAnimator;

    private Queue<string> storysentences;

    public GameObject SkipStoryButton;

    private bool skipstoryDialog;

    public bool isStoryEnd;



    void Start()
    {
        storysentences = new Queue<string>();
        skipstoryDialog = false;
        isStoryEnd = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartStoryDialog(Story dialog)
    {
        skipstoryDialog = false;
        isStoryEnd = false;

        StoryPanelAnimator.SetBool("IsOpen", true);

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
        SkipStoryButton.SetActive(true);

        if(storysentences.Count == 0)
        {
            EndStory();
            return;
        }

        string sentence = storysentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeStory(sentence));
    }

    IEnumerator TypeStory(string sentence)
    {
        skipstoryDialog = false;
        isStoryEnd = false;
        StoryDialog.text = "";

        foreach(char letter in sentence.ToCharArray())
        {
            if (skipstoryDialog)
            {
                StoryDialog.text += "";
            }
            else
            {
                StoryDialog.text += letter;
            }

            yield return new WaitForSeconds(0.02f);

            if (skipstoryDialog)
            {
                StopAllCoroutines();
                StoryDialog.text = sentence;
            }
        }

        SkipStoryButton.SetActive(false);
    }

    void EndStory()
    {
        SkipStoryButton.SetActive(false);

        isStoryEnd = true;

        StoryPanelAnimator.SetBool("IsOpen", false);

        Debug.Log("End Of Conversation.");

    }

    public void SkipStory()
    {

        skipstoryDialog = true;
        SkipStoryButton.SetActive(false);

    }
}
