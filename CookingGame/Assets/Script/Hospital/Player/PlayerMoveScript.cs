using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 4.0f;
    private Vector3 startPosition;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
    }
    private void Update()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector3(xMove, startPosition.y, zMove) * playerSpeed;//เครื่องที่
        switch (xMove , zMove)
        {
            case (0, 1):
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                break;
            case (1, 1):
                transform.rotation = Quaternion.Euler(0f, 45f, 0f);
                break;
            case (1, 0):
                transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                break;
            case (1, -1):
                transform.rotation = Quaternion.Euler(0f, 135f, 0f);
                break;
            case (0, -1):
                transform.rotation = Quaternion.Euler(0f, 180, 0f);
                break;
            case (-1, -1):
                transform.rotation = Quaternion.Euler(0f, -135f, 0f);
                break;
            case (-1, 0):
                transform.rotation = Quaternion.Euler(0f, -90f, 0f);
                break;
            case (-1, 1):
                transform.rotation = Quaternion.Euler(0f, -45f, 0f);
                break;
        }//หมุน
    }
}