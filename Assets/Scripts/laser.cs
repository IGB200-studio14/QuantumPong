using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour {

	public float speed = 10;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		move();
		batteryCollision();
	}

	void batteryCollision() {
		//if the laser goes past the battery on the right hand of the screen
		if (this.transform.position.x >= 12) {

			//update the batteries charge
			GameObject.Find("GameManager").GetComponent<GameManager>().playerOneBattery += 1;

			Destroy(this.gameObject);
		}
	}

	void move() {
		Vector3 position = this.transform.position;

		position.x += speed * Time.deltaTime;

		this.transform.position = position;
	}
}
