using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VisualNovelScript : MonoBehaviour
{
    public Sprite[] imageCharacter;
    public Dialoge[] dataText;
    public GameObject buttomNext;
    public int nowDialogue = 0;
    public bool pressButtom = false;
    public bool taking = false;
    public TextMeshProUGUI textShowSpeed;

    public GameObject historyUI;
    public void NextDialogue()
    {
        if (nowDialogue == dataText.Length - 1)
        {
            Debug.Log("End");
        }
        else if (!taking)
        {
            nowDialogue++;
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
            textShowSpeed.text = "X" + 5;
        }
        else
        {
            dialogueBoxScript.talkTextSpeed = 0.05f;
            textShowSpeed.text = "X" + 1;
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
        historyUI.SetActive(false);
    }
    private void Update()
    {

    }
    [System.Serializable]
    public class Dialoge
    {
        public int positionSit;//1-3 4
        public bool openImage;
        public int imageCharacter;//ถ้าไม่ได้เปลี่ยน ไม่ต้องเปลี่ยน
        public string name;//ถ้าไม่ได้เปลี่ยน ไม่ต้องเปลี่ยน
        public string text;
    }

}
