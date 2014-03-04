using UnityEngine;
using System.Collections;

public class ambientMusic : MonoBehaviour {

	//Provided variables
	public AudioClip title; //Title audio
	public AudioClip music; //Ambient game audio

	// Use this for initialization
	//Not needed because Audio source keeps default (initial) AudioClip
	void Start () {
		
	}
	
	// Update is called once per frame
	//AudioSource will be playing and looping music
	void Update () {
	
	}

	//Toggles music dependent on bool parameter
	void switchMusic(bool titlePlaying)
	{
		//If at title, play title song through AudioSource
		if(titlePlaying)
		{
			audio.clip = title;
		}
		//If false (not at title), play music through AudioSource
		else
		{
			audio.clip = music;
		}
		//Play it
		audio.Play();
	}
}
