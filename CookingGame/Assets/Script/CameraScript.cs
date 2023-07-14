using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player;
    public Vector3 offSet;
    public float smoothTime;
    private Vector3 i = Vector3.zero;
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 camara = player.position + offSet;
        transform.position = camara;
        //transform.position = Vector3.SmoothDamp(transform.position, camara,ref i, smoothTime);
        //transform.position = Vector3.Lerp(transform.position, camara, smoothTime);
    }
}
