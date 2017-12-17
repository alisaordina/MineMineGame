using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The singlenton
 * Player.cs script
 * Alisa Ordina
 * id: 100967526
* November 17, 2017
 * 
 * This is a singleton class that will keep track of
 * score, mineral, lowesttime, time, hasWon and life counters. A singleton class means that
 * only a single Player class copy could exist in this system.
 * For example if the second level is reloaded the player class 
 * could still exist and cannot be intantiated again.
 * This class will take some resposibility of the HUDController
 * because of this class's capabilities. This class could be
 * carried over to other levels. 
 * So, this class is implemented in a way that cannot be instantiated 
 * any where else just inside this class. This is done by private constructor.
 * 
*/

public class Player 
{

	//private Player declaired under _instance variable
	static private Player _instance;

	//So, this class is implemented in a way that cannot be instantiated 
	//any where else just inside this class. This is done by private constructor.
	static public Player Instance
	{
		//read only
		get 
		{
			//This condition checks if the Player class
			//exists if it does not exist this condition is invoked
			if(_instance == null)
			{
				//construct a new Player object with
				//its default contructor
				_instance = new Player ();
			}

			//it the Player class does exists then,
			//return the Player's class intance
			return _instance;
		}
	}

	//setting up a static contant variable that stored in key and cannot be changed;
	private const string key = "HIGH_SCORE";


	//setting up a static contant variable that stored in key and cannot be changed;
	private const string tkey1 = "FIRSTLOWESTTIME";

	//setting up a static contant variable that stored in key and cannot be changed;
	private const string tkey2 = "SECONDLOWESTTIME";

	//Player default contructor
	private Player()
	{
		//Simply comparing/checking the static contant value that set in variable called key.
		if (PlayerPrefs.HasKey (key)) 
		{
			//Then stored this value in PlayerPrefs and store that value in the high 
			//score variable. Basically, the Player prefs is used to store like a persitant
			//storage variable. This means when the game is reset/started again and execute again all over that
			//value is still saved as a high score value and not get to reset back to zero.
			_highScore = PlayerPrefs.GetInt (key);
		}
		if (PlayerPrefs.HasKey (tkey1)) 
		{
			//Then stored this value in PlayerPrefs and store that value in the lowest 
			//time variable. Basically, the Player prefs is used to store like a persitant
			//storage variable. This means when the game is reset/started again and execute again all over that
			//value is still saved as a lowest time level 1, as _lowestTimelvl1 value and not get to reset back to zero.
			_lowestTimelvl1 = PlayerPrefs.GetFloat (tkey1);


		}
		if (PlayerPrefs.HasKey (tkey2)) 
		{
			//Then stored this value in PlayerPrefs and store that value in the lowest 
			//time variable. Basically, the Player prefs is used to store like a persitant
			//storage variable. This means when the game is reset/started again and execute again all over that
			//value is still saved as a lowest time level 2, as _lowestTimelvl2 value and not get to reset back to zero.
			_lowestTimelvl2 = PlayerPrefs.GetFloat (tkey2);
		}
	}

	//The HUDController class is added
	//This is for Player class to access the HUDController class 
	//in order to apply the HUDController's methods
	//It basically establishes a link a communication between 
	//Player and the HUDController class.
	public HUDController gameCtl;

	//private variable, setting up of the the score variable
	private int _score;

	//private variable, setting up the life variable
	private int _life;

	//private variable, setting up the high score variable
	private int _highScore = 0;

	//private variable, setting up the boolean has Mineral variable
	private bool _hasMineral;

	//private variable, setting up the lowest Time for level 1 variable
	private float _lowestTimelvl1 = 120;

	//private variable, setting up the lowest Time for level 2 variable
	private float _lowestTimelvl2 = 120;

	//private variable, setting up the timmer variable
	private float _timmer;

	//private variable, setting up the your time variable
	private float _yourTime;

	//private variable, setting up the boolean has hasWon variable
	private bool hasWon = false;



	//Player's public property
	//Using the get (read) and set property
	//to update the HasMineral variable
	public bool HasMineral 
	{
		get{ return _hasMineral; }
		set{ _hasMineral = value; }
	}


	//Player's public property
	//Using the get (read) and set property
	//to update the HasWon variable
	public bool HasWon 
	{
		get{ return hasWon; }
		set{ hasWon = value; }
	}

	//Player's public property
	//Using the get (read) and set property
	//to update the score variable
	public int Score
	{
		//reading the score
		get 
		{
			return _score;
		}

		//Setting the score to the new value
		set
		{
			_score = value;

			//Update high score points as well
			HighScore = _score;

			//ScoreLabel.text = "Score: " + _score;
			//This will call the HUDController's
			//method called update UI in order to update 
			//the score label
			gameCtl.updateUI();
		}
	}


