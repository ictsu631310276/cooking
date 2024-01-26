using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveScript : MonoBehaviour
{
    public PotionDataScript dataPotion;//เอาเวลาที่ใช้ในการกดปุ่ม
    private float timeDelayInput;
    private ToolPlayerScript toolPlayer;
    [SerializeField] private float playerSpeed = 4.0f;
    private Vector3 startPosition;
    private Rigidbody rb;
    private SoundPlayerScript sound;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private ParticleSystem particleWalk;

    private PlayerInputActions inputActions;
    [HideInInspector] public float xMove, zMove;
    private void PlayerMovement()
    {
        rb.velocity = new Vector3(xMove, startPosition.y, zMove) * playerSpeed;//เครื่องที่
        switch (xMove, zMove)
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
    public void Movement_performed(InputAction.CallbackContext obj)
    {
        Vector2 inputV = obj.ReadValue<Vector2>();
        xMove = inputV.x;
        zMove = inputV.y;

        if (xMove != 0 || zMove != 0)
        {
            particleWalk.gameObject.SetActive(true);
            particleWalk.Play();
            playerAnimator.SetBool("walking", true);
            sound.PlaySoundWalk();
            sound.soundPlayerEffect.loop = true;
        }
        else
        {
            particleWalk.gameObject.SetActive(false);
            playerAnimator.SetBool("walking", false);
            sound.soundPlayerEffect.loop = false;
        }//อนิเมชั่นเดินและเสียง
    }
    private void AnimationArrow(int i)
    {
        playerAnimator.SetInteger("arrow", i);
        timeDelayInput = 0;
        StartCoroutine(Delaysome());
    }
    IEnumerator Delaysome()
    {
        yield return new WaitForSeconds(0.1f);
        playerAnimator.SetInteger("arrow", 5);
    }
    public void InputAnimationArrow(InputAction.CallbackContext obj)
    {
        if (toolPlayer.bed.Count > 0 && toolPlayer.bed[0].NPCData != null)
        {
            Vector2 sw = obj.ReadValue<Vector2>();
            if (obj.started)
            {
                switch (sw.x, sw.y)
                {
                    case (0f, 1f):
                        AnimationArrow(0);
                        break;
                    case (0f, -1f):
                        AnimationArrow(1);
                        break;
                    case (-1f, 0f):
                        AnimationArrow(2);
                        break;
                    case (1f, 0f):
                        AnimationArrow(3);
                        break;
                    default:
                        AnimationArrow(5);
                        break;
                }
            }
        }
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        toolPlayer = GetComponent<ToolPlayerScript>();
        sound = GetComponent<SoundPlayerScript>();
        startPosition = transform.position;
        timeDelayInput = 0;

        inputActions = new PlayerInputActions();
        inputActions.Player.walk.performed += Movement_performed;

    }
    private void Update()
    {
        PlayerMovement();

        if (toolPlayer.havePatient || toolPlayer.itemID != 0)
        {
            playerAnimator.SetBool("pick", true);
        }
        else 
        {
            playerAnimator.SetBool("pick", false);
        }

        timeDelayInput += Time.deltaTime;
    }
}