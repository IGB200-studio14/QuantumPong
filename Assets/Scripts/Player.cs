using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	//these will need to be changed according to player height
	public float groundHeight = -3;
	public float roofHeight = 4;
	
	//just guessing these, but they feel alright;
	public float acceleration = 2;
	public float gravity = 1;
	private float currentSpeed = 0;
	public float maxSpeed = 10;

	//the control scheme
	public string upKey = "up";
	public string shootKey = "right";
	//is the player on the left of the screen?
	public bool right;


	//the object we shoot
	public GameObject laser;

	//how big the laser that we shoot is. defaults to 1
	public float charge = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		move();
		shoot();
	}

	void shoot() {
		if (Input.GetKey(shootKey)) {
			charge += 0.1f;
		}

		if (Input.GetKeyUp(shootKey)) {
			GameObject laserObject = Instantiate(laser, this.transform.position, this.transform.rotation);
			laser laserScript = laserObject.GetComponent<laser>();
			//make sure we always give *some* charge
			if (charge < 1) {
				charge = 1;
			}
			laserScript.health = (int)charge;
			

			//if the player is on the right of the screen
			if (right) {
				//change the direction the lasers travel
				laserScript.speed = -laserScript.speed;
			}
			charge = 0;
		}
	}

	void move() {
		Vector3 position = this.transform.position;

		//accelerate
		if(Input.GetKey(upKey)) {
			if(currentSpeed <= maxSpeed) {
				currentSpeed += acceleration;
			}
		}

		//update the players position
		position.y += currentSpeed * Time.deltaTime;

		//apply gravity after we have moved, and only when we are above the ground
		if(position.y > groundHeight) {
			currentSpeed -= gravity;
		} else if(position.y <= groundHeight) {
			position.y = groundHeight;
			currentSpeed = 0;
		}

		//make the player not fall too fast but still feel heavy.
		if(currentSpeed < -maxSpeed - 5) {
			currentSpeed = -maxSpeed - 5;
		}

		//player hits the roof
		if(position.y >= roofHeight) {
			position.y = roofHeight;
		}	

		this.transform.position = position;
	}

}
