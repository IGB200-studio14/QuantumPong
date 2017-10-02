using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour {

	public float speed = 10;

	public int power = 1;
	public int health = 1;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//update power and health to be the same.
		power = health;

		this.transform.localScale = new Vector3(4.5f + (float)health,1.8f+ +(0.4f * (float)health));

		move();
		batteryCollision();
	}

	void batteryCollision() {
		//if the laser goes past the battery on the right hand of the screen
		if (this.transform.position.x >= 12) {

			//update the batteries charge
			GameObject.Find("GameManager").GetComponent<GameManager>().playerOneBattery += health;

			Destroy(this.gameObject);
		}else if (this.transform.position.x <= -12) {

			//update the batteries charge
			GameObject.Find("GameManager").GetComponent<GameManager>().playerTwoBattery += health;

			Destroy(this.gameObject);
		}
	}

	void move() {
		Vector3 position = this.transform.position;

		position.x += speed * Time.deltaTime;

		this.transform.position = position;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "laser") {
			//damage the enemy
			coll.gameObject.GetComponent<laser>().Damage(power);
			//damage ourselves, just in case the enemy can't
	//		Damage(coll.gameObject.GetComponent<laser>().power);
		}
	}

	public void Damage(int amt) {
		health -= amt;
		if (health <= 0) {
			Destroy(gameObject);
		}
	}

}
