using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, InterfaceDataSave
{

    public static Key instance;

    public bool isFollowing;

    public float followSpeed;

    public Transform followTarget;

    public GameObject InteractTxt;

    public void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (isFollowing)
        {
            PlayerControllers player = FindObjectOfType<PlayerControllers>();
            player.followingKey = this;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowing)
        {
            transform.position = Vector3.Lerp(transform.position, followTarget.position, followSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!isFollowing)
            {
                DataSaveManager.instance.SaveGame();
                PlayerControllers player = FindObjectOfType<PlayerControllers>();

                followTarget = player.keyFollowPoint;

                isFollowing = true;

                InteractTxt.gameObject.SetActive(!isFollowing);

                player.followingKey = this;
            }
            else
            {
                PlayerControllers player = FindObjectOfType<PlayerControllers>();

                followTarget = player.keyFollowPoint;

                isFollowing = true;

                InteractTxt.gameObject.SetActive(!isFollowing);

                player.followingKey = this;

            }
        }
    }

    public void LoadData(GameDataSave data)
    {
        this.followTarget.position = data.playerPosition;
        this.isFollowing = data.isFollowing;
    }

    public void SaveData(ref GameDataSave data)
    {
        //data.isFollowing = this.isFollowing;

    }




}
