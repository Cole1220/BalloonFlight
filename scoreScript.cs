using UnityEngine;
using System.Collections;

public class scoreScript : MonoBehaviour {

	//Player score and gui
	int playerScore = 0;
	public GUIText score;

	// Use this for initialization
	void Start () {
		score.text = "Score: " + playerScore.ToString(); //set to zero essentially
	}
	
	// Update is called once per frame
	void Update () 
	{
		//score.text = "Score: " + playerScore.ToString(); //Update score


		if(GameObject.Find("Player").transform.position.y > (playerScore * 10) + 10)
		{
			playerScore += 1;
			score.text = "Score: " + playerScore.ToString(); //Update score (to only do when needed)
		}
	}

	//send score over to world script when dead
	void sendFinalScore()
	{
		GameObject.Find("worldScript").SendMessage("finalScoreGet", playerScore);
	}
	
}
