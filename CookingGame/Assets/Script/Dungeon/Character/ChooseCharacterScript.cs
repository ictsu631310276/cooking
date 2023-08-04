using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChooseCharacterScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public List<DataCh> dataCH;
    public GameObject chooseCHUI;
    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        chooseCHUI.SetActive(false);
    }
    private void Start()
    {
        chooseCHUI.SetActive(false);
    }

    [System.Serializable]
    public class DataCh
    {
        public int id;
        
    }
}
