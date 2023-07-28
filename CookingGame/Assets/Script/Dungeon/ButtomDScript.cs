using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtomDScript : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler
{
    public int idDun;
    public GameObject explainUI;
    public void OnPointerEnter(PointerEventData eventData)
    {
        explainUI.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        explainUI.SetActive(false);
    }

    public void WillGoDann()
    {
        if (DungeonScript.numOfDunWillGo == 0)
        {
            DungeonScript.numOfDunWillGo = idDun;
        }
        else if (DungeonScript.numOfDunWillGo != 0)
        {
            DungeonScript.numOfDunWillGo = 0;
        }
    }
    // Start is called before the first frame update
    private void Start()
    {
        //GetComponent<Button>().Mouse
        explainUI.SetActive(false);
        //GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
