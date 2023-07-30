using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderUIScript : MonoBehaviour
{
    public List<int> orderIDItem;
    public ShowOrderScript orderUI;
    public List<ShowOrderScript> All;
    private int numToDelete = 0;
    private int finishedFood = 0;
    public void CreateOrderUI()
    {
        DestroyItemInList();
        finishedFood = 0;
        for (int i = 0; i < orderIDItem.Count; i++)
        {
            if (orderIDItem[i] != 0)
            {
                int j = i - finishedFood;
                ShowOrderScript showOrder = Instantiate(orderUI, transform, false);
                showOrder.idItem = orderIDItem[i];
                showOrder.transform.position = new Vector3(showOrder.transform.position.x, showOrder.transform.position.y + (180f * j * -1f), showOrder.transform.position.z);
                All.Add(showOrder);
            }
            else if (orderIDItem[i] == 0)
            {
                finishedFood++;
            }
        }
    }
    private void DestroyItemInList()
    {
        for (int i = 0; i < All.Count; i++)
        {
            All[i].reCreate = true;
        }
        All.Clear();
    }
    private void Update()
    {
        if (orderIDItem.Count != numToDelete)
        {
            numToDelete = orderIDItem.Count;
            CreateOrderUI();
        }
    }
}
