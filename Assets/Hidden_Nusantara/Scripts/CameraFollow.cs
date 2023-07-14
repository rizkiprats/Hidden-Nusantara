using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform Player;
    public float offset = 2f;
    public float speed = 1f;
    private float lookAhead;
    private float upperBound;
    private float lowerBound;
    private Camera cam;
    [SerializeField] private GameObject background;
    private float bgLeft;
    private float bgRight;
    private float camLeft;
    // Start is called before the first frame update
    void Start()
    {
        if (background == null)
        background = GameObject.Find("background");
        cam = GetComponent<Camera>();

        bgLeft = background.transform.position.x - background.GetComponent<SpriteRenderer>().bounds.size.x/2f;
        bgRight = background.transform.position.x + background.GetComponent<SpriteRenderer>().bounds.size.x/2f;
        float camWidth = 2f*cam.orthographicSize *cam.aspect;
        lowerBound = bgLeft + camWidth/2;
        upperBound = bgRight - camWidth/2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null)
            Player = GameObject.Find("Player").transform;
        lookAhead = Mathf.Lerp(lookAhead, (offset * (Player.localScale.x) / 1f), Time.deltaTime * speed);

        camLeft = cam.ScreenToWorldPoint(new Vector3(0,cam.pixelHeight,cam.nearClipPlane)).x;
        if (camLeft < bgLeft){
            transform.position = new Vector3(transform.position.x + Mathf.Abs(bgLeft - camLeft) + 1, transform.position.y, transform.position.z);
        }
        
        if (Player.position.x + lookAhead > upperBound){
            transform.position = new Vector3(upperBound, transform.position.y, transform.position.z);
        }
        else if (Player.position.x + lookAhead < lowerBound){
            transform.position = new Vector3(lowerBound, transform.position.y, transform.position.z);
        }
        else if (Player.position.x + lookAhead < upperBound && Player.position.x + lookAhead > lowerBound)
            transform.position = new Vector3(Player.position.x + lookAhead, transform.position.y, transform.position.z);
    }
}
