using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveScript : MonoBehaviour
{
    [SerializeField] private PotionDataScript dataPotion;//เอาเวลาที่ใช้ในการกดปุ่ม
    private float timeDelayInput;
    private ToolPlayerScript toolPlayer;
    [SerializeField] private float playerSpeed = 4.0f;
    private Vector3 startPosition;
    private Rigidbody rb;
    [SerializeField] private Animator playerAnimator;

    private PlayerInput playerInput;
    private PlayerInputActions inputActions;

    float xMove,zMove;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        toolPlayer = GetComponent<ToolPlayerScript>();
        startPosition = transform.position;
        timeDelayInput = 0;

        playerInput = GetComponent<PlayerInput>();
        inputActions = new PlayerInputActions();
        inputActions.Player.walk.performed += Movement_performed;
    }
    public void Movement_performed(InputAction.CallbackContext obj)
    {
        Vector2 inputV = obj.ReadValue<Vector2>();
        xMove = inputV.x;
        zMove = inputV.y;
        Debug.Log(obj);
    }
    private void AnimationArrow(int i)
    {
        playerAnimator.SetInteger("arrow", i);
        //transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        timeDelayInput = 0;
    }

    private void Update()
    {
        //float xMove = Input.GetAxisRaw("Horizontal");
        //float zMove = Input.GetAxisRaw("Vertical");
        
        if (xMove != 0 || zMove != 0)
        {
            playerAnimator.SetBool("walking", true);
        }
        else
        {
            playerAnimator.SetBool("walking", false);
        }//อนิเมชั่นเดิน
        timeDelayInput += Time.deltaTime;
        if (toolPlayer.bed.Count > 0 && timeDelayInput >= dataPotion.timeDelayInput - 0.1f)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                AnimationArrow(0);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                AnimationArrow(1);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                AnimationArrow(2);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                AnimationArrow(3);
            }
            else
            {
                playerAnimator.SetInteger("arrow", 4);//null
            }
        }
        else
        {
            playerAnimator.SetInteger("arrow", 4);//null
        }

        rb.velocity = new Vector3(xMove, startPosition.y, zMove) * playerSpeed;//เครื่องที่
        switch (xMove , zMove)
        {
            case (0, > 0.5f):
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                break;
            case ( > 0.5f, > 0.5f):
                transform.rotation = Quaternion.Euler(0f, 45f, 0f);
                break;
            case ( > 0.5f, 0):
                transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                break;
            case ( > 0.5f, < -0.5f):
                transform.rotation = Quaternion.Euler(0f, 135f, 0f);
                break;
            case (0, < -0.5f):
                transform.rotation = Quaternion.Euler(0f, 180, 0f);
                break;
            case ( < -0.5f, < -0.5f):
                transform.rotation = Quaternion.Euler(0f, -135f, 0f);
                break;
            case ( < -0.5f, 0):
                transform.rotation = Quaternion.Euler(0f, -90f, 0f);
                break;
            case ( < -0.5f, > 0.5f):
                transform.rotation = Quaternion.Euler(0f, -45f, 0f);
                break;
        }//หมุน
    }
}