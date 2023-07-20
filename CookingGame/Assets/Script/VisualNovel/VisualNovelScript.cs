using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualNovelScript : MonoBehaviour
{
    public VisualNovelData data;
    public GameObject buttomNext;
    public int nowDialogue = 0;
    public void NextDialogue()
    {
        nowDialogue++;
    }
    void Start()
    {
        //buttomNext.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
