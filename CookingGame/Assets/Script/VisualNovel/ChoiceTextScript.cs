using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceTextScript : MonoBehaviour
{
    public ChoiceText allChoice;

    [System.Serializable]
    public class ChoiceText
    {
        public string[] text = new string[2];
        public int[] result = new int[2];
    }
}
