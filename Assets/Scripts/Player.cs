using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character {
    [Header("__________________")]
    [Header("Player")]

    [Header("General")]
    public string playerName = "Player";

    [Header("Movement")]
    public float jumpForce = 30;

    [Header("Controls")]
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    public KeyCode jump = KeyCode.W;
    public KeyCode throwBall = KeyCode.Space;

    [Header("Throwing")]
    public GameObject snowBall;
    public Transform throwPoint;

    [Header("Audio")]
    public AudioClip throwSound;

    [Header("HUD")]
    public GameObject HUD;
    private GameObject HUDClone;

    public void Initialize(string _playerName, int _health, float _moveSpeed, float _jumpForce, KeyCode _left, KeyCode _right, KeyCode _jump, KeyCode _throwBall, Vector2 hudPos)
    {
        playerName = _playerName;
        health = _health;

        moveSpeed = _moveSpeed;
        jumpForce = _jumpForce;

        left = _left;
        right = _right;
        jump = _jump;
        throwBall = _throwBall;

        // Initialize HUD by instantiating correct hud prefab to correct location with correct info
        Debug.Log("Creating hud");
        Vector3 HUDPos = new Vector3(hudPos.x, hudPos.y, 0);
        HUDClone = (GameObject)Instantiate(HUD, HUDPos, Quaternion.identity);
        // Set parent to canvas so UI actually shows. Doing GameObject.Find is a bit slow so maybe change to tag search.
        HUDClone.transform.SetParent(GameObject.Find("Canvas").transform);
        HUDClone.GetComponent<HUDController>().Initialize(playerName, health);
    }

    // Overridden because player need some special cuddling
    protected override void Update()
    {
        // TODO: This could be done with raycasting, but would it be better?
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);

        CheckInputs();

        // Flip character based on movement direction
        if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        // Give updated parameter values to animator
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("Grounded", isGrounded);
    }

    void CheckInputs()
    {
        if (Input.GetKey(left))
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
        else if (Input.GetKey(right))
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
        else
        {
            // TODO: Add surface based sliding
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (Input.GetKeyDown(jump) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetKeyDown(throwBall))
        {
            GameObject ballClone = (GameObject)Instantiate(snowBall, throwPoint.position, throwPoint.rotation);
            ballClone.transform.localScale = transform.localScale;
            anim.SetTrigger("Throw");

            PlayAudio(audioSrc, throwSound, 1);
        }
    }

    // Overridden because we need to inform the hud unlike other characters
    public override void takeDamage(int dmgAmount)
    {
        health -= dmgAmount;
        PlayAudio(audioSrc, hurtSound, 0.5f);

        // Inform hud to decrease health
        HUDClone.GetComponent<HUDController>().SetHealth(health);
    }
}
