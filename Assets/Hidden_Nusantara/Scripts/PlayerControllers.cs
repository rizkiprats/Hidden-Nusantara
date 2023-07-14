using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Audio;
[RequireComponent(typeof(Movable))]
public class PlayerControllers : MonoBehaviour/*, InterfaceDataSave*/
{
    public InputManager inputManager;
    private bool lastFaceLeft;
    private float dir;
    private Movable movable;
    private Animator anim;
    private Jumpable jumpable;
    private bool ifInteract = false;
    private bool CanInteract = false;
    public UnityAction Interaction;
    public Transform keyFollowPoint;
    public Key followingKey;
    public bool showDialog;
    private GameObject interactable;

    public AudioSource JumpAudio;

    void Awake()
    {
        anim = GetComponent<Animator>();
        movable = GetComponent<Movable>();
        jumpable = GetComponent<Jumpable>();
        dir = transform.localScale.x;
    }
      
    void Update()
    {
        if (jumpable.checkGrounded())
            
            anim.SetBool("isJump", false);

        anim.SetBool("isMoving", movable.direction.x != 0);


    }
    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Box")
        {
            if(jumpable.checkGrounded()){
                anim.SetBool("isPushing",true);
            }
            else{
                anim.SetBool("isPushing",false);
                anim.SetBool("isJump",false);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Box")
        {
            anim.SetBool("isPushing",false);
        }
    }
    private void OnSetDirection(Vector2 direction)
    {
        movable.setDir(direction);

        if (direction.x != 0)
        {
            if (direction.x > 0)
                transform.localScale = new Vector3(dir, transform.localScale.y, transform.localScale.z);
            else if (direction.x < 0)
                transform.localScale = new Vector3(-dir, transform.localScale.y, transform.localScale.z);
        }
        anim.SetFloat("horizontalInput", direction.x);
    }
    private void OnJump()
    {
        JumpAudio.Play();
        anim.SetBool("isJump", true);
        jumpable.jump(GetComponent<Rigidbody2D>());
    }

    public void OnInteract()
    {
       
        if (CanInteract)
        {
            interactable.GetComponent<DialogTrigger>().TriggerDialog();
        }
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

    public void setInteract(bool x)
    {
        CanInteract = x;
        Debug.Log("interact state: " + CanInteract);
    }
    //public void LoadData(GameDataSave data)
    //{
    //    this.transform.position = data.playerPosition;
    //}

    //public void SaveData(ref GameDataSave data)
    //{
    //    data.playerPosition = this.transform.position;
    //}
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Interact")
        {
            interactable =  other.gameObject;

        }
    }

}
