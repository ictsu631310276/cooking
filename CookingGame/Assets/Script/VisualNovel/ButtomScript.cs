using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ButtomScript : MonoBehaviour
{
    public VisualNovelScript buttom;
    public GameObject B1;
    public TextMeshProUGUI B1Text;
    public GameObject B2;
    public TextMeshProUGUI B2Text;
    public int[] love = new int[2];
    public static bool behindChoice = false;
    public void B1Clike()
    {
        buttom.pressButtom = true;
        behindChoice = true;
        VisualNovelScript.nowDialogue++;//ไปข้ามตอนจบข้อความ
        buttom.buttomGOJ.SetActive(false);
    }
    public void B2Clike()
    {
        buttom.pressButtom = true;
        VisualNovelScript.nowDialogue = VisualNovelScript.nowDialogue + buttom.dataText[VisualNovelScript.nowDialogue].skipDialoge;//ข้ามเลย
        buttom.buttomGOJ.SetActive(false);
    }
    private void Start()
    {
        B1.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        B2.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
    }
}