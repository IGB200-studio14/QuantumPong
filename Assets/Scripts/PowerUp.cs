using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

	public int id = 0;
	//should be only one(left, p1) or -1(right, p2)
	public int player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.localScale = new Vector3(10,10,0);
	}

	void reduceBatteryCharge() {
		Debug.Log("player: " + player);
		if (player == -1) {
			GameObject.Find("GameManager").GetComponent<GameManager>().playerOneBattery -= 5;
		}else if (player == 1) {
			GameObject.Find("GameManager").GetComponent<GameManager>().playerTwoBattery -= 5;
		}
	}

	public void OnCollisionEnter2D(Collision2D col) {
		//only do stuff if we collide with a laser
		if (col.gameObject.tag == "laser") {

			player = (int)(col.gameObject.GetComponent<laser>().speed / 10);

			//do different things according to it's id
			switch (id) {
				//reduce damage taken/charge given
				case 1:
					reduceBatteryCharge();
					break;

				default:
					break;
			}

			Destroy(col.gameObject);
			Destroy(this.gameObject);
		}
	}
}
