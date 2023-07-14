using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresurePlate : MonoBehaviour/*, InterfaceDataSave*/
{
    public static PresurePlate instance;
    public bool isOn;

    [Header("Stuff to Control")]
    public Animator anim;
    public GameObject theboxes;
    public GameObject notif;
    public Animator doorAnim;

    public void Awake()
    {
        instance = this;
        doorAnim = theboxes.GetComponentInParent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        theboxes.gameObject.SetActive(isOn);
        doorAnim.SetBool("open", isOn);
        notif.gameObject.SetActive(!isOn);
        anim.SetBool("pressed",isOn);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isOn = !isOn;
            if (isOn == true)
            {
                GetComponent<Collider2D>().enabled = false;
                DataSaveManager.instance.SaveGame();
            }
        }
    }

    //public void LoadData(GameDataSave data)
    //{
    //    this.isOn = data.isOn;
    //}

    //public void SaveData(ref GameDataSave data)
    //{
    //    data.isOn = this.isOn;

    //}

}
