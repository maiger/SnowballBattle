using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall : MonoBehaviour {

    public float ballSpeed;

    private Rigidbody2D rb;

    public GameObject snowBallEffect;

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
            other.GetComponent<Player>().takeDamage(1);
        }

        Instantiate(snowBallEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
