using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tangga_Interact : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Tangga;
    public GameObject LantaiTingkat;
    //public GameObject Player;
    //private bool moveup;
    //private bool ladder;

    void Start()
    {
        //moveup = Player.GetComponent<PlayerController>().moveup;
        //Direction.GetComponent<BoxCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //moveup = Player.GetComponent<PlayerController>().moveup;
        //print(moveup);
        //if (ladder & moveup)
        //{
        //    Direction.GetComponent<BoxCollider2D>().enabled = true;
        //}
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

}
