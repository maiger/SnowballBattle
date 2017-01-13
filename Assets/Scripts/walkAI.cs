using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkAI : MonoBehaviour {

    [Header("General")]
    public float speed;
    public int health;

    [Header("Ground check")]
    public Transform groundCheckPoint;
    public float groundCheckRadius = 0.2f;
    public LayerMask whatIsGround;

    public bool isGrounded;

    private Rigidbody2D rb;
    private Animator anim;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
	
	void Update () {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);

        rb.velocity = new Vector2(-speed, rb.velocity.y);

        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("Grounded", isGrounded);
    }

    public void takeDamage(int dmgAmount)
    {
        health -= dmgAmount;
        //PlayAudio(audioSrc, hurtSound, 1);
        if(health <= 0)
        {
            Die();
        }

    }

    void Die()
    {
        Destroy(gameObject);
    }
}
