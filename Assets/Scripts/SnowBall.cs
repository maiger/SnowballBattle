﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall : MonoBehaviour {

    public float ballSpeed;

    private Rigidbody2D theRB;

    public GameObject snowBallEffect;

	// Use this for initialization
	void Start () {
        theRB = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        theRB.velocity = new Vector2(ballSpeed * transform.localScale.x, 0);
	}

    // FIX: Bug, if character is jumping and moving sideways, snowball might collide with the character immediately
    // Move the spawnpooint further away from the player, or check if the snowball collided with the character who spawned the snowball.
    void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(snowBallEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
