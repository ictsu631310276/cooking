using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player;
    public Vector3 offSet;
    public GameObject cam1;
    public GameObject cam2;

    public bool zoomOut = true;

    private void Start()
    {
        cam1.SetActive(true);
        cam2.SetActive(false);
    }
    private void Update()
    {
        Vector3 camara = player.position + offSet;
        transform.position = camara;
        if (zoomOut)
        {
            cam1.SetActive(true);
            cam2.SetActive(false);
        }
        else
        {
            cam1.SetActive(false);
            cam2.SetActive(true);
        }
    }
}
