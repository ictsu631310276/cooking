using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    public float timeMax;
    public Image Fill;
    private float time;
    public static bool closeDay = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        Fill.fillAmount = time / timeMax;
        if (time >= timeMax)
        {
            closeDay = true;
            SpawnNPCScript.open = false;
        }
    }
}
