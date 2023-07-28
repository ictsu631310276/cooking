using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class dialogueBoxScript : MonoBehaviour
{
    public int position;
    public VisualNovelScript data;
    public SpriteRenderer imageCharacter;
    public SpriteRenderer boxText;
    public TextMeshPro nameCharacter;
    public TextMeshPro dialogue;
    private int numOfCharacters;
    public static float talkTextSpeed = 0.05f;
    private int numOfWords = 0;

    public bool willFade = false;//คุมการจาง
    private float fadeValue = 0;
    private bool fading = false;//กำลังจางอยู่หรือไม่

    IEnumerator RunText(float speed)
    {
        numOfCharacters = dialogue.textInfo.characterCount; 
        for (numOfWords = 0; numOfWords < numOfCharacters; numOfWords++)
        {
            dialogue.maxVisibleCharacters = numOfWords + 1;
            yield return new WaitForSeconds(speed);
        }
        data.taking = false;
        numOfWords = 0;
    }
    private void UpdateText(int now)
    {
        //numOfCharacters = dialogue.textInfo.characterCount;
        if (data.dataText[now].imageCharacter == 0)
        {
            imageCharacter.sprite = data.imageCharacter[data.dataText[now - 1].imageCharacter];
        }
        else
        {
            imageCharacter.sprite = data.imageCharacter[data.dataText[now].imageCharacter];
        }
        if (data.dataText[now].name == "")
        {
            nameCharacter.text = data.dataText[now - 1].name;
        }
        else
        {
            nameCharacter.text = data.dataText[now].name;
        }
        dialogue.text = data.dataText[now].text;
        numOfCharacters = dialogue.textInfo.characterCount;
    }
    private void Start()
    {
        imageCharacter.color = new Color(255,255,255, fadeValue);
        boxText.color = new Color(255, 255, 255, fadeValue);
        nameCharacter.color = new Color(0, 0, 0, fadeValue);
        dialogue.color = new Color(0, 0, 0, fadeValue);
    }
    private void Update()
    {        
        if (data.pressButtom && fading && !data.taking && position == data.dataText[VisualNovelScript.nowDialogue].positionSit)
        {
            data.pressButtom = false;
            StartCoroutine(RunText(talkTextSpeed));
        }
        else if (data.pressButtom && fading && data.taking)
        {
            StopCoroutine(RunText(talkTextSpeed));
            numOfWords += 1000;
            dialogue.maxVisibleCharacters += 1000;
            data.pressButtom = false;
        }//บทดพูด

        if (willFade)
        {
            if (fadeValue <= 255)
            {
                fadeValue += Time.deltaTime;
                fading = true;
            }
            else
            {
                fading = false;
            }
        }
        else if (!willFade) //จาง
        {
            if (fadeValue >= 0)
            {
                fadeValue -= Time.deltaTime;
                fading = true;
            }
            else
            {
                fading = false;
            }
        }
        imageCharacter.color = new Color(255, 255, 255, fadeValue);//จาง
        boxText.color = new Color(255, 255, 255, fadeValue);
        nameCharacter.color = new Color(255, 255, 255, fadeValue);
        dialogue.color = new Color(255, 255, 255, fadeValue);

        if (position == data.dataText[VisualNovelScript.nowDialogue].positionSit)
        {
            willFade = data.dataText[VisualNovelScript.nowDialogue].openImage;
            UpdateText(VisualNovelScript.nowDialogue);//อับเดดข้อมูล
        }

    }
}
