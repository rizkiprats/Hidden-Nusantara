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
   
    private Animator anim;

    //public AudioSource JumpAudio;

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
        //vertical_dir = transform.localScale.y;

    }

    private void Start()
    {
        
    }

    void Update()
    {
        if (jumpable.checkGrounded())
        {
            anim.SetBool("Jump", false);
        }

        anim.SetBool("Run", moveable.direction.x != 0);
        //print("MoveUp : "+moveup);
        //print("MoveDown : " + movedown);
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

        if(direction.y == 0)
        {
            //moveup = false;
            //movedown = false;
        }

        if(direction.y != 0)
        {
            if(direction.y > 0)
            {
                moveup = true;
                movedown = false;
            }else if(direction.y < 0)
            {
                movedown = true;
                moveup = false;
            }
        }

    }
    private void OnJump()
    {
        anim.SetBool("Jump", true);
        jumpable.jump(GetComponent<Rigidbody2D>());
        //JumpAudio.Play();
        
    }


    private void OnEnable()
    {
        inputManager.OnMoveAction += OnSetDirection;
        inputManager.OnJumpAction += OnJump;
        inputManager.OnInteractAction += OnInteract;
    }
    private void OnDisable()
    {
        inputManager.OnMoveAction -= OnSetDirection;
        inputManager.OnJumpAction -= OnJump;
        inputManager.OnInteractAction -= OnInteract;
    }

    public void OnInteract()
    {
        if (CanInteract)
        {
            interactable.GetComponent<ObjectDialogTrigger>().TriggerObjectDialog();
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


    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Tangga")
    //    {
    //        if (vertical_dir > 0)
    //        {
    //            anim.SetBool("naiktangga", true);
    //        }
    //        else
    //        {
    //            anim.SetBool("naiktangga", false);
    //        }
    //    }
    //    else
    //    {
    //        anim.SetBool("naiktangga", false);
    //    }
    //}

    //private void OnCollisionEnter2D(Collision2D other)
    //{
    //    if (other.gameObject.tag == "Box")
    //    {
    //        //if (jumpable.checkGrounded())
    //        //{
    //        //    anim.SetBool("isPushing", true);
    //        //}
    //        //else
    //        //{
    //        //    anim.SetBool("isPushing", false);
    //        //    anim.SetBool("isJump", false);
    //        //}
    //    }
    //}


    //private void OnCollisionExit2D(Collision2D other)
    //{
    //    if (other.gameObject.tag == "Box")
    //    {
    //        //anim.SetBool("isPushing", false);
    //    }
    //}

    //public void LoadData(GameDataSave data)
    //{
    //    this.transform.position = data.playerPosition;
    //}

    //public void SaveData(ref GameDataSave data)
    //{
    //    data.playerPosition = this.transform.position;
    //}
}
