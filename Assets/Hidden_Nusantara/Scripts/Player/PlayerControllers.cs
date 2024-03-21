using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movable))]
public class PlayerControllers : MonoBehaviour
{
    public InputManager inputManager;
    
    private float dir;
    private Movable movable;
    private Rigidbody2D rb;
    private Vector2 direction;
    private float direction_addedx;
    
    private Animator anim;
    private Jumpable jumpable;
   
    [SerializeField]private bool CanInteract = false;
    
    public Transform keyFollowPoint;
    public Key followingKey;
    
    public GameObject interactable;

    public AudioSource JumpAudio;
    public AudioSource RunAudio;

    public bool setPlayerNotMove;

    private Vector3 LastMove;

    [SerializeField] GameObject[] DialogPlayerRandomObjectiveUnfinish;
    [SerializeField] GameObject[] DialogPlayerRandomObjectivefinish;
    [SerializeField] GameObject[] DialogPlayerRandomHitBlockade;
    private int RandomDialogIndex;

    private int Random_reaction;

    public bool InteractPressed = false;

    bool Crouch = false;
    bool flashlightOn = false;

    public GameObject FlashLight;

    public GameObject interactableHint;

    void Awake()
    {
        anim = GetComponent<Animator>();
        movable = GetComponent<Movable>();
        jumpable = GetComponent<Jumpable>();
        rb = GetComponent<Rigidbody2D>();
        dir = transform.localScale.x;
    }
      
    void Update()
    {
        moveUsingMovable();

        if(interactableHint != null)
        {
            if(transform.localScale.x < 0)
            {
                RectTransform rect_trans = interactableHint.GetComponent<RectTransform>();
                rect_trans.localScale = new Vector3(-1, rect_trans.localScale.y, rect_trans.localScale.z);
            }
            if (transform.localScale.x > 0)
            {
                RectTransform rect_trans = interactableHint.GetComponent<RectTransform>();
                rect_trans.localScale = new Vector3(1, rect_trans.localScale.y, rect_trans.localScale.z);
            }

            if (interactable != null && InteractPressed == false)
            {
                interactableHint.SetActive(true);
            }
            else
            {
                interactableHint.SetActive(false);
            }
        }
    }

    private void OnSetDirection(Vector2 direction_added)
    {
        if (setPlayerNotMove)
        {
            direction_added.x = 0;
            direction_added.y = 0;
        }
        direction = direction_added;

        if(anim != null)
            anim.SetFloat("horizontalInput", direction_added.x);
    }
    private void OnJump()
    {
        if(jumpable != null)
        {
            if (jumpable.isGrounded)
            {
                if(JumpAudio != null)
                    JumpAudio.Play();
                if(anim != null)
                    anim.SetBool("isJump", true);
                if(jumpable != null)
                    jumpable.jump(GetComponent<Rigidbody2D>());
            }
        }   
    }

    private void OnInteract()
    {
        InteractPressed = !InteractPressed;

        if (CanInteract)
        {
            if (interactable != null)
            {
                if (interactable.tag == "Box" && InteractPressed)
                {
                    movable.speed -= 2f;
                }
                else if(interactable.tag == "Box" && !InteractPressed)
                {
                    movable.speed += 2f;
                }

                if (interactable.GetComponent<DialogTrigger>() != null)
                {
                    direction.x = 0;

                    interactable.GetComponent<DialogTrigger>().TriggerDialog();

                    InteractPressed = false;
                }  
            }
        }
        else
        {
            direction.x = 0;

            if(FindAnyObjectByType<ObjekCount>() != null)
            {
                ObjekCount objective_count = FindAnyObjectByType<ObjekCount>();

                if (objective_count.CheckObjectCount())
                {
                    RandomDialogIndex = Random.Range(0, DialogPlayerRandomObjectivefinish.Length);
                    DialogPlayerRandomObjectivefinish[RandomDialogIndex].GetComponent<DialogTrigger>().TriggerDialog();
                }
                else
                {
                    RandomDialogIndex = Random.Range(0, DialogPlayerRandomObjectiveUnfinish.Length);
                    DialogPlayerRandomObjectiveUnfinish[RandomDialogIndex].GetComponent<DialogTrigger>().TriggerDialog();
                }
            }
            InteractPressed = false;
        }
    }

    private void OnCrouch()
    {
        Crouch = !Crouch;
        if(anim != null)
        {
            anim.SetBool("isCrouch", Crouch);
        }

        if (Crouch)
        {
            movable.speed -= 2f;
        }
        else if(!Crouch)
        {
            movable.speed += 2f;
        }
    }

