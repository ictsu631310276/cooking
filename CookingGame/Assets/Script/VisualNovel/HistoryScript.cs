using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryScript : MonoBehaviour
{
    public VisualNovelScript data;
    private List<string> whoTalk;
    private List<string> text;
    public GameObject message;
    public Transform createMessage;
    private void Start()
    {
        for (int i = 0; i < data.dataText.Length; i++)
        {
            whoTalk.Add(data.dataText[i].name);
            text.Add(data.dataText[i].text);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
