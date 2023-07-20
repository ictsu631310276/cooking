using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VisualNovelData : MonoBehaviour
{
    public Sprite[] character;
    public RawImage charaterImage;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public string[] dialogue;
    private float i = 0;
    private void Start()
    {
        charaterImage.color = new Color(255,255,255,0);
    }

    // Update is called once per frame
    private void Update()
    {
        if (i <= 255)
        {
            i = i + Time.deltaTime;
        }
        charaterImage.color = new Color(255, 255, 255, i);
    }
}
