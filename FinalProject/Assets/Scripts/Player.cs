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
 * carried over to other levels if it was possible in this game. 
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
		get 
		{
			//This condition checks if the Player class
			//exists if it does not exist this condition is invoked
			if(_instance == null)
			{
				//construct a new Player object with
				//its default contructor
				//GameObject.DontDestroyOnLoad(gameObject);
				_instance = new Player ();
			}
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
			//value. This means when the game is reset/started to execute all over again that
			//value is still saved as a high score value and not get to reset back to zero.
			_highScore = PlayerPrefs.GetInt (key);
		}
		if (PlayerPrefs.HasKey (tkey1)) 
		{
			//Then stored this value in PlayerPrefs and store that value in the high 
			//score variable. Basically, the Player prefs is used to store like a persitant
			//value. This means when the game is reset/started to execute all over again that
			//value is still saved as a high score value and not get to reset back to zero.
			_lowestTimelvl1 = PlayerPrefs.GetFloat (tkey1);


		}
		if (PlayerPrefs.HasKey (tkey2)) 
		{
			//Then stored this value in PlayerPrefs and store that value in the high 
			//score variable. Basically, the Player prefs is used to store like a persitant
			//value. This means when the game is reset/started to execute all over again that
			//value is still saved as a high score value and not get to reset back to zero.
			_lowestTimelvl2 = PlayerPrefs.GetFloat (tkey2);
		}
	}

	//The HUDController class is added
	//This is for Player class to access the HUDController class 
	//in order to apply the HUDController's methods
	//It basically establishes a link a communication between 
	//Player and the HUDController class.
	public HUDController gameCtl;

	//setting up the score variable
	private int _score;

	//setting up the life variable
	private int _life;

	//setting up the high score variable
	private int _highScore = 0;

	//setting up the boolean has Mineral variable
	private bool _hasMineral;

	//setting up the lowest Time for level 1 variable
	private float _lowestTimelvl1 = 30;

	//setting up the lowest Time for level 2 variable
	private float _lowestTimelvl2 = 30;

	//setting up the timmer variable
	private float _timmer;

	private float _yourTime;

	//setting up the boolean has hasWon variable
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
		//setting the score to the new value
		set
		{
			_score = value;
			//update high score points as well
			HighScore = _score;

			//scoreLabel.text = "Score: " + _score;
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
		//reading the high score
		get 
		{
			return _highScore;
		}
		//setting the high score to the new value
		set
		{
			//only set of the current value
			//is greater than the current score
			if (value > _highScore)
			{
				_highScore = value;
				PlayerPrefs.SetInt (key, _highScore);
			}

			//highScoreLabel.text = "High Score: " + _highscore;
			//This will call the HUDController's
			//method called update UI in order to update 
			//the high score label
			//gameCtl.updateUI();
		}
	}

	//Player's public property
	//Using the get (read) and set property
	//to update the Timmer variable
	public float Timmer
	{
		//reading the timmer
		get 
		{
			return _timmer;
		}
		//setting the timmer to the new value
		set
		{
			//paused t value


			_timmer = value;
			//update Lowest Time points as well
			//LowestTime = _timmer;

			//timmerLabel.text = "Timmer: " + _timmer;
			//This will call the HUDController's
			//method called update UI in order to update 
			//the timmer label
			//gameCtl.EndUI();
		}
	}
	public float YourTime
	{
		//reading the timmer
		get 
		{
			return _yourTime;
		}
		//setting the timmer to the new value
		set
		{
			//paused t value


			_yourTime = value;
			//update Lowest Time points as well

		}
	}

	//Player's public property
	//Using the get (read) and set property
	//to update the Lowest Time variable
	public float LowestTime
	{
		//reading the Lowest Time
		get 
		{
			return _lowestTimelvl1;
		}
		//setting the Lowest Time to the new value
		set
		{


			if (_lowestTimelvl1 == 0) {
				
				_lowestTimelvl1 = 5000;
			
			}
			//only set of the current value
			//is greater than the current Lowest Time
			if (value < _lowestTimelvl1)
			{
				_lowestTimelvl1 = value;

				PlayerPrefs.SetFloat (tkey1, _lowestTimelvl1);
				//PlayerPrefs.SetFloat (tkey1, _lowestTimelvl1);
				//PlayerPrefs.Save ();
			}





			//LowestTimeLabel.text = "Lowest Time: " + _LowestTime;
			//This will call the HUDController's
			//method called end UI in order to update 
			//the Lowest Time label
			//gameCtl.endUI();
		}
	}
	public float LowestTimeTwo
	{
		//reading the Lowest Time
		get 
		{
			return _lowestTimelvl2;
		}
		//setting the Lowest Time to the new value
		set
		{
			//only set of the current value
			//is greater than the current Lowest Time
			if (value < _lowestTimelvl2)
			{
				_lowestTimelvl2 = value;
				PlayerPrefs.SetFloat (tkey2, _lowestTimelvl2);
			}

			//LowestTimeLabel.text = "Lowest Time: " + _LowestTime;
			//This will call the HUDController's
			//method called end UI in order to update 
			//the Lowest Time label
			//gameCtl.endUI();
		}
	}

	//Player's public property
	//Using the get (read) and set property
	//to update the life variable
	public int Life
	{
		//reading the life counter variable
		get 
		{
			return _life;
		}
		//setting up a new value for the life variable
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
