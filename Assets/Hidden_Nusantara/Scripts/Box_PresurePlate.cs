using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_PresurePlate : MonoBehaviour, InterfaceDataSave
{
    public static Box_PresurePlate instance;
    public bool Boxpressed;
    public bool byBox;

    [Header("Stuff to Control")]
    public Animator anim;
    public GameObject theboxes;
    public GameObject Nextlevel;
    public GameObject notif;
    public Animator doorAnim;
    public Vector3 theboxesPosition;

    public void Awake()
    {
        instance = this;
        doorAnim = Nextlevel.GetComponentInParent<Animator>();
        anim = this.gameObject.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Boxpressed)
        {
            theboxes.transform.position = theboxesPosition;
            // Nextlevel.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Nextlevel.gameObject.SetActive(Boxpressed);
       //notif.gameObject.SetActive(!Boxpressed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Box" || collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.tag == "Box")
                byBox = true;
            Boxpressed = true;
            doorAnim.SetBool("open", true);
            anim.SetBool("pressed", true);
            Nextlevel.SetActive(true);
            theboxesPosition = theboxes.transform.position;
            DataSaveManager.instance.SaveGame();
            // Nextlevel.gameObject.SetActive(!Boxpressed);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Box" || collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.tag == "Box")
                byBox = false;
            if (collision.gameObject.tag == "Player" && !byBox){
                Boxpressed = false;
                doorAnim.SetBool("open", false);
                anim.SetBool("pressed", false);
                theboxesPosition = theboxes.transform.position;
                DataSaveManager.instance.SaveGame();
            }
        }
    }

    public void LoadData(GameDataSave data)
    {
        this.Boxpressed = data.boxpressedlv3;
        this.theboxesPosition = data.Boxlv3Pos;
        
    }

    public void SaveData(ref GameDataSave data)
    {
        data.boxpressedlv3 = this.Boxpressed;
        data.Boxlv3Pos = this.theboxesPosition;

    }

}
