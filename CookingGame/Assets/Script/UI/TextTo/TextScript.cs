using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TextScript : MonoBehaviour
{
    [SerializeField] private RawImage playerImage;
    [SerializeField] private TextMeshProUGUI textBox;
    public static int textStart;//0 start , 1 ยกบันดะ ,2 เปล่า , 3 รักษาเสร็จ , 4 สร้างอีกสองตัว , 5 = เปล่า , 6,7 = รักษาเสร็จ , 9 = เปล่า
    [SerializeField] private float speedText;
    [SerializeField] private float ageText;
    public GameObject allObj;
    private int numChar;
    [SerializeField] private TextAndImage[] allText;
    [SerializeField] private TextAndImage[] willTreat;
    [SerializeField] private TextAndImage[] finishTreat;
    [SerializeField] private TextAndImage[] newTreat;
    [SerializeField] private TextAndImage[] deadNPC;
    [SerializeField] private TextAndImage[] useMedicine2;
    [SerializeField] private TextAndImage[] done;
    private IEnumerator ShowText(TextAndImage[] textArray)
    {
        textStart++;
        allObj.SetActive(true);
        textBox.text = string.Empty;
        numChar = 0;
        for (int i = 0; i < textArray.Length; i++)
        {
            textBox.text = string.Empty;
            playerImage.texture = textArray[numChar].playImage.texture;
            foreach (char itemText in textArray[numChar].text.ToCharArray())
            {
                textBox.text += itemText;
                yield return new WaitForSeconds(speedText);
            }
            numChar++;
            yield return new WaitForSeconds(ageText);
        }
        allObj.SetActive(false);
        if (textStart == 2 || textStart == 5 || textStart == 15 || textStart == 18)
        {
            textStart++;
        }
    }
    private void Start()
    {
        textStart = -1;
        allObj.SetActive(true);
        StartCoroutine(ShowText(allText));
    }
    private void Update()
    {
        Debug.Log(textStart);
        if (textStart == 1)
        {
            StopAllCoroutines();
            StartCoroutine(ShowText(willTreat));
        }
        else if (textStart == 4)
        {
            StopAllCoroutines();
            StartCoroutine(ShowText(finishTreat));
        }
        else if (textStart == 7)
        {
            StopAllCoroutines();
            StartCoroutine(ShowText(newTreat));
        }
        else if (textStart == 10)
        {
            StopAllCoroutines();
            StartCoroutine(ShowText(deadNPC));
        }
        else if (textStart == 14)
        {
            StopAllCoroutines();
            StartCoroutine(ShowText(useMedicine2));
        }
        else if (textStart == 17)
        {
            StopAllCoroutines();
            StartCoroutine(ShowText(done));
        }
        else if (textStart == 19)
        {
            SceneManager.LoadScene("CutScene2");
        }
    }
}
[System.Serializable]
public class TextAndImage
{
    public string text;
    public Sprite playImage;
}
