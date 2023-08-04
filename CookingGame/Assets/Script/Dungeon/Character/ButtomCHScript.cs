using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ButtomCHScript : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler
{
    public GameObject explainUI;
    public TextMeshProUGUI explainText;
    public GameObject UIChooseCH;
    public void OnPointerEnter(PointerEventData eventData)
    {
        explainUI.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        explainUI.SetActive(false);
    }
    public void ChooseCharacter()
    {
        explainUI.SetActive(false);
        UIChooseCH.SetActive(true);
        UIChooseCH.transform.position = new Vector3(transform.position.x, UIChooseCH.transform.position.y, UIChooseCH.transform.position.z);
    }
    private void Start()
    {
        explainUI.SetActive(false);
        //GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
    }
}
