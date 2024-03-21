using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class DialogManager_Story : MonoBehaviour, InterfaceDataSave
{
    [Header("Story Panel")]
    public GameObject StoryPanel;
    public TextMeshProUGUI StoryDialog;

    [Header("Story Panel Animator")]
    public Animator StoryPanelAnimator;

    [Header("Story Audio")]
    public AudioSource storyTypingAudioSource;
    public AudioSource storyClipAudioSource;
    private Queue<string> storysentences;
    private Queue<AudioClip> storyAudioClip;
    private bool stopDialog;

    [Header("Skip Story Button")]
    public GameObject SkipStoryButton;

    private bool skipstoryDialog;

    public bool isStoryEnd;

    private UnityEvent endStoryEvent;

    [SerializeField]private string playerName;

    // Start is called before the first frame update
    void Start()
    {
        if(storyTypingAudioSource != null)
        {
            storyTypingAudioSource.mute = true;
        }
        storysentences = new Queue<string>();
        storyAudioClip = new Queue<AudioClip>();
        skipstoryDialog = false;
        isStoryEnd = false;

        if (DataSaveManager.instance != null)
        {
            DataSaveManager.instance.LoadGame();
        }
    }

    public void StartStoryDialog(Story story)
    {
        StoryPanel.gameObject.SetActive(true);

        skipstoryDialog = false;
        isStoryEnd = false;

        StoryPanelAnimator.SetBool("IsOpen", true);

        Debug.Log("Start Conversation from " + story.name);

        endStoryEvent = story.endStoryEvent;

        storysentences.Clear();
        storyAudioClip.Clear();

        foreach (string sentence in story.sentences)
        {
            storysentences.Enqueue(sentence);
        }

        foreach (AudioClip AudioClipsentence in story.StoryAudioClipsentences)
        {
            storyAudioClip.Enqueue(AudioClipsentence);
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
        if(playerName != "")
        {
            sentence = sentence.Replace("Raden", playerName);
        }
        
        AudioClip AudioClipsentence = storyAudioClip.Dequeue();

        StopAllCoroutines();

        StartCoroutine(TypeStory(sentence, AudioClipsentence));
    }

    IEnumerator TypeStory(string sentence, AudioClip AudioClipsentence)
    {
        stopDialog = false;
        skipstoryDialog = false;
        isStoryEnd = false;
        StoryDialog.text = "";

        if(AudioClipsentence != null)
        {
            storyClipAudioSource.clip = AudioClipsentence;
        }

        foreach (char letter in sentence.ToCharArray())
        {

            if (skipstoryDialog)
            {
                StoryDialog.text += "";
                StopAllCoroutines();
                StoryDialog.text = sentence;
                storyClipAudioSource.Stop();
            }
            else
            {
                StoryDialog.text += letter;
                if (stopDialog)
                    storyClipAudioSource.Stop();
                else
                    storyClipAudioSource.Play();
                storyTypingAudioSource.Play();
                yield return new WaitForSeconds(0.075f);
                //Debug.Log(storyClipAudioSource.time);
                if (storyClipAudioSource.time == 0f)
                {
                    stopDialog = true;
                }
                storyClipAudioSource.Pause();
                storyTypingAudioSource.Pause();
            }
        }

        SkipStoryButton.SetActive(false);

        if(StoryDialog.text == sentence)
        {
            yield return new WaitForSeconds(1.5f);
            DisplayNextStory();
        }
    }

    void EndStory()
    {
        storyClipAudioSource.Stop();
        storyTypingAudioSource.Stop();

        StopAllCoroutines();

        SkipStoryButton.SetActive(false);

        isStoryEnd = true;

        StoryPanelAnimator.SetBool("IsOpen", false);

        endStoryEvent.Invoke();

        Debug.Log("End Of Conversation.");

        if (FindObjectOfType<PlayerControllers>() != null)
            FindObjectOfType<PlayerControllers>().OnEnable();
    }

    public void SkipStory()
    {
        storyTypingAudioSource.Pause();
        skipstoryDialog = true;
        SkipStoryButton.SetActive(false);
    }

    public void DisableStoryPanel()
    {
        StoryPanel.gameObject.SetActive(false);
    }

    public void SkipStoryDialog()
    {
        EndStory();
    }

    public void LoadData(GameDataSave data)
    {
        if(data != null)
        {
            playerName = data.playerName;
        }
    }

    public void SaveData(ref GameDataSave data)
    {
        
    }
}