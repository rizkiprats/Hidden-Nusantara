using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Following : MonoBehaviour
{
    //// Start is called before the first frame update

    //[SerializeField] private Transform Player;
    //public float offset = 2f;
    //public float speed = 1f;
    //private float lookAhead;
    //private float upperBound;
    //private float lowerBound;
    //private Camera cam;
    //[SerializeField] private GameObject background;
    //private float bgLeft;
    //private float bgRight;
    //private float camLeft;

    //void Start()
    //{
    //    if (background == null)
    //        background = GameObject.Find("Background");
    //    cam = GetComponent<Camera>();

    //    bgLeft = background.transform.position.x - background.GetComponent<SpriteRenderer>().bounds.size.x / 2f;
    //    bgRight = background.transform.position.x + background.GetComponent<SpriteRenderer>().bounds.size.x / 2f;
    //    float camWidth = 2f * cam.orthographicSize * cam.aspect;
    //    lowerBound = bgLeft + camWidth / 2;
    //    upperBound = bgRight - camWidth / 2;

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (Player == null)
    //        Player = GameObject.Find("Player").transform;
    //    lookAhead = Mathf.Lerp(lookAhead, (offset * (Player.localScale.x) / 1f), Time.deltaTime * speed);

    //    camLeft = cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane)).x;
    //    if (camLeft < bgLeft)
    //    {
    //        transform.position = new Vector3(transform.position.x + Mathf.Abs(bgLeft - camLeft) + 1, transform.position.y, transform.position.z);
    //    }

    //    if (Player.position.x + lookAhead > upperBound)
    //    {
    //        transform.position = new Vector3(upperBound, transform.position.y, transform.position.z);
    //    }
    //    else if (Player.position.x + lookAhead < lowerBound)
    //    {
    //        transform.position = new Vector3(lowerBound, transform.position.y, transform.position.z);
    //    }
    //    else if (Player.position.x + lookAhead < upperBound && Player.position.x + lookAhead > lowerBound)
    //        transform.position = new Vector3(Player.position.x + lookAhead, transform.position.y, transform.position.z);

    //}

    //offset from the viewport center to fix damping
    public float m_DampTime = 10f;
    public Transform m_Target;
    public float m_XOffset = 0;
    public float m_YOffset = 0;

    private float margin = 0.1f;

    void Start()
    {
        if (m_Target == null)
        {
            
        }
    }

    void Update()
    {
        if (m_Target == null)
        {
            m_Target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (m_Target)
        {
            float targetX = m_Target.position.x + m_XOffset;
            float targetY = m_Target.position.y + m_YOffset;

            if (Mathf.Abs(transform.position.x - targetX) > margin)
                targetX = Mathf.Lerp(transform.position.x, targetX, 1 / m_DampTime * Time.deltaTime);

            if (Mathf.Abs(transform.position.y - targetY) > margin)
                targetY = Mathf.Lerp(transform.position.y, targetY, m_DampTime * Time.deltaTime);

            transform.position = new Vector3(targetX, targetY, transform.position.z);
        }
    }
}