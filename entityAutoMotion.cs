using UnityEngine;
using System.Collections;

public class entityAutoMotion : MonoBehaviour {
	
	bool movingLeft = false; //direction to face - Left is true, Right is false
	public float moveSpeed = 5.0f; //movement speed

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		moveLR(); //continue checking facing direction 
	}

	//Moves bird and keeps trak of boundaries
	void moveLR()
	{
		//make sure never get stuck on left
		if(transform.position.x < -15)
		{
			transform.position = new Vector3(-15,transform.position.y,transform.position.z); //Make bird go to left boundary
			movingLeft = !movingLeft; //Make bird move in other direction
			//flipBird(); //Make bird face other direction


			if(transform.name == "Player")
			{
			flipBird();
			}
			 //*/
		}
		//make sure never get stuck on right
		if(transform.position.x > 15)
		{
			transform.position = new Vector3(15,transform.position.y,transform.position.z); //Make bird go to right boundary
			movingLeft = !movingLeft; //Make bird move in other direction
			//flipBird(); //Make bird face other direction

			if(transform.name == "Player")
			{
			flipBird();
			}
			 //*/
		}

		//move left if true
		if(movingLeft)
		{
			transform.position += new Vector3(1,0,0) * (-1 * moveSpeed) * Time.deltaTime; //move, over time, to left 
		}
		//move right if false
		else
		{
			transform.position += new Vector3(1,0,0) * moveSpeed * Time.deltaTime; //move, over time, to right
		}
	}

	//See if input was same as before - called from other script
	void setDir(bool leftMove)
	{
		//if new input is diff than prev input then change direction and flip
		if(movingLeft != leftMove)
		{
			movingLeft = leftMove;
			flipBird();
		}
	}

	//flips bird around to face right direction
	void flipBird()
	{
		transform.eulerAngles += new Vector3(0,180,0); //rotate bird on y axis 180 to flip around
	}
}
