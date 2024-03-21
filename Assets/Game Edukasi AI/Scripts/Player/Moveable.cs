using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour
{
    public Vector3 direction;
    [SerializeField] float speed;

    // Update is called once per frame
    void Update()
    {
        updatePos();
    }
    private void updatePos()
    {
        transform.position = getNextPos();
    }
    internal void setXDirection(float v)
    {
        direction.x = v;
    }
    internal void setYDirection(float v)
    {
        direction.y = v;
    }
    public Vector3 getNextPos()
    {
        return transform.position + newPos();
    }
    public Vector3 newPos()
    {
        return direction * Time.deltaTime * speed;
    }
    public void setDir(Vector3 dir)
    {
        direction = dir;
    }
    public void setDir(Vector2 dir)
    {
        direction.x = dir.x;
        direction.y = dir.y;
    }
}