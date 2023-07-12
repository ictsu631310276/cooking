using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTableScript : MonoBehaviour
{
    public int idTable;
    public GameObject glowObject;

    private int itemInHand = 0;//ของบนโต้ะ
    private bool haveItem = false;
    public GameObject handPoint;
    private GameObject itemModel;
    private GameObject[] allItemModel;
    public ingredientScript ingredient;

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
    private void Start()
    {
        allItemModel = new GameObject[ingredient.allIngredient.Length];
        for (int i = 0; i < ingredient.allIngredient.Length; i++)
        {
            allItemModel[i] = ingredient.allIngredient[i];
        }
        glowObject.SetActive(false);
    }

    // Update is called once per frame
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
                    itemInHand = InteractionPlayerScript.itemInHand;

                    InteractionPlayerScript.itemInHand = 0;
                }
                else if ((Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("Jump")) && haveItem &&
                    !InteractionPlayerScript.haveItem)
                {
                    InteractionPlayerScript.itemInHand = itemInHand;

                    itemInHand = 0;
                }
            }
            else if (InteractionPlayerScript.tableInteraction[InteractionPlayerScript.tableInteraction.Count - 1] != idTable)
            {
                glowObject.SetActive(false);
            }
        }
    }
}
