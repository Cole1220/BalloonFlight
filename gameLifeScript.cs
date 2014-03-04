using UnityEngine;
using System.Collections;

public class gameLifeScript : MonoBehaviour {

	//Essentially provided pointers to gui
	//Start Screen
	public GUIText startGUI;
	public GUITexture startScreen;
	public GUIText goToCredGUI;

	//Dead Screen
	public GUIText restartGUI;
	public GUITexture endScreen;
	public GUIText finalScoreText;
	public GUIText highScores;
	public GUIText nameLabel;
	public GUIText actualScores;

	//Credit Screen
	public GUIText myJob;
	public GUIText myName;
	public GUIText AudioName;
	public GUIText AudioArtist1;
	public GUIText AudioArtist2;
	public GUIText returnGUI;
	//*/

	//Player Score and Name
	int finalScore;
	string playerName = "";

	//game bools
	bool isPaused = false;
	bool gameOver = false;
	bool once = true;
	bool clickable = true;

	// Use this for initialization
	void Start () {
		restartEnable(false); //remove all endgame GUI
		creditScreen(false); //remove all credit GUI
		SendMessage("callGet"); //get highscores update
	}
	
	// Update is called once per frame
	void Update () {

		//GAME MENU INPUT ===============================================
		//Restart Game
		if(Input.GetKey(KeyCode.R) && restartGUI.enabled == true)
		{
			//Application.LoadLevel("Balloon Flight - The Climb Up"); //Reload Level 
			Application.LoadLevel(Application.loadedLevel); //Reload Level (Not Hardcoded)
		}

		//Credit Screen
		if(Input.GetKey(KeyCode.C) && clickable)
		{
			clickable = false; //dont allow input
			//if start screen is up
			//check through is text says you can  go to credits
			if(goToCredGUI.enabled == true)
			{
				startEnable(false); //disable start GUI 
				creditScreen(true); //enable credit GUI
			}
			//if credit screen is up
			//check through is text says you can  go to main menu
			else if(returnGUI.enabled == true)
			{
				creditScreen(false); //disable credit GUI
				startEnable(true); //enable start GUI
			}
		}

		//dont allow input unless buttons are let go
		if(!Input.GetKey(KeyCode.C))
		{
			clickable = true;
		}
		//END GAME MENU INPUT ===============================================
	}

	//Pause Game on Death
	//if fed true game will pause, otherwise it will become or remain unpaused 
	void pauseGame(bool pause)
	{
		if(pause)
		{
			isPaused = true;
			Time.timeScale = 0;
		}
		else
		{
			isPaused = false;
			Time.timeScale = 1;
		}
	}

	//Turn on or off Start Menu
	void startEnable(bool startGame)
	{
		if(startGame)
		{
			startScreen.enabled = true;
			startGUI.enabled = true;
			goToCredGUI.enabled = true;
		}
		else
		{
			startGUI.enabled = false;
			startScreen.enabled = false;
			goToCredGUI.enabled = false;
		}
	}

	//Turn on or off End Game Menu
	void restartEnable(bool restartGame)
	{
		gameOver = restartGame;
		if(restartGame)
		{
			GameObject.Find("worldScript").SendMessage("sendFinalScore");

			endScreen.enabled = true;
			finalScoreText.enabled = true;
			restartGUI.enabled = true;
			highScores.enabled = true;
			nameLabel.enabled = true;
			actualScores.enabled = true;
		}
		else
		{
			restartGUI.enabled = false;
			finalScoreText.enabled = false;
			endScreen.enabled = false;
			highScores.enabled = false;
			nameLabel.enabled = false;
			actualScores.enabled = false;
		}
	}

	//Turn on or off Credit Screen 
	void creditScreen(bool creds)
	{
		if(creds)
		{
			GameObject.Find("Player").SendMessage("setCanStart", false);
			myJob.enabled = true;
			myName.enabled = true;
			AudioName.enabled = true;
			AudioArtist1.enabled = true;
			AudioArtist2.enabled = true;
			returnGUI.enabled = true;
			endScreen.enabled = true;
		}
		else
		{
			myJob.enabled = false;
			myName.enabled = false;
			AudioName.enabled = false;
			AudioArtist1.enabled = false;
			AudioArtist2.enabled = false;
			returnGUI.enabled = false;
			endScreen.enabled = false;
			GameObject.Find("Player").SendMessage("setCanStart", true);
		}
	}

	//Bring up Player Input for submission
	void OnGUI()
	{
		if(gameOver)
		{
			playerName = GUI.TextField(new Rect((Screen.width/2)-150,(Screen.height/2)-25,100,25),playerName,20); //Create text input box
			//if the submit button is clicked
			if(GUI.Button (new Rect ((Screen.width/2)-150,(Screen.height/2)+75,100,25), new GUIContent ("Submit", "Send to Highscore Board")))
			{
				//cant send multiple parameters to another script so split into 2 functions
				SendMessage("setNameP",playerName); //send name
				SendMessage("setScoreP", finalScore); //send score

				//Disable everything
				restartGUI.enabled = false;
				gameOver = false;
				nameLabel.enabled = false;
				finalScoreText.enabled = false;

				//allow player to restart game
				resetGame();
			}
		}
	}

	//Update player final score GUI
	void finalScoreGet(int score)
	{
		finalScore = score;
		finalScoreText.text = "Score: " + finalScore.ToString();
	}

	//Allow game to reset
	//Intending to make automatic, but enough time needs to be given so score gets submitted
	void resetGame()
	{
		////Application.LoadLevel("flappyBird");
		restartGUI.enabled = true;
	}
}
