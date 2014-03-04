using UnityEngine;
using System.Collections;

public class CamFollowBirdY : MonoBehaviour {

	//Didn't put camera in Player because then it would follow on X,Y, and Z (not just Y)
	GameObject bird; //Player Gameobject

	// Use this for initialization
	void Start () {
		//set bird to player gameobject
		bird = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		//Make camera.y set to bird.y 
		transform.position = new Vector3(0, bird.transform.position.y, -10);
	}
}
