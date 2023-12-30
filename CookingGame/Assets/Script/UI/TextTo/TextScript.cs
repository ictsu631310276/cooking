using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextScript : MonoBehaviour
{
    [SerializeField] private RawImage playerImage;
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private TextAndImage[] allText = new TextAndImage[1];
    [SerializeField] private float speedText;
    [SerializeField] private GameObject allObj;
    private int numChar;
    private bool showTexting;
    private void ShowText()
    {
        showTexting = true;
        playerImage = allText[numChar].playImage;
        textBox.text = string.Empty;
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
        foreach (char item in allText[numChar].text.ToCharArray())
        {
            textBox.text += item;
            yield return new WaitForSeconds(speedText);
            if (!showTexting)
            {
                break;
            }
        }
        showTexting = false;
    }
    private void Start()
    {
        showTexting = false;
        allObj.SetActive(true);
        numChar = 0;
        ShowText();
    }
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (!showTexting)
            {
                if (numChar == allText.Length - 1)
                {
                    allObj.SetActive(false);
                }
                else
                {
                    numChar++;
                    ShowText();
                }
            }
            else
            {
                StopCoroutine(TypeLine());
                textBox.text = allText[numChar].text;
                showTexting = false;
            }
        }
    }
}
[System.Serializable]
public class TextAndImage
{
    public string text;
    public RawImage playImage;
}
