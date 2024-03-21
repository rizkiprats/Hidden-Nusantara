using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn_Player : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.Find("Player").transform.position = Game_Manager.instance.respawnPoint;
        }
    }
}