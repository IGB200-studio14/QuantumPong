using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public float playerOneBattery = 0;
	public float playerTwoBattery = 0;

	public Slider playerOneSlider;
	public Slider playerTwoSlider;
	//the gameobjects that hold the sprite of the batteries
	public GameObject playerOneBatteryObject;
	public GameObject playerTwoBatteryObject;

	public float playerOneCharge = 10;
	public float playerTwoCharge = 10;

	public Slider playerOneChargeSlider;
	public Slider playerTwoChargeSlider;

	public float rechargeRate = 0.1f;

	public GameObject powerUp;

	System.Random rand = new System.Random();
	private int numPowerUps = 2;

	//frames since the game started
	private int frames;

	//how many seconds till we spawn a powerup
	public int powerupTime = (int)(0.25 * 60);

	public float batteryOffset = 0;

	// Use this for initialization
	void Start () {

		
	}

	// Update is called once per frame
	void Update () {
		frames++;

		playerOneSlider.value = playerOneBattery;
		playerTwoSlider.value = playerTwoBattery;

		if (playerOneBattery >= 100) {
			SceneManager.LoadScene("Victory");
		}else if (playerTwoBattery >= 100) {
			SceneManager.LoadScene("Victory");
		}

		playerCharge();

		powerUps();

		/*
		---------------
		Slider position
		---------------
		*/
		Vector3 p1BatteryPos = Camera.main.WorldToScreenPoint(playerOneBatteryObject.transform.position);
		p1BatteryPos.y = Camera.main.WorldToScreenPoint(new Vector3(0, 2, 0)).y;

		Vector3 p2BatteryPos = Camera.main.WorldToScreenPoint(playerTwoBatteryObject.transform.position);
		p2BatteryPos.y = Camera.main.WorldToScreenPoint(new Vector3(0, 2, 0)).y;

		playerOneSlider.transform.position = p1BatteryPos;
		playerTwoSlider.transform.position = p2BatteryPos;

		playerOneChargeSlider.transform.position = Camera.main.WorldToScreenPoint(new Vector3(-10,-6,0));
		playerTwoChargeSlider.transform.position = Camera.main.WorldToScreenPoint(new Vector3(10, -6, 0));
	}

	void powerUps() {
		if (frames % (powerupTime * 60) == 0) {
			GameObject temp = Instantiate(powerUp, new Vector3(0,0,0), this.transform.rotation);
			//TODO get this to pick randomly from all possible powerups
			temp.GetComponent<PowerUp>().id = rand.Next(1,numPowerUps + 1) ;
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