    private void OnFlashLight()
    {
        flashlightOn = !flashlightOn;
        if(FlashLight != null)
        {
            FlashLight.SetActive(flashlightOn);
        }
    }

    public void OnEnable()
    {
        inputManager.OnMoveAction += OnSetDirection;
        inputManager.OnJumpAction += OnJump;
        inputManager.OnInteractAction += OnInteract;
        inputManager.OnCrouchAction += OnCrouch;
        inputManager.OnFlashLightAction += OnFlashLight;
    }
    public void OnDisable()
    {
        inputManager.OnMoveAction -= OnSetDirection;
        inputManager.OnJumpAction -= OnJump;
        inputManager.OnInteractAction -= OnInteract;
        inputManager.OnCrouchAction -= OnCrouch;
        inputManager.OnFlashLightAction -= OnFlashLight;
    }

    public void setInteract(bool x)
    {
        CanInteract = x;
        //Debug.Log("Interact State: " + CanInteract);
    }

    public void PlayerSoundDisabled()
    {
        JumpAudio.Stop();
        RunAudio.Stop();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Blockade"))
        {
            RandomDialogIndex = Random.Range(0, DialogPlayerRandomHitBlockade.Length);
            DialogPlayerRandomHitBlockade[RandomDialogIndex].GetComponent<DialogTrigger>().TriggerDialog();

            PlayernotMove();
        }
    }

    public void PlayernotMove()
    {
        direction.x = 0;
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    private void moveUsingMovable()
    {
        movable.direction = Vector3.zero;

        direction_addedx = direction.x;

        //movable.direction.x = direction_addedx;

        if (setPlayerNotMove)
        {
            PlayerSoundDisabled();

            direction.x = 0;
            direction.y = 0;

            direction_addedx = 0;

            anim.Play("Idle");

            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }

        if (jumpable.isGrounded)
        {
            anim.SetBool("isJump", false);
            anim.SetBool("isInAir", false);

            movable.direction.x = direction_addedx;

            if (interactable != null && InteractPressed)
            {
                if (interactable.gameObject.CompareTag("Box") && transform.localScale.x > 0)
                {
                    if (movable.direction.x != 0)
                    {
                        if (movable.direction.x > 0)
                            transform.localScale = new Vector3(dir, transform.localScale.y, transform.localScale.z);
                        else if (movable.direction.x < 0)
                            transform.localScale = new Vector3(dir, transform.localScale.y, transform.localScale.z);
                    }
                }
                else if (interactable.gameObject.CompareTag("Box") && transform.localScale.x < 0)
                {
                    if (movable.direction.x != 0)
                    {
                        if (movable.direction.x > 0)
                            transform.localScale = new Vector3(-dir, transform.localScale.y, transform.localScale.z);
                        else if (movable.direction.x < 0)
                            transform.localScale = new Vector3(-dir, transform.localScale.y, transform.localScale.z);
                    }
                }
                else
                {
                    if (movable.direction.x != 0)
                    {
                        if (movable.direction.x > 0)
                            transform.localScale = new Vector3(dir, transform.localScale.y, transform.localScale.z);
                        else if (movable.direction.x < 0)
                            transform.localScale = new Vector3(-dir, transform.localScale.y, transform.localScale.z);
                    }
                }
            }
            else
            {
                if (movable.direction.x != 0)
                {
                    if (movable.direction.x > 0)
                        transform.localScale = new Vector3(dir, transform.localScale.y, transform.localScale.z);
                    else if (movable.direction.x < 0)
                        transform.localScale = new Vector3(-dir, transform.localScale.y, transform.localScale.z);
                }
            }
        }
        else
        {
            
            anim.SetBool("isInAir", true);
            RunAudio.Stop();
            movable.direction.x = LastMove.x;
            if (movable.direction.x != direction_addedx && direction_addedx != 0)
            {
                movable.direction.x = 0;
            }
        }

        LastMove = movable.direction;

        anim.SetBool("isMoving", movable.direction.x != 0);

        if (movable.direction.x == 0)
        {
            RunAudio.Stop();
        }
        else if (movable.direction.x != 0)
        {
            if (!RunAudio.isPlaying)
            {
                RunAudio.Play();
            }
        }

        if (interactable != null && InteractPressed)
        {
            if (interactable.gameObject.CompareTag("Box"))
            {
                if (jumpable.isGrounded)
                {
                    anim.SetBool("isPushing", true);
                }
            }
        }
        else
        {
            anim.SetBool("isPushing", false);
        }
    }
}