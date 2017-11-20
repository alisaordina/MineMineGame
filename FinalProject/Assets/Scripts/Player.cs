using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
				_instance = new Player ();
			}
			//return the Player's class intance
			return _instance;
		}
	}
	//setting up a static contant variable that stored in kay and cannot be changed;
	private const string key = "HIGH_SCORE";

	//setting up a static contant variable that stored in kay and cannot be changed;
	private const string tkey = "TIMMER";

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
		if (PlayerPrefs.HasKey (tkey)) 
		{
			//Then stored this value in PlayerPrefs and store that value in the high 
			//score variable. Basically, the Player prefs is used to store like a persitant
			//value. This means when the game is reset/started to execute all over again that
			//value is still saved as a high score value and not get to reset back to zero.
			_lowestTime = PlayerPrefs.GetInt (tkey);
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

	private bool _hasMineral;

	private float _lowestTime;

	private float _timmer;

	private bool hasWon;

	//Player's public property
	//Using the get (read) and set property
	//to update the score variable
	public bool HasMineral 
	{
		get{ return _hasMineral; }
		set{ _hasMineral = value; }
	}
	public bool HasWon 
	{
		get{ return _hasMineral; }
		set{ _hasMineral = value; }
	}
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

	public float Timmer
	{
		//reading the score
		get 
		{
			return _timmer;
		}
		//setting the score to the new value
		set
		{
			_timmer = value;
			//update high score points as well
			LowestTime = _timmer;

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
	public float LowestTime
	{
		//reading the high score
		get 
		{
			return _lowestTime;
		}
		//setting the high score to the new value
		set
		{
			//only set of the current value
			//is greater than the current score
			if (value > _lowestTime)
			{
				_lowestTime = value;
				PlayerPrefs.SetFloat (tkey, _lowestTime);
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
				gameCtl.gameOver ();
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
