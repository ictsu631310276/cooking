using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VisualNovelScript : MonoBehaviour
{
    public Sprite[] imageCharacter;
    public Sprite[] imageBG;
    public Dialoge[] dataText;
    public GameObject buttomNext;
    public static int nowDialogue = 0;

    public bool taking = false;
    public bool pressButtom = false;
    public Image speedButtom;
    public Sprite[] ShowSpeed;
    public GameObject historyUI;

    public GameObject buttomChoice; 
    public ChoiceTextScript choiceData;
    public ButtomScript buttomScript;

    public SpriteRenderer BackGround;
    public GameObject buttomGOJ;
    public void NextDialogue()
    {
        if (nowDialogue == dataText.Length-1)
        {
            Debug.Log("End");
        }
        else if (!taking)
        {
            if (dataText[nowDialogue].ChoiceText.Length == 0)//ไม่มีตัวเลือก
            {
                if (ButtomScript.behindChoice)
                {
                    ButtomScript.behindChoice = false;
                    nowDialogue += 2;
                }
                else
                {
                    nowDialogue++;
                }
            }
            else
            {
                buttomGOJ.SetActive(true);
                buttomScript.B1Text.text = dataText[nowDialogue].ChoiceText[0];
                buttomScript.B2Text.text = dataText[nowDialogue].ChoiceText[1];
                buttomScript.love[0] = dataText[nowDialogue].relationship[0];
                buttomScript.love[1] = dataText[nowDialogue].relationship[1];
            }
            pressButtom = true;
        }
        else if (taking)
        {
            pressButtom = true;
        }
    }
    public void SetSpeed()
    {
        if (dialogueBoxScript.talkTextSpeed == 0.05f)
        {
            dialogueBoxScript.talkTextSpeed = 0.01f;
            speedButtom.sprite = ShowSpeed[0];
        }
        else
        {
            dialogueBoxScript.talkTextSpeed = 0.05f;
            speedButtom.sprite = ShowSpeed[1];
        }
        
    }
    public void OpenHistoryButtom()
    {
        historyUI.SetActive(true);
    }
    public void CloseHistoryButtom()
    {
        historyUI.SetActive(false);
    }
    private void Start()
    {
        //buttomNext.SetActive(false);
        buttomGOJ.SetActive(false);
        historyUI.SetActive(false);
    }
    private void Update()
    {
        BackGround.sprite = imageBG[dataText[nowDialogue].BGNow];
    }
    [System.Serializable]
    public class Dialoge
    {
        public int positionSit = 0;//1-3 4
        public bool openImage = false;
        public int imageCharacter = 0;//ถ้าไม่ได้เปลี่ยน ไม่ต้องเปลี่ยน
        public string name;//ถ้าไม่ได้เปลี่ยน ไม่ต้องเปลี่ยน
        public string text;
        public string[] ChoiceText;
        public int[] relationship;
        public int idNPCGetRelationship;
        public int BGNow;
    }
}
