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

    // Update is called once per frame
    void Update()
    {


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
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
}