	//Player's public property
	//Using the get (read) and set property
	//to update the high score variable
	public int HighScore
	{
		//Reading the high score
		get 
		{
			return _highScore;
		}
		//Setting the high score to the new value
		set
		{
			//Only set of the current value
			//is greater than the current score
			if (value > _highScore)
			{
				_highScore = value;
				PlayerPrefs.SetInt (key, _highScore);
			}
				
		}
	}

	//Player's public property
	//Using the get (read) and set property
	//to update the Timmer variable
	public float Timmer
	{
		//Reading the timmer
		get 
		{
			return _timmer;
		}

		//Setting the timmer to the new value
		set
		{

			//Setting up the timmer
			//in order to write to the timmer variable
			_timmer = value;

			//used previously
			//-----------------------------------------
			//update Lowest Time points as well
			//_lowestTimelvl1 = _timmer;
			//_lowestTimelvl2 = _timmer;

			//timmerLabel.text = "Timmer: " + _timmer;
			//This will call the HUDController's
			//method called update UI in order to update 
			//the timmer label
			//gameCtl.EndUI();
			//-----------------------------------------

		}
	}

	//Player's public property
	//Using the get (read) and set property
	//to update the YourTime variable
	public float YourTime
	{
		//Reading the timmer
		get 
		{
			return _yourTime;
		}
		//Setting the timmer to the new value
		set
		{

			//Setting up the yourTime
			//in order to write to the yourTime variable
			_yourTime = value;


		}
	}

	//Player's public property
	//Using the get (read) and set property
	//to update the Lowest Time variable in level one only
	public float LowestTime
	{
		//Reading the Lowest Time
		get 
		{
			return _lowestTimelvl1;
		}

		//Setting the Lowest Time to the new value
		set
		{

			//If the lowest time for level one does not exist
			//then set it up as a default value as 120 seconds as two minutes.
			if (_lowestTimelvl1 == 0) 
			{

				//Set it up as its initial/default value as 120 seconds as two minutes.
				_lowestTimelvl1 = 120;
			
			}

			//Only set of the current lowestTimelvl1 value
			//if it is less than previous value than the current Lowest Time
			//in the level one only
			if (value < _lowestTimelvl1)
			{
				
				//If it is true the the next lowest time value is less than
				//the previous lowest time value then set it up as its new 
				//lowest time value in level one only
				_lowestTimelvl1 = value;

				//Then store it in the memory as a persitant data for this level one in this game.
				PlayerPrefs.SetFloat (tkey1, _lowestTimelvl1);

			}

		}
	}

	//Player's public property
	//Using the get (read) and set property
	//to update the Lowest Time variable in level two only
	public float LowestTimeTwo
	{
		//Reading the Lowest Time in level 2
		get 
		{
			return _lowestTimelvl2;
		}

		//Setting the Lowest Time to the new value
		set
		{
			
			//If the lowest time for level two does not exist
			//then set it up as a default value as 120 seconds as two minutes.
			if (_lowestTimelvl2 == 0) 
			{

				//Set it up as its initial/default value as 120 seconds as two minutes.
				_lowestTimelvl2 = 120;

			}

			//Only set of the current lowestTimelvl2 value
			//if it is less than previous value than the current Lowest Time
			//in the level two only
			if (value < _lowestTimelvl2)
			{

				//If it is true the the next lowest time value is less than
				//the previous lowest time value then set it up as its new 
				//lowest time value in level two only
				_lowestTimelvl2 = value;

				//Then store it in the memory as a persitant data for this level
				//two lowest timmer variable value in this game.
				PlayerPrefs.SetFloat (tkey2, _lowestTimelvl2);
			}
				
		}
	}

	//used previously
	//-----------------------------------------
	/*public void resetLvl1Score()
	{
	
		PlayerPrefs.SetFloat (tkey1, 0);
	}

	public void resetLvl2Score()
	{
	
		PlayerPrefs.SetFloat (tkey2, 9999);
	}*/
	//-----------------------------------------


	//Player's public property
	//Using the get (read) and set property
	//to update the life variable
	public int Life
	{
		//Reading the life counter variable
		get 
		{
			return _life;
		}
		//Setting up a new value for the life variable
		set
		{
			_life = value;

			//If life has been dipleated
			if (_life <= 0) 
			{
				//If it is true
				//This will call the HUDController's
				//method called gameOver method in order to  
				//display the specific UI onto the scene
				gameCtl.DeadGameOver();
			} 
			else 
			{
				//If the life couter has not been dipleated
				//This will call the HUDController's
				//method called updateUI method in order to  
				//update the life label in the UI onto the scene
				gameCtl.updateUI ();
			}
		}
}
}
