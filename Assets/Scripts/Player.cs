using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    [Header("General")]
    public string playerName = "Player";
    public int health = 5;

    [Header("Movement")]
    public float moveSpeed = 10;
    public float jumpForce = 30;

    [Header("Controls")]
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    public KeyCode jump = KeyCode.W;
    public KeyCode throwBall = KeyCode.Space;

    [Header("Ground check")]
    public Transform groundCheckPoint;
    public float groundCheckRadius = 0.2f;
    public LayerMask whatIsGround;

    public bool isGrounded;

    [Header("Throwing")]
    public GameObject snowBall;
    public Transform throwPoint;

    [Header("Audio")]
    public AudioSource throwSound;
    public AudioSource hurtSound;

    [Header("HUD")]
    public GameObject HUD;
    private GameObject HUDClone;

    private Rigidbody2D theRB;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

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

    // Update is called once per frame
    void Update()
    {

        // TODO: This could be done with raycasting, but would it be better?
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);

        if (Input.GetKey(left))
        {
            theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
        }
        else if (Input.GetKey(right))
        {
            theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
        }
        else
        {
            // TODO: Add surface based sliding
            theRB.velocity = new Vector2(0, theRB.velocity.y);
        }

        if (Input.GetKeyDown(jump) && isGrounded)
        {
            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
        }

        if (Input.GetKeyDown(throwBall))
        {
            GameObject ballClone = (GameObject)Instantiate(snowBall, throwPoint.position, throwPoint.rotation);
            ballClone.transform.localScale = transform.localScale;
            anim.SetTrigger("Throw");

            throwSound.Play();
        }

        // Flip character based on movement direction
        if (theRB.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (theRB.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        // Give updated parameter values to animator
        anim.SetFloat("Speed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("Grounded", isGrounded);
    }

    public void takeDamage(int dmgAmount)
    {
        health -= dmgAmount;
        hurtSound.Play();

        // Inform hud to decrease health
        HUDClone.GetComponent<HUDController>().SetHealth(health);
    }
}
