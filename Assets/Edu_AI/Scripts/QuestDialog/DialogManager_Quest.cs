using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager_Quest : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI QuestDialog;
    public Animator QuestPanelAnimator;

    private Queue<string> questsentences;

    public GameObject SkipQuestButton;

    private bool skipquestDialog;

    public bool isquestEnd;



    void Start()
    {
        questsentences = new Queue<string>();
        skipquestDialog = false;
        isquestEnd = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartQuestDialog(Quest dialog)
    {
        skipquestDialog = false;
        isquestEnd = false;

        QuestPanelAnimator.SetBool("IsOpen", true);

        Debug.Log("Start Conversation from " + dialog.name);

        questsentences.Clear();

        foreach (string sentence in dialog.sentences)
        {
            questsentences.Enqueue(sentence);
        }

        DisplayNextQuest();
    }

    public void DisplayNextQuest()
    {
        SkipQuestButton.SetActive(true);

        if (questsentences.Count == 0)
        {
            EndQuest();
            return;
        }

        string sentence = questsentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeQuest(sentence));
    }

    IEnumerator TypeQuest(string sentence)
    {
        skipquestDialog = false;
        isquestEnd = false;
        QuestDialog.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            if (skipquestDialog)
            {
                QuestDialog.text += "";
            }
            else
            {
                QuestDialog.text += letter;
            }

            yield return new WaitForSeconds(0.02f);

            if (skipquestDialog)
            {
                StopAllCoroutines();
                QuestDialog.text = sentence;
            }
        }

        SkipQuestButton.SetActive(false);
    }

    void EndQuest()
    {
        SkipQuestButton.SetActive(false);

        isquestEnd = true;

        QuestPanelAnimator.SetBool("IsOpen", false);

        Debug.Log("End Of Conversation.");

    }

    public void SkipQuest()
    {

        skipquestDialog = true;
        SkipQuestButton.SetActive(false);

    }
}
