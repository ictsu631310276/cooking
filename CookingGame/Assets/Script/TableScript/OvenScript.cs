using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OvenScript : MonoBehaviour
{
    public int idTable;
    public GameObject glowObject;
    public ingredientScript ingredientScript;

    private int itemInHand = 0;//ของบนโต้ะ
    private bool haveItem = false;
    public GameObject handPoint;
    private GameObject itemModel;
    private GameObject[] allItemModel;
    private float timeChopped;
    private float timeUse;
    private bool finished = false;

    public Slider timeBar;
    public GameObject timeBarUI;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            glowObject.SetActive(true);
            InteractionPlayerScript.tableInteraction.Add(idTable);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            glowObject.SetActive(false);
            InteractionPlayerScript.tableInteraction.Remove(idTable);
        }
    }
    private void ShowModel()
    {
        if (!haveItem && itemInHand != 0)//แสดง item
        {
            haveItem = true;
            switch (itemInHand)
            {
                case 1:
                    itemModel = Instantiate(allItemModel[itemInHand - 1], handPoint.transform, false);
                    break;
                case 2:
                    itemModel = Instantiate(allItemModel[itemInHand - 1], handPoint.transform, false);
                    break;
                default:
                    Debug.LogError("No have this ID.");
                    break;
            }
            itemModel.transform.parent = handPoint.transform;
        }
        else if (itemInHand == 0 && haveItem)
        {
            haveItem = false;
            Destroy(itemModel, 0);
        }
    }
    private void Baking()
    {
        switch (itemInHand)
        {
            case 1 :
                itemInHand = 2;
                timeChopped = timeUse;
                break;
            default:
                break;
        }
        Destroy(itemModel, 0);
        haveItem = false;//เพื่อให้ลบ item เดิมออก
        ShowModel();
    }
    private void Showtime(float i)
    {
        timeBarUI.SetActive(true);
        timeBar.value = i;
        if (i >= timeUse)
        {
            timeBarUI.SetActive(false);
        }
    }
    private void Start()
    {
        glowObject.SetActive(false);
        timeBarUI.SetActive(false);
        timeUse = ingredientScript.timeUseManuel;
        allItemModel = new GameObject[ingredientScript.allIngredient.Length];
        for (int i = 0; i < ingredientScript.allIngredient.Length; i++)
        {
            allItemModel[i] = ingredientScript.allIngredient[i];
        }
    }
    void Update()
    {
        ShowModel();
        if (InteractionPlayerScript.tableInteraction.Count != 0)
        {
            if (InteractionPlayerScript.tableInteraction[InteractionPlayerScript.tableInteraction.Count - 1] == idTable)
            {
                glowObject.SetActive(true);
                if ((Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump")) && !haveItem &&
                    InteractionPlayerScript.haveItem)
                {
                    finished = false;
                    itemInHand = InteractionPlayerScript.itemInHand;
                    InteractionPlayerScript.itemInHand = 0;
                }
                else if ((Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump")) && haveItem &&
                    !InteractionPlayerScript.haveItem)
                {
                    InteractionPlayerScript.itemInHand = itemInHand;
                    itemInHand = 0;
                    timeBarUI.SetActive(false);
                    finished = true;
                    timeChopped = 0;//เอา item ออก = สับใหม่
                }
            }
            else if (InteractionPlayerScript.tableInteraction[InteractionPlayerScript.tableInteraction.Count - 1] != idTable)
            {
                glowObject.SetActive(false);
            }
        }
        if (haveItem && !finished )
        {
            timeChopped = timeChopped + Time.deltaTime;
            Showtime(timeChopped);
            if (timeChopped >= timeUse)
            {
                Baking();
            }
        }
    }
}

