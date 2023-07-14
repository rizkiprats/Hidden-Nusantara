using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public static Door instance;
    private PlayerControllers player;
    public bool doorOpen, WaitingToOpen;
    public GameObject Gate;
    public GameObject Interact;
    public Animator anim;
    public Animator doorAnim;
    // Start is called before the first frame update

    public void Awake()
    {
        instance = this;
        anim = GetComponentInChildren<Animator>();
        doorAnim = Gate.GetComponentInParent<Animator>();
    }
    void Start()
    {
        if (doorOpen)
        {
            anim.SetBool("open", true);
            doorAnim.SetBool("open",doorOpen);
            Gate.gameObject.SetActive(doorOpen);
            Key.instance.isFollowing = false;
            Interact.gameObject.SetActive(false);
            player = FindObjectOfType<PlayerControllers>();
            player.followingKey.gameObject.SetActive(false);
            player.followingKey = null;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (WaitingToOpen)
        {
            if (Vector3.Distance(player.followingKey.transform.position, transform.position) < 0.1f)
            {
                WaitingToOpen = false;
                doorOpen = true;
                doorAnim.SetBool("open",doorOpen);
                Gate.gameObject.SetActive(doorOpen);
                player.followingKey.gameObject.SetActive(false);
                Key.instance.isFollowing = false;
                player.followingKey = null;
                DataSaveManager.instance.SaveGame();
                anim.SetBool("open",true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerControllers>().followingKey != null)
            {
                collision.gameObject.GetComponent<PlayerControllers>().followingKey.followTarget = transform;
                player = collision.gameObject.GetComponent<PlayerControllers>();
                WaitingToOpen = true;
            }
        }
    }

    public void LoadData(GameDataSave data)
    {
        this.doorOpen = data.doorOpen;
    }

    public void SaveData(ref GameDataSave data)
    {
        data.isFollowing = false;
        //data.doorOpen = this.doorOpen;

    }


}
