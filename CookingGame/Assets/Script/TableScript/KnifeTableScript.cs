using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KnifeTableScript : MonoBehaviour
{
    public int idTable;
    public GameObject glowObject;

    private int itemInHand = 0;//ของบนโต้ะ
    private bool haveItem = false;
    public GameObject handPoint;
    private GameObject itemModel;
    private GameObject[] allItemModel;
    public float timeChopped;
    private float timeOld;
    private bool holdButtom = false;

    public ingredientScript ingredientScript;

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
    private void Chopped()
    {
        switch (itemInHand)
        {
            case 1 :
                itemInHand = 2;
                timeChopped = timeOld;
                break;
            default:
                break;
        }
        Destroy(itemModel, 0);
        haveItem = false;//เพื่อให้ลบ item เดิมออก
        ShowModel();
    }
    private void Start()
    {
        glowObject.SetActive(false);
        timeOld = timeChopped;
        allItemModel = new GameObject[ingredientScript.allIngredient.Length];
        for (int i = 0; i < ingredientScript.allIngredient.Length; i++)
        {
            allItemModel[i] = ingredientScript.allIngredient[i];
        }
    }
    void Update()
    {
        ShowModel();
        if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("Fire2"))
        {
            holdButtom = true;
        }//กดค้าง
        else if (Input.GetKeyUp(KeyCode.R) || Input.GetButtonUp("Fire2"))
        {
            holdButtom = false;
        }
        if (InteractionPlayerScript.tableInteraction.Count != 0)
        {
            if (InteractionPlayerScript.tableInteraction[InteractionPlayerScript.tableInteraction.Count - 1] == idTable)
            {
                glowObject.SetActive(true);
                if ((Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump")) && !haveItem &&
                    InteractionPlayerScript.haveItem)
                {
                    itemInHand = InteractionPlayerScript.itemInHand;

                    InteractionPlayerScript.itemInHand = 0;
                }
                else if ((Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump")) && haveItem &&
                    !InteractionPlayerScript.haveItem)
                {
                    InteractionPlayerScript.itemInHand = itemInHand;
                    itemInHand = 0;
                    timeChopped = timeOld;//เอา item ออก = สับใหม่
                }
                if (holdButtom)
                {
                    timeChopped = timeChopped - Time.deltaTime;
                    if (timeChopped <= 0)
                    {
                        Chopped();
                    }
                }
            }
            else if (InteractionPlayerScript.tableInteraction[InteractionPlayerScript.tableInteraction.Count - 1] != idTable)
            {
                glowObject.SetActive(false);
            }
        }
    }
}

