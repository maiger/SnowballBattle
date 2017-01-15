using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall : MonoBehaviour {

    [Header("General")]
    public float ballSpeed;
    public int damageAmount = 1;

    public GameObject snowBallEffect;

    private Rigidbody2D rb;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        rb.velocity = new Vector2(ballSpeed * transform.localScale.x, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<Player>().takeDamage(damageAmount);
        } else if(other.tag == "Enemy")
        {
            other.GetComponent<Character>().takeDamage(damageAmount);
        }

        Instantiate(snowBallEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
