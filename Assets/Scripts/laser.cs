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
	}

	void move() {
		Vector3 position = this.transform.position;

		position.x += speed * Time.deltaTime;

		this.transform.position = position;
	}
}
