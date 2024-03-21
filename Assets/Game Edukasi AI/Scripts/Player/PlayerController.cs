using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[RequireComponent(typeof(Moveable))]
public class PlayerController : MonoBehaviour
{
    public InputManager inputManager;
    
    private float dir;
    //private float vertical_dir;

    private Moveable moveable;
    private Jump jumpable;
   
    public Animator anim;
    public AudioSource JumpAudio;

    public bool moveup;
    public bool movedown;

    private GameObject interactable;
    public UnityAction Interaction;
    private bool CanInteract = false;


    void Awake()
    {
        anim = GetComponent<Animator>();
        moveable = GetComponent<Moveable>();
        jumpable = GetComponent<Jump>();

        dir = transform.localScale.x;
    }

    void Update()
    {
        if (jumpable.checkGrounded())
        {
            anim.SetBool("Jump", false);
            anim.SetBool("Run", moveable.direction.x != 0);
        }

        anim.SetBool("Run", moveable.direction.x != 0);
    }
   
    private void OnSetDirection(Vector2 direction)
    {
        moveable.setDir(direction);

        if (direction.x != 0)
        {
            if (direction.x > 0)
                transform.localScale = new Vector3(dir, transform.localScale.y, transform.localScale.z);
            else if (direction.x < 0)
                transform.localScale = new Vector3(-dir, transform.localScale.y, transform.localScale.z);
        }

        anim.SetFloat("horizontalinput", direction.x);
    }

    private void OnUpAndDown(Vector2 direction) 
    {
        if (direction.y != 0)
        {
            if (direction.y > 0)
            {
                moveup = true;
                movedown = false;
            }
            else if (direction.y < 0)
            {
                movedown = true;
                moveup = false;
            }
        }
    }

    public void OnJump()
    {
        anim.SetBool("Jump", true);
        JumpAudio.Play();
        jumpable.jump(GetComponent<Rigidbody2D>());
    }


    private void OnEnable()
    {
        inputManager.OnMoveAction += OnSetDirection;
        inputManager.OnJumpAction += OnJump;
        inputManager.OnInteractAction += OnInteract;
        inputManager.OnUpAndDownAction += OnUpAndDown;
    }
    private void OnDisable()
    {
        inputManager.OnMoveAction -= OnSetDirection;
        inputManager.OnJumpAction -= OnJump;
        inputManager.OnInteractAction -= OnInteract;
        inputManager.OnUpAndDownAction -= OnUpAndDown;
    }

    public void OnInteract()
    {
        if (CanInteract)
        {
            if(interactable.GetComponent<ObjectDialogTrigger>() != null)
                interactable.GetComponent<ObjectDialogTrigger>().TriggerObjectDialog();
            if(interactable.GetComponent<QuestTrigger>() != null)
                interactable.GetComponent<QuestTrigger>().TriggerQuest();
        }
    }

    public void setInteract(bool x)
    {
        CanInteract = x;
        Debug.Log("Interact State: " + CanInteract);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Interact")
        {
            interactable = other.gameObject;
        }
    }

    //public void LoadData(GameDataSave data)
    //{
    //    this.transform.position = data.playerPosition;
    //}

    //public void SaveData(ref GameDataSave data)
    //{
    //    data.playerPosition = this.transform.position;
    //}
}