using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public LayerMask groundLayer;
    public float jumpPower = 5f;

    public bool checkGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.5f;
        
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Ground" || hit.collider.gameObject.tag == "Tangga")
                return true;
            else
                return false;
        }
        else
        {
            return false;
        }
    }
    public bool canJump()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.5f;
        
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        
        if (checkGrounded())
            return true;
        else
            return false;
    }
    public void jump(Rigidbody2D rb)
    {
        if (canJump())
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
    }
}