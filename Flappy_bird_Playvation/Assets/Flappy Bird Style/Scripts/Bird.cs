﻿using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour 
{
	public float upForce;					//Upward force of the "flap".
	private bool isDead = false;			//Has the player collided with a wall?

	private Animator anim;					//Reference to the Animator component.
	private Rigidbody2D rb2d;				//Holds a reference to the Rigidbody2D component of the bird.

  public float maxYPosition;      // The max height where the bird can jump
  float minYposition = -5.66f; // The minimum height where bird can go when is on "Ghost Mode"
	void Start()
	{
		//Get reference to the Animator component attached to this GameObject.
		anim = GetComponent<Animator> ();
		//Get and store a reference to the Rigidbody2D attached to this GameObject.
		rb2d = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		//Don't allow control if the bird has died.
		if (isDead == false) 
		{
			//Look for input to trigger a "flap" And the bird has to be visible on the screen AND the game has not to be paused.
			if (Input.GetMouseButtonDown(0) && gameObject.GetComponent<Transform>().position.y<maxYPosition && GameControl.instance.isPaused == false) 
			{
				//...tell the animator about it and then...
				anim.SetTrigger("Flap");
				//...zero out the birds current y velocity before...
				rb2d.velocity = Vector2.zero;
				//	new Vector2(rb2d.velocity.x, 0);
				//..giving the bird some upward force.
				rb2d.AddForce(new Vector2(0, upForce));

        // Each time the bird flaps the wing sound is played 
        FindObjectOfType<AudioManager>().Play("Wing");

      }
      // If the bird is under screen he died 
      if (gameObject.GetComponent<Transform>().position.y< minYposition) {
        isDead = true;
        GameControl.instance.BirdDied();
      }
    }
	}

	void OnCollisionEnter2D(Collision2D other)
	{
    // This condition allows to not call these lines below each time the bird collids with something after he died
    if (isDead == false) {
      // When the bird collides with something when he's alive the hit sound is played 
      FindObjectOfType<AudioManager>().Play("Hit");
      // Zero out the bird's velocity
      rb2d.velocity = Vector2.zero;
      // If the bird collides with something set it to dead...
      isDead = true;

      //...tell the Animator about it...
      anim.SetTrigger("Die");
      //...and tell the game control about it.
      GameControl.instance.BirdDied();
    }
    
	}
}
