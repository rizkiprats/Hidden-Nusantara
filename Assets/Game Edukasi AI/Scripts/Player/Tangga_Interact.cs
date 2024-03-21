using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tangga_Interact : MonoBehaviour
{
    public GameObject Tangga;
    public GameObject LantaiTingkat;
    
    //public GameObject Player;
    //private bool moveup;
    //private bool ladder;

    // Update is called once per frame
    void Update()
    {
        this.gameObject.tag = "Ground";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Tangga.GetComponent<BoxCollider2D>().isTrigger = true;

            if(LantaiTingkat != null)
            {
                LantaiTingkat.GetComponent<EdgeCollider2D>().isTrigger = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            JumpDisable();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            JumpEnable();
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