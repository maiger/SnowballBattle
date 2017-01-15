using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkAI : Character {

    void FixedUpdate()
    {
        float rayDist = 2f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * Mathf.Sign(rb.velocity.x), rayDist);
        //Debug.DrawRay(transform.position, Vector2.right * Mathf.Sign(rb.velocity.x) * rayDist, Color.red);
        if (hit.collider != null)
        {
            moveSpeed *= -1;
        }
        
    }
}
