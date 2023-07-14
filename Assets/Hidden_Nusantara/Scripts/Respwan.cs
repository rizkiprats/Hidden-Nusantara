using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respwan : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.Find("Player").transform.position = GameManager.instance.respawnPoint;
        }
    }
}
