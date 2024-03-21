using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager_Object : MonoBehaviour
{
    public Text nameText;

    public Text dialogText;
    public TextMeshProUGUI TMPdialogText;

    public Image karakterImage;

    public Animator animator;

    public AudioSource audioAudioSource;

    private Queue<string> sentences;

    public GameObject Skipbutton;

    private bool skipDialog;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        skipDialog = false;
    }

    public void StartObjectDialog(ObjectDialog dialog)
    {
        skipDialog = false;

        animator.SetBool("IsOpen", true);

        Debug.Log("Starting Conversation from " + dialog.objectName);

        if (nameText != null)
        {
            nameText.text = dialog.objectName;
        }

        if (karakterImage != null)
        {
            karakterImage.sprite = dialog.objectImage;
        }

        sentences.Clear();

        foreach (string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextObjectSentence();
    }

    public void DisplayNextObjectSentence()
    {
        Skipbutton.SetActive(true);
        if (sentences.Count == 0)
        {
            EndObjectDialog();
            return;
        }

        string sentence = sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeObjectSentence(sentence));
    }

    IEnumerator TypeObjectSentence(string sentence)
    {
        skipDialog = false;
        TMPdialogText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            if (skipDialog)
            {
                TMPdialogText.text += "";
            }
            else
            {
                TMPdialogText.text += letter;
                audioAudioSource.Play();
                yield return new WaitForSeconds(0.07f);
                audioAudioSource.Pause();
            }
            
            if (skipDialog)
            {
                audioAudioSource.Pause();
                StopAllCoroutines();
                TMPdialogText.text = sentence;
            }
        }

        Skipbutton.SetActive(false);
    }

    void EndObjectDialog()
    {
        Skipbutton.SetActive(false);

        animator.SetBool("IsOpen", false);

        Debug.Log("End Of Conversation.");
    }

    public void SkipDialog()
    {
        audioAudioSource.Pause();
        skipDialog = true;
        Skipbutton.SetActive(false);
    }

    //public void LoadData(GameDataSave data)
    //{
    //    this.answered = data.answeredquestion;
    //}

    //public void SaveData(ref GameDataSave data)
    //{
    //    data.answeredquestion = this.answered;
    //}
}