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

	public string upKey = "up";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		move();
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
