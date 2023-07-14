using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityAction interaction;
    public GameObject InteractPanel;
    public GameObject InteractButton;
    void Awake()
    {
        InteractPanel.SetActive(false);
        InteractButton.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerControllers>().setInteract(true);
            InteractPanel.SetActive(true);
            InteractButton.SetActive(true);
            other.gameObject.GetComponent<PlayerControllers>().Interaction += interaction;

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerControllers>().setInteract(false);
            InteractPanel.SetActive(false);
            InteractButton.SetActive(false);
            other.gameObject.GetComponent<PlayerControllers>().Interaction -= interaction;
        }
    }

}
