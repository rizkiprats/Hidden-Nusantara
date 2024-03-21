using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Action_ChoiceBox : MonoBehaviour
{
    [TextArea(2, 10)]
    public string PertanyaanSentence;
    public ChoiceBox choiceBox;

    [Header("ChoiceBox")]
    public TMP_Text SoalText;
    public List<Button> ButtonChoice;
    public int SoalNumber;

    //private void Awake()
    //{
    //    SoalText.text = PertanyaanSentence;

    //    RandomizeChoice();
    //}

    private void Start()
    {
        SoalText.text = PertanyaanSentence;

        RandomizeChoice();
    }

    public void RandomizeChoice()
    {
        if(ButtonChoice.Count != 0 && choiceBox.choiceBoxes.Length != 0)
        {
            List<int> randomChoices = new List<int>();

            for (int i = 0; i < ButtonChoice.Count; i++)
            {
                int randomChoicesIndex = Random.Range(0, choiceBox.choiceBoxes.Length);
                while (randomChoices.Contains(randomChoicesIndex))
                {
                    randomChoicesIndex = Random.Range(0, choiceBox.choiceBoxes.Length);
                }
                if (ButtonChoice[i].GetComponentInChildren<TMP_Text>() != null)
                {
                    ButtonChoice[i].GetComponentInChildren<TMP_Text>().text = choiceBox.choiceBoxes[randomChoicesIndex].Jawabansentences;
                }
                if (ButtonChoice[i].GetComponent<Add_SoalEntry>() != null)
                {
                    ButtonChoice[i].GetComponent<Add_SoalEntry>().setJawaban(choiceBox.choiceBoxes[randomChoicesIndex].addJawabanisBenar, SoalNumber);
                }
                randomChoices.Add(randomChoicesIndex);
            }
        }
    }
}