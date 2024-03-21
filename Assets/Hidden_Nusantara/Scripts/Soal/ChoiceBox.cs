using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChoiceBox
{
    [System.Serializable]
    public struct choiceBox
    {
        public bool addJawabanisBenar;
        [TextArea(2, 10)]
        public string Jawabansentences;
    }

    public choiceBox[] choiceBoxes= new choiceBox[3];
}