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
    public void B1Clike()
    {

        buttom.buttomGOJ.SetActive(false);
    }
    public void B2Clike()
    {

        buttom.buttomGOJ.SetActive(false);
    }
    private void Start()
    {
        B1.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        B2.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
    }
}