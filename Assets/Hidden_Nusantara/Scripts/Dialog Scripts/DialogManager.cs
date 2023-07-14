using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour, InterfaceDataSave
{
    public static DialogManager instance;
    public Text nameText;
    public Text dialogText;
    public GameObject dimmer;
    public Image karakterImage;

    public Animator animator;

    private Queue<string> sentences;

    public GameObject Skipbutton;

    private bool skipDialog;

    public GameObject ChoicesPanel;

    public GameObject rightPanel;
    public GameObject wrongPanel;
    public GameObject wrong2Panel;


    public GameObject Textbox;
    public GameObject Choice1;
    public GameObject Choice2;
    public GameObject Choice3;

    public GameObject NextLevel;
    public Animator doorAnim;

    public float timer;

    public bool answered;

    public int ChoiceMade;

    public bool haveChoices;
    void Awake()
    {
        instance = this;
    }



    // Start is called before the first frame update
    void Start()
    {


        sentences = new Queue<string>();
        skipDialog = false;

        doorAnim = NextLevel.GetComponentInParent<Animator>();
        haveChoices = false;
        if (answered)
        {
            NextLevel.gameObject.SetActive(true);
            doorAnim = NextLevel.GetComponentInParent<Animator>();
            doorAnim.SetBool("open", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        timer -= Time.deltaTime;
        if (Choice1 != null)
        {
            haveChoices = true;
            if (ChoiceMade >= 1)
            {
                if (timer <= 1)
                {
                    if (answered)
                    {
                        rightPanel.SetActive(false);
                        timer = 0;
                        doorAnim.SetBool("open", true);
                    }
                    else
                    {
                        wrong2Panel.SetActive(false);
                        wrongPanel.SetActive(false);
                        timer = 0;
                    }

                }
            }else if (ChoiceMade >= 0)
            {
                if(timer <= 1)
                {
                    if (answered)
                    {
                        rightPanel.SetActive(false);
                        timer = 0;
                        doorAnim.SetBool("open", true);
                    }
                }
            }
        }
        if (answered == true)
        {
            NextLevel.gameObject.SetActive(true);
            doorAnim = NextLevel.GetComponentInParent<Animator>();
            doorAnim.SetBool("open", true);
        }
    }

    public void StartDialog(Dialog dialog)
    {
        dimmer.SetActive(true);

        skipDialog = false;

        animator.SetBool("IsOpen", true);

        Debug.Log("Starting Conversation from " + dialog.name);

        nameText.text = dialog.name;

        karakterImage.sprite = dialog.karakterImage;

        sentences.Clear();

        foreach (string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();

    }

    public void DisplayNextSentence()
    {
        Skipbutton.SetActive(true);
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();

        //Debug.Log(sentence);

        //dialogText.text = sentence;
        
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        skipDialog = false;
        dialogText.text = "";
      
            foreach (char letter in sentence.ToCharArray())
            {
                if (skipDialog)
                {
                    dialogText.text += "";
                }
                else
                {
                dialogText.text += letter;

                }
                yield return new WaitForSeconds(0.02f);
                if (skipDialog)
                {
                StopAllCoroutines();
                dialogText.text = sentence;
                
                }
            }
            
        Skipbutton.SetActive(false);


    }

    void EndDialog()
    {
        Skipbutton.SetActive(false); 

        dimmer.SetActive(false);

        animator.SetBool("IsOpen", false);

        if (haveChoices)
        {
            if (answered)
            {
                rightPanel.SetActive(true);
                timer = 3;
            }
            else
            {
                ChoicesPanel.SetActive(true);
            }
        }

        Debug.Log("End Of Conversation.");

    }

   public void  SkipDialog()
    {
        
        skipDialog = true;
        Skipbutton.SetActive(false);

    }

    public void ChoiceOption1()
    { 
        rightPanel.SetActive(true);
        ChoicesPanel.SetActive(false);
        timer = 3;
        ChoiceMade = 1;
        answered = true;
        DataSaveManager.instance.SaveGame();

        NextLevel.gameObject.SetActive(true);
        doorAnim.SetBool("open", true);
    }
    public void ChoiceOption2()
    {
        ChoiceMade = 2;
        wrongPanel.SetActive(true);
        ChoicesPanel.SetActive(false);
        timer = 3;
    }
    public void ChoiceOption3()
    {
        ChoiceMade = 3;
        wrong2Panel.SetActive(true);
        ChoicesPanel.SetActive(false);
        timer = 3;
    }

    public void LoadData(GameDataSave data)
    {
        this.answered = data.answeredquestion;
    }

    public void SaveData(ref GameDataSave data)
    {
        data.answeredquestion = this.answered;
    }

}

