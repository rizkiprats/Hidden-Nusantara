using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject InteractPanel;
    public GameObject InteractButton;

    void Awake()
    {
        if(InteractPanel != null)
            InteractPanel.SetActive(false);
        if(InteractButton != null)
            InteractButton.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (InteractPanel != null)
                InteractPanel.SetActive(true);
            if (InteractButton != null)
                InteractButton.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (InteractPanel != null)
                InteractPanel.SetActive(true);
            if (InteractButton != null)
                InteractButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (InteractPanel != null)
                InteractPanel.SetActive(false);
            if (InteractButton != null)
                InteractButton.SetActive(false);
        }
    }
}