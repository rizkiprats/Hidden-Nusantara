using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable_Object : MonoBehaviour
{
    public UnityAction interaction;
    public GameObject InteractButton;

    void Awake()
    {
        InteractButton.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().setInteract(true);
            InteractButton.SetActive(true);
            other.gameObject.GetComponent<PlayerController>().Interaction += interaction;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().setInteract(false);
            InteractButton.SetActive(false);
            other.gameObject.GetComponent<PlayerController>().Interaction -= interaction;
        }
    }
}