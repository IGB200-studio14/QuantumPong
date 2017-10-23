using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

	public int id = 0;
	//should be only one(left, p1) or -1(right, p2)
	public int player;

	public Sprite Health;
	public Sprite Shield;

	SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer>();
		sr.sprite = null;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.localScale = new Vector3(10,10,0);
		//only if it has no sprite;
		//hacky way of doing this - should change this if we ever add more powerui
		if (sr.sprite == null) {
			switch (id) {
				case 1:
					sr.sprite = Health;
					break;
				case 2:
					sr.sprite = Shield;
					break;
				default:
					//just use the health as a default
					sr.sprite = Health;
					break;
			}

		}
	}

	void reduceBatteryCharge() {
		//Debug.Log("player: " + player);
		if (player == -1) {
			GameObject.Find("GameManager").GetComponent<GameManager>().playerOneBattery -= 5;
		}else if (player == 1) {
			GameObject.Find("GameManager").GetComponent<GameManager>().playerTwoBattery -= 5;
		}
	}

    void applyShield () {
        if (player == 1) {
            GameObject.Find("PlayerOne").GetComponent<Player>().giveShield();
        } else if (player == -1) {
            GameObject.Find("PlayerTwo").GetComponent<Player>().giveShield();
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

                case 2:
                    applyShield();
                    break;

				default:
					break;
			}

			Destroy(col.gameObject);
			Destroy(this.gameObject);
		}
	}
}
