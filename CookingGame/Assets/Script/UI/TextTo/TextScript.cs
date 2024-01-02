using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextScript : MonoBehaviour
{
    [SerializeField] private RawImage playerImage;
    [SerializeField] private TextMeshProUGUI textBox;
    public int textStart;
    [SerializeField] private TextAndImage[] allText = new TextAndImage[1];
    [SerializeField] private float speedText;
    public GameObject allObj;
    private int numChar;
    private bool showTexting;
    public TextAndImage[] WillTreat;
    public void ShowText(TextAndImage[] i)
    {
        if (Input.anyKeyDown)
        {
            if (!showTexting)
            {
                if (numChar >= i.Length - 1)
                {
                    allObj.SetActive(false);
                    numChar = 0;
                    textStart++;
                }
                else
                {
                    numChar++;
                    showTexting = true;

                    playerImage = i[numChar].playImage;
                    textBox.text = string.Empty;
                    StartCoroutine(TypeLine(i));
                }
            }
            else
            {
                StopCoroutine(TypeLine(i));
                textBox.text = i[numChar].text;
                showTexting = false;
            }
        }
    }
    IEnumerator TypeLine(TextAndImage[] i)
    {
        foreach (char item in i[numChar].text.ToCharArray())
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
        textStart = 0;
        showTexting = false;
        numChar = 0;
    }
    private void Update()
    {
        Debug.Log("Hi");
        if (textStart == 0)
        {
            allObj.SetActive(true);
            ShowText(allText);
        }
        else if (textStart == 2)
        {
            allObj.SetActive(true);
            ShowText(WillTreat);
        }
    }
}
[System.Serializable]
public class TextAndImage
{
    public string text;
    public RawImage playImage;
}
