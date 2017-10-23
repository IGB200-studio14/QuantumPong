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
    public float maxCharge = 10;
    public Vector3 largeLaserOffset = new Vector3(2.5f, 0, 0);
    public Vector3 laserOffset = new Vector3 (4, 0, 0);


    //the object we shoot
    public GameObject laser;  
    public float fireTime = 0.0f;
    public float fireRate = 0.5f;

    //how big the laser that we shoot is. defaults to 1
    public float charge = 1.0f;

    // Large Laser Offset cuttoff value
    public float laserChargeCuttOff = 4.0f;

    // Player Attributes
    public float resetHealth = 2.0f;
    public float health = 2.0f;
    public bool stunned;

    // Shield 
    public GameObject shield;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
            move();

            shoot();          
    }

    // Enables the shield in front of the player that moves with them
    public void giveShield() {
        this.transform.GetChild(0).gameObject.SetActive(true);

    }

    // Disbales the shield as it has been hit
    public void hitShield() {
        this.transform.GetChild(0).gameObject.SetActive(false);
    }


    // If player is hit by a big enough laser, they are stunned
    public void takeDamage(float damage) {
        health -= damage;

        // stuns the plyaer if the shot is big enough
        if (health <= 0) {
            stunned = true;
        }
        // resets health if the shot wasn't big enough to stun the player 
        else {
            health = resetHealth;
        }//end if


    }//end takeDamage


    // Shoots the laser 
    void shoot() {
        if (!stunned) {
            if (Input.GetKey(shootKey)) {
                if (right && GameObject.Find("GameManager").GetComponent<GameManager>().playerTwoCharge > 0) {
                    charge += 0.15f;
                    if (charge >= maxCharge) {
                        charge = maxCharge;
                    }

                }
                if (!right && GameObject.Find("GameManager").GetComponent<GameManager>().playerOneCharge > 0) {
                    charge += 0.15f;
                    if (charge >= maxCharge) {
                        charge = maxCharge;
                    }
                }

                if (right) {
                    GameObject.Find("GameManager").GetComponent<GameManager>().playerTwoCharge -= 0.16f;
                } else {
                    GameObject.Find("GameManager").GetComponent<GameManager>().playerOneCharge -= 0.16f;
                }
            }// end getkey

            if (Input.GetKeyUp(shootKey) && Time.time > fireTime) {
                
                if (charge > 0) {
                    // if it is a large laser charge, instantiate laser slightly further away so it doesnt hit the player casting it
                    if (charge >= laserChargeCuttOff) {
                        if (right) {
                            InstantiateLaser(largeLaserOffset);

                        } else {
                            InstantiateLaser(largeLaserOffset);

                        }
                        // smaller laser is instantiated slightly closer to player than larger laser 
                        // to make it feel smoother and consistant with large lasers
                    } else if (charge < laserChargeCuttOff) {
                        if (right) {
                            InstantiateLaser(laserOffset);

                        } else {
                            InstantiateLaser(laserOffset);
                        }
                    }

                }












                if (charge > 0) {
                    // instantiate laser slightly offset from player so it doesnt hit the player (or their shield) casting it

                    if (right) {
                        InstantiateLaser(laserOffset);
                    } else {
                        InstantiateLaser(laserOffset);
                    }

                    // Coodldown of shot
                    fireTime = Time.time + fireRate;
                }

            }// end getkeyup
        }// end Stunned
	} // END SHOOT()


    // Instantiates the laser with respect to which player is casting it, 
    // and offsets the laser to the correct side of the character respectively
    private void InstantiateLaser(Vector3 offset) {
        laser laserScript;
        
        //checks who is shooting and offsets the laser respectively
        if (right) {
            GameObject laserObject = Instantiate(laser, this.transform.position - offset, this.transform.rotation);
            laserScript = laserObject.GetComponent<laser>();
            laserScript.health = (int)charge;
        } else {
            GameObject laserObject = Instantiate(laser, this.transform.position + offset, this.transform.rotation);
            laserScript = laserObject.GetComponent<laser>();
            laserScript.health = (int)charge;
        }

        if (right) {
            //change the direction the lasers travel
            laserScript.speed = -laserScript.speed;
        }
        charge = 0;
    }// END INSTANTIATELASER()


    // --- Controls the player movement 
    // checks to see if player is stunned, if they aren't they have full control, 
    // otherwise they have zero control
	void move() {
		Vector3 position = this.transform.position;

        // Checks if the player should still be stunned
        if (position.y <= groundHeight) {
            stunned = false;
        }

		//accelerate
        if (!stunned) {
            if (Input.GetKey(upKey)) {
                if (right && GameObject.Find("GameManager").GetComponent<GameManager>().playerTwoCharge > 0) {
                    if (currentSpeed <= maxSpeed) {
                        currentSpeed += acceleration;
                        GameObject.Find("GameManager").GetComponent<GameManager>().playerTwoCharge -= 0.175f;

                    }
                }

                if (!right && GameObject.Find("GameManager").GetComponent<GameManager>().playerOneCharge > 0) {
                    if (currentSpeed <= maxSpeed) {
                        currentSpeed += acceleration;
                        GameObject.Find("GameManager").GetComponent<GameManager>().playerOneCharge -= 0.175f;

                    }
                }
            }
		} // end stunned

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
