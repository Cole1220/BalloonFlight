using UnityEngine;
using System.Collections;

public class worldGenerate : MonoBehaviour {

	//Block levels
	GameObject blockade;
	public GameObject blockadeE; //easy
	public GameObject blockadeN; //normal
	public GameObject blockadeH1; //harder
	public GameObject blockadeH2; //harder variant
	public GameObject blockadeH3; //hardest

	//holds player object for location
	GameObject player;

	//variables
	float nextLevel = 10f;
	int generated = 1;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		blockade = blockadeE;
		//generate initial number
		generateStruct(5);
	}
	
	// Update is called once per frame
	void Update () {
		//advance dfficulties

		//easy -> normal
		//to be at 10
		if(generated > 5)
		{
			blockade = blockadeN;
		}

		//normal -> hard
		//to be at 25
		if(generated > 15)
		{
			int hard = Random.Range(0,2);

			if(hard <= 0)
			{
				blockade = blockadeH1;
			}
			else
			{
				blockade = blockadeH2;
			}
		}

		//hard -> harder
		//to be at 50
		if(generated > 35)
		{
			int hard = Random.Range(0,3);
			
			if(hard <= 0)
			{
				blockade = blockadeH1;
			}
			else if(hard > 0 && hard < 2 )
			{
				blockade = blockadeH2;
			}
			else
			{
				blockade = blockadeH3;
			}
		}
		//*/

		//make more when player gets closer
		if(player.transform.position.y > nextLevel - 30)
		{
			generateStruct(1); //generate 1
		}
	}

	//Generate level dependent on amount wanted for generation
	void generateStruct(int amount)
	{
		for(int x = 0; x < amount; x++)
		{
			//create prefab at location
			Instantiate(blockade, new Vector3(Random.Range(-15.0f, 15.0f),nextLevel,0),Quaternion.identity);
			
			//add to generated
			generated++;
			
			//Increment nextLevel height
			nextLevel = generated * 10;
		}
	}
}
