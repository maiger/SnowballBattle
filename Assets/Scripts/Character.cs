using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Character")]

    //[Header("General")]
    protected float moveSpeed;
    protected int health;

    [Header("Ground check")]
    public Transform groundCheckPoint;
    public float groundCheckRadius = 0.2f;
    public LayerMask whatIsGround;

    protected bool isGrounded;

    [Header("Audio")]
    public AudioClip hurtSound;
    protected AudioSource audioSrc;

    protected Rigidbody2D rb;
    protected Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSrc = GetComponent<AudioSource>();
    }

    public virtual void Initialize(int _health, float _moveSpeed)
    {
        health = _health;
        moveSpeed = _moveSpeed;
    }

    protected virtual void Update()
    {
        // Check if player is touching the ground.
        // NOTE: Could this be done with raycasting to be more efficient and customizable?
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);

        // Move character
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

        // Flip character based on movement direction
        if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Give updated parameter values to animator

        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("Grounded", isGrounded);
    }

    public virtual void takeDamage(int dmgAmount)
    {
        health -= dmgAmount;
        PlayAudio(audioSrc, hurtSound, 0.5f);
        if (health <= 0)
        {
            Die();
        }
    }

    protected void Die()
    {
        Destroy(gameObject);
    }

    protected void PlayAudio(AudioSource audioSrc, AudioClip audioClip, float volume)
    {
        audioSrc.clip = audioClip;
        audioSrc.volume = volume;
        audioSrc.Play();
    }
}
