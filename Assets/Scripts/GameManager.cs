using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public float playerOneBattery = 0;
	public float playerTwoBattery = 0;

	public Slider playerOneSlider;
	public Slider playerTwoSlider;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		playerOneSlider.value = playerOneBattery;
		playerTwoSlider.value = playerTwoBattery;

	}
}
