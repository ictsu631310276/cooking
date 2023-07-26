using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetItemScript : MonoBehaviour
{
    public int id;
    public int amount;
    public string nameItem;
    public TextMeshProUGUI nameItemText;
    public Image imageItem;
    public void GetItem()
    {
        if (amount > 0)
        {
            if (amount != 999)
            {
                amount--;
            }
            InteractionPlayerScript.itemInHand = id;
            FridgeScript.openUI = false;
            CameraScript.zoomOut = true;
        }
        else if (amount <= 0)
        {
            Debug.Log("out of item");
        }
    }
    private void Update()
    {
        nameItemText.text = nameItem + " : " + amount;
    }
}
