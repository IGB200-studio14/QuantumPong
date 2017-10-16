using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public float playerOneBattery = 0;
	public float playerTwoBattery = 0;

	public Slider playerOneSlider;
	public Slider playerTwoSlider;

	public float playerOneCharge = 10;
	public float playerTwoCharge = 10;

	public Slider playerOneChargeSlider;
	public Slider playerTwoChargeSlider;

	public float rechargeRate = 0.1f;

	public GameObject powerUp;

	//frames since the game started
	private int frames;

	//how many seconds till we spawn a powerup
	public int powerupTime = (int)(0.25 * 60);

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		frames++;

		playerOneSlider.value = playerOneBattery;
		playerTwoSlider.value = playerTwoBattery;

		playerCharge();

		powerUps();
	}

	void powerUps() {
		if (frames % (powerupTime * 60) == 0) {
			GameObject temp = Instantiate(powerUp, new Vector3(0,0,0), this.transform.rotation);
			//TODO get this to pick randomly from all possible powerups
			temp.GetComponent<PowerUp>().id = 1;
		}
		
	}

	void playerCharge() {
		if (playerOneCharge < 10) {
			//just guessing this value
			playerOneCharge += rechargeRate;
		}

		if (playerTwoCharge < 10) {
			playerTwoCharge += rechargeRate;
		}

		if (playerOneCharge < -1) {
			playerOneCharge = 0;
		}


		if (playerTwoCharge < -1) {
			playerTwoCharge = 0;
		}

		playerOneChargeSlider.value = playerOneCharge;
		playerTwoChargeSlider.value = playerTwoCharge;
	}

}
