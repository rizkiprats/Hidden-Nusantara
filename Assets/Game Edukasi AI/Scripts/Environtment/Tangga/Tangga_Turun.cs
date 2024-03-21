using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tangga_Turun : MonoBehaviour
{
    public GameObject TanggaNaik;
    public GameObject LantaiTingkat;
    void Start()
    {
        this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 7;
            if (collision.GetComponent<PlayerController>().moveup)
            {
                this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            }
            else if (collision.GetComponent<PlayerController>().movedown)
            {
                this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;

                TanggaNaik.GetComponent<BoxCollider2D>().isTrigger = false;
                LantaiTingkat.GetComponent<EdgeCollider2D>().isTrigger = true; 
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 7;
            JumpDisable();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            JumpDisable(); 
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            JumpDisable();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            JumpDisable();
        }
    }
    
    public void JumpDisable()
    {
        FindObjectOfType<PlayerController>().inputManager.OnJumpAction -= FindObjectOfType<PlayerController>().OnJump;
    }

    public void JumpEnable()
    {
        FindObjectOfType<PlayerController>().inputManager.OnJumpAction += FindObjectOfType<PlayerController>().OnJump;
    }
}