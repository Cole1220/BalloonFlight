using UnityEngine;
using System.Collections;

public class playerInput : MonoBehaviour {

	float vertSpeed = 10.0f;//upward/jump speed

	float gravity = 5.0f; //rate of falling
	float termVel = 5.0f; //max fall speed
	Vector3 vel = Vector3.zero; //holder of grav
	float velY; //holder of grav

	public AudioClip jumpSound;
	public AudioClip dieSound;

	Vector3 moveDir = Vector3.zero; //player movement

	public LayerMask whatIsGround; //collision for dying

	bool startGame = false;//initial movement starts
	bool alive = true;//keep game running
	bool canJump = false;//only jump once at a time
	bool makeJump1 = false;//allow jump in fixed update
	bool makeJump2 = false;//allow jump in fixed update
	//bool useGrav = true;//whether to use grav or not

	bool canStart = false;
	
	// Use this for initialization
	void Start () {
		velY = vel.y;//set initial current velocity
		//PAUSE GAME
		GameObject.Find("worldScript").SendMessage("pauseGame", true);
		GameObject.Find("worldScript").SendMessage("startEnable", true);
	}
	
	// Update is called once per frame
	//player input better in update than fixedupdate
	void Update () 
	{
		//PLAYER INPUT ==================================================
		//flap left
		if(Input.GetKey(KeyCode.A) && canJump && alive && canStart)
		{
			//GameObject.Find("PlayerNew").SendMessage("flap");
			if(!startGame)
			{
				startTheGame();
			}
			canJump = false;
			makeJump1 = true;
		}

		//flap right
		if(Input.GetKey(KeyCode.D) && canJump && alive && canStart)
		{
			//GameObject.Find("PlayerNew").SendMessage("flap");
			if(!startGame)
			{
				startTheGame();
			}
			canJump = false;
			makeJump2 = true;
		}

		//Make sure key is let go to let jumping again
		if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
		{
			canJump = true;
		}
		//END PLAYER INPUT ==================================================
	}

	void FixedUpdate()
	{
		//if died in this update
		if(!alive)
		{
			audio.PlayOneShot(dieSound, .5f);
			if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down),.5f) || transform.position.y < -5)
			{
				audio.PlayOneShot(dieSound, .5f);
				GameObject.Find("Main Camera").SendMessage("switchMusic", true);
				GameObject.Find("worldScript").SendMessage("pauseGame", true);
				GameObject.Find("worldScript").SendMessage("restartEnable", true);
			}
		}

		//JUMP
		if(makeJump1)
		{
			ApplyJump(true);
		}
		if(makeJump2)
		{
			ApplyJump(false);
		}
		//END JUMP

		//Apply Gravity
		ApplyGravity();

		//Apply Changes
		transform.position += moveDir * Time.deltaTime;
	}

	//Add upward motion to move vector
	void ApplyJump(bool jumpDir)
	{
		//audio
		audio.PlayOneShot(jumpSound, .5f);
		//change direction
		SendMessage("setDir", jumpDir);

		//reset gravity
		vel = Vector3.zero;
		velY = vel.y;

		//change movedir to add later
		moveDir = new Vector3(0,1,0)* vertSpeed;

		//make jump not auto-happen next update
		makeJump1 = false;
		makeJump2 = false;
	}

	//Calculate downward motion to later add to move vector
	void ApplyGravity()
	{
		//do gravity
		velY = velY - gravity * Time.deltaTime;

		//Have Terminal Velocity
		velY = Mathf.Max(velY, -termVel);

		//set to vector to be added to movedir
		vel = new Vector3(0, velY, 0);  

		moveDir = moveDir + vel; //add downard velocity to move vector
	}

	//check collision
	void OnTriggerEnter(Collider hit)
	{
		//Debug.Log("hit: " + hit.gameObject.tag);
		if(hit.gameObject.tag == "Block")
		{
			alive = false;
		}
	}

	//initials for game start
	void startTheGame()
	{
		startGame = true;
		//first jump switches music
		GameObject.Find("Main Camera").SendMessage("switchMusic", false);
		//removes GUI
		GameObject.Find("worldScript").SendMessage("startEnable", false);
		GameObject.Find("worldScript").SendMessage("pauseGame", false);
	}

	//limiter of start dependent on othr script
	void setCanStart(bool yesNo)
	{
		canStart = yesNo;
	}
}
