using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * The HUDController.cs script
 * Alisa Ordina
 * id: 100967526
 * November 17, 2017
 * 
 * This script component is attached to Canvas game object in the scene.
 * This script will connect with UI interface and keeps track of user's points.
 * To be able to access these UI elements the UnityEngine.UI class is imported
 * by coding: using UnityEngine.UI
 * Since the following change of the scene is applied on to the button event
 * the ScneManagement is imported
 * by coding: using UnityEngine.SceneManagement
 * This following code applies updates to score label, life label,
 * Gave Over label, timmer label, lowest time label and button. This code in ables to apply
 * changes to the UI display and display specific labels 
 * when appropriate and apply an event to a button game object.
 * 
*/

public class HUDController : MonoBehaviour 
{

	//Declaire variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated game object that is called dwarf.

	[SerializeField] GameObject dwarf; 


	//Declaire variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated lifeLabel game object of the Canvas UI.
	[SerializeField] Text lifeLabel;

	//Declaire variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated scoreLabel game object of the Canvas UI.
	[SerializeField] Text scoreLabel;



	//Declaire variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated gameOverLabel game object of the Canvas UI.
	[SerializeField] Text gameOverLabel;

	//Declaire variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated highScoreLabel game object of the Canvas UI.
	[SerializeField] Text highScoreLabel;

	//Declaire variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated lowestTimeLabel game object of the Canvas UI.
	[SerializeField] Text lowestTimeLabel;

	//Declaire variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated resetBtn game object of the Canvas UI.
	[SerializeField] Button resetBtn;

	//declaring the maximum 30 seconds time when the countdown begins
	[SerializeField] private int CountDown = 30;

	//setting up the boolean flag Countup.
	[SerializeField] private bool CountUp = false;

	//Declaire variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated timerLabel game object of the Canvas UI.
	[SerializeField] Text timerLabel;

	//this label would store the paused time whenthe playerhas stopped playing
	private float pauseTime;

	//startTime this is depending whether the time starts at 0 counting up or counting down from 30 seconds.
	private float startTime;

	//setting up the isameover flag to false
	private bool isgameOvr = false;

	//setting up the isgameStart flag to false
	private bool isgameStart = false;

	//float t;



	//Declaire private variable
	//This variable is assigned to a designated AudioSource variable
	//private AudioSource _gameOverSound;

	/*private int _score = 0;

	private int _life = 3;

	public int Score
	{

		get 
		{
			return _score;
		}
		set
		{
			_score = value;

			scoreLabel.text = "Score: " + _score;
		}
	}

	public int Life
	{

		get 
		{
			return _life;
		}
		set
		{
			_life = value;

			if (_life <= 0) 
			{
				gameOver ();
			} 
			else 
			{
				lifeLabel.text = "Life: " + _life;
			}
		}
	}*/

	//This function is called at the start function
	//where it would display the score and the life labels.
	//This function will make certain labels in Active state
	//which means enabled state that appear on the camera view. 
	//While making other labels in non Active mode setting the
	//game object in disabled mode, 
	//which means that this game object labels will not
	//appear in the scene and not interact with any thing in the scene.
	private void initialize()
	{
		//call timmer function to restart when the game starts
		reStart ();

		//Creates, clones the enemy game object onto the
		//scene
		//set the Dwarf game object in active move meaning it is not disabled
		//and appears in the screen.
		//dwarf.gameObject.SetActive (true);

		//First setting up the score label to 0 value.
		Player.Instance.Score = 0;

		//First setting up the life label to a value of 3.
		Player.Instance.Life = 50;

		//First setting up the high score label to a value of 0.
		//Player.Instance.HighScore = 0;

		//Setting life label in Heads Up Display
		//in UI in active mode
		//An active means that this life label game object 
		//is enabled and is appeared in the scene.
		lifeLabel.gameObject.SetActive (true);

		//Setting score label in Heads Up Display
		//in UI in active mode
		//An active means that this score label game object 
		//is enabled and is appeared in the scene
		scoreLabel.gameObject.SetActive (true);

		//Setting timer label in Heads Up Display
		//in UI in active mode
		//An active means that this score label game object 
		//is enabled and is appeared in the scene
		timerLabel.gameObject.SetActive (true);

		//Setting lowest Time label in Heads Up Display
		//in UI in non-active mode
		//A non-active means that this GameOver label game object 
		//is disabled and it is not appeared in the scene
		lowestTimeLabel.gameObject.SetActive (false);


		//Setting Game Over label in Heads Up Display
		//in UI in non-active mode
		//A non-active means that this GameOver label game object 
		//is disabled and it is not appeared in the scene
		gameOverLabel.gameObject.SetActive (false);

		//Setting high score label in Heads Up Display
		//in UI in non-active mode
		//A non-active means that this high score label game object 
		//is disabled and it is not appeared in the scene
		highScoreLabel.gameObject.SetActive (false);

		//Setting up the button in Heads Up Display
		//in UI in non-active mode.
		//A non-active means that the button game object 
		//is disabled and it is not appeared in the scene
		resetBtn.gameObject.SetActive (false);


		//Since the initialize function 
		//has been called the enemy game object would be instantiated (created)  
		//onto the scene.
		//The coroutine function AddEnemy() would be called.
		//To add enemy game object onto the scene with random specified delayed seconds.
		//StartCoroutine ("AddEnemy");
	}

	public void gameOver()
	{
		

		//reset the hasMineral boolean value to false 
		Player.Instance.HasMineral = false;

		//If the tag is equals to the player tag then access the tag's game object's
		//DwarfController script
		DwarfController Dc = dwarf.gameObject.GetComponent<DwarfController> ();


		//Check if the scrpt is assigned to this game object 
		//if it does contain itandit is not null
		if (Dc!= null) 
		{


			//invoke form Dwarf's controller script Death method/function
			Dc.Death();
		}

		//if(_gameOverSound !=null)
		//{
		//_gameOverSound.Play ();
		//}

		//Setting life label in Heads Up Display
		//in UI in Non-active mode
		//Non-active means that this life label game object 
		//is disabled and it is not appeared in the scene.
		lifeLabel.gameObject.SetActive (false);

		//Setting score label in Heads Up Display
		//in UI in Non-active mode
		//Non-active means that this score label game object 
		//is disabled and it is not appeared in the scene
		scoreLabel.gameObject.SetActive (false);

		//Setting timer Label in Heads Up Display
		//in UI in Non-active mode
		//Non-active means that this score label game object 
		//is disabled and it is not appeared in the scene
		timerLabel.gameObject.SetActive (true);



		//timerLabel.text = minutes + ":" + seconds;

		//diactivate the Dwarf on the scene
		//Non-active means that this Dwarf game object 
		//is disabled and it is not appeared in the scene
		//dwarf.SetActive (false);

		//Setting game over label in Heads Up Display
		//in UI in active mode
		//An active means that this Game Over label game object 
		//is enabled and is appeared in the scene.
		gameOverLabel.gameObject.SetActive (true);

		//Setting high score label in Heads Up Display
		//in UI in active mode
		//An active means that this high score label game object 
		//is enabled and is appeared in the scene.
		highScoreLabel.gameObject.SetActive (true);

		//Setting lowest Time Label in Heads Up Display
		//in UI in active mode
		//An active means that this lowest Time Label game object 
		//is enabled and is appeared in the scene.
		lowestTimeLabel.gameObject.SetActive (true);

		//Call EndUI function
		EndUI();


		//Setting reset button in Heads Up Display
		//in UI in active mode
		//An active means that the button game object 
		//is enabled and is appeared in the scene.
		resetBtn.gameObject.SetActive (true);



	}

	//Update the following labels 
	public void EndUI()
	{
		//change the boolean value
		isgameOvr = true;

		//updating the score label
		highScoreLabel.text = "Highest Score: " +Player.Instance.HighScore;


		//pause ();
			//if the player lost the timmer will be stored for displaying purposes
			float endTime = Player.Instance.Timmer;


			//Parse the string into a more friendly display string that is more readle to the user
			//into a more disirable view.
			string minutes = ((int)endTime / 60).ToString ();
			string seconds = (endTime % 60).ToString ("f2");


			//store the time into a time label in the UI.
			timerLabel.text = "Time: " + minutes + ":" + seconds;


	}


	public void DeadGameOver()
	{

		//Setting life label in Heads Up Display
		//in UI in Non-active mode
		//Non-active means that this life label game object 
		//is disabled and it is not appeared in the scene.
		lifeLabel.gameObject.SetActive (false);

		//Setting score label in Heads Up Display
		//in UI in Non-active mode
		//Non-active means that this score label game object 
		//is disabled and it is not appeared in the scene
		scoreLabel.gameObject.SetActive (false);

		//Setting timer Label in Heads Up Display
		//in UI in Non-active mode
		//Non-active means that this score label game object 
		//is disabled and it is not appeared in the scene
		timerLabel.gameObject.SetActive (true);

		//Setting game over label in Heads Up Display
		//in UI in active mode
		//An active means that this Game Over label game object 
		//is enabled and is appeared in the scene.
		gameOverLabel.gameObject.SetActive (true);

		//Setting high score label in Heads Up Display
		//in UI in active mode
		//An active means that this high score label game object 
		//is enabled and is appeared in the scene.
		highScoreLabel.gameObject.SetActive (true);

		//Setting lowest Time Label in Heads Up Display
		//in UI in active mode
		//An active means that this lowest Time Label game object 
		//is enabled and is appeared in the scene.
		lowestTimeLabel.gameObject.SetActive (true);

		//Call EndUI function
		EndUI();


		//Setting reset button in Heads Up Display
		//in UI in active mode
		//An active means that the button game object 
		//is enabled and is appeared in the scene.
		resetBtn.gameObject.SetActive (true);


	}

	//This function will apply change to the life 
	//and score labels by referencing the player class's
	//public score and life properties. The Player class
	//keeps the track of the score and life counters.
	public void updateUI()
	{
		//Update the score label with its score's counter from 
		//the Player's public property which is score counter
		//that keeps track of the Bird_player's score counter.
		scoreLabel.text = "Score: " + Player.Instance.Score;

		//Update the life label with its life's counter variable from 
		//the Player's public property which is life counter
		//that keeps track of the Bird_player's life counter.
		lifeLabel.text = "Life: " + Player.Instance.Life +"% "; 



		/*if (t >= 0) 
		{

			string minutes = ((int)t / 60).ToString ();
			string seconds = (t % 60).ToString ("f2");

			timerLabel.text = "" + Player.Instance.Timmer + minutes + ":" + seconds;
		}*/

	}

	public void WinUI()
	{
		isgameOvr = true;
	}


	// Use this for initialization
	void Start () 
	{
		
		//This establishes the link between 
		//the HUDController class and between
		//the Player class. This way the HUDController could access 
		//the Player's public score and life counter variables properties. 
		//This only works because the Player class has access of the HUDCOntroller 
		//class that is noted in the Player script code.
		//Without that this would not work.
		//Then the Player has to be intantiated in order for the
		//HUDController to communicate the score and life variable.
		//So, here the Player class is introduced to the HUDController class.
		Player.Instance.gameCtl = this;

		//Here the set up of the Audio Source. 
		//This Audio Source Component is accessed from this specific game object
		//which the script is attached to Canvas game object in the scene.
		//Basically, from this game object the Get Component is invoked which allows to 
		//access the Audio Source.
		//This is set up, so the specific methods could be applied to control 
		//this game object's Audio Source and invoke Play method when appropriate.
		//	_gameOverSound = gameObject.GetComponent<AudioSource> ();

		//The initialize function is called to display the appropriate 
		//UI labels in Heads Up Display in the scene.
		initialize();

		//initialize the time get Time read only and store it in
		//the variable start time
		startTime = Time.time;

		//Check to see if it is Count up
		if (!CountUp) 
		{
			//if it is true and it is not
			//set as count up then sounted down
			//from updated counted down variable
			startTime += CountDown;
		}




	}

	// Update is called once per frame
	void Update ()
	{
		//declaire float t for storing the current time
		//for the UI display purposes and update the time
		float t;

		//check to see if the game has started.
		if (isgameStart) 
		{
			//if the game has started then initiate the time
			//Startthe time either counting up or counting down.
			reStart ();

			//reset the boolean isgamestart to false
			isgameStart = false;
		}
		//check to see if it is a coundown time 
		//by checking if the boolean countup is false
		if (!CountUp) 
		{
			//if it is true and it is not count up time
			//then subtract from start time 30 tothe current going time
			//and update the t variable for UI display purposes
			t = startTime - Time.time;
		} 
		else 
		{
			//if it is a count up time
			//then subtract from going time subtract the start time
			//and update the t variable for UI display purposes
			t = Time.time - startTime;
		}
		//checking if game is over

			//check to see if the player has won
			if (CountUp && Player.Instance.HasWon) 
			{
				pause (t);
				//if it is true and the player has won then update
				//the lowest time variable to the t variable
				Player.Instance.LowestTime = t;
				Player.Instance.YourTime = t;
				//reset hasWon variable
				Player.Instance.HasWon = false;
			}
			//check to see if the player has won
			if (!CountUp && Player.Instance.HasWon) 
			{
				pause (t);
				//if it is true and the player has won then update
				//the lowest time variable to the t variable
				//float previousT = Player.Instance.LowestTime;

				//Then store new t variable
				//Player.Instance.LowestTime = t;

				float TimeCompleted = CountDown - t;
				Player.Instance.LowestTimeTwo = TimeCompleted;
				Player.Instance.YourTime = TimeCompleted;
				//reset hasWon variable
				Player.Instance.HasWon = false;
			}
			//reset the isgameOver boolean variale
			

			//This detects if the time is paused
			if (pauseTime > 0) 
			{
				//exists method
				return;
			}


		

			if (t >= 0) 
			{
			
			//Parse the string into a more friendly display string that is more readle to the user
			//into a more disirable view.
				string minutes = ((int)t / 60).ToString ();
				string seconds = (t % 60).ToString ("f2");

			//update player intsance timmer variable
				Player.Instance.Timmer = t;
			//update timmer label in UI
				timerLabel.text = minutes + ":" + seconds;
			}
		}


	
		
	

	//This function is attached to the button gameobject
	//in order to invoke this appropriate event once the
	//button is clicked by the user.
	public void ResetBtnClick()
	{
		//This is simply creating a name of the current scene and reloading it
		//by invoking this function. This will get the active scene which 
		//is just the one we have here. It will reload it and get
		//the only scene that is stored in Unity. The start function would be called and
		//the inistialize function would be called 
		//Which will invoke the approapriate methods and the
		//game will start all over again with appropriate labels and its counters.
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);

	}

	void pause (float t)
	{
		//check to see if pause is equals to zero
		if (pauseTime == 0) 
		{
			//if it is then return the current paused time
			pauseTime = t;
		} 
		else 
		{
			//check tosee if the time is set to count up
			if (CountUp) 
			{
				//if it is a count up time
				//then subtract from going time subtract the start time
				//and update the t variable 
				startTime = Time.time - pauseTime;
			} 
			//if it is not count up then 
			//use the pause time
			else 
			{
				//Then use the pause time
				//to set the start time and resume counting down
				startTime = Time.time + pauseTime;
			}
			//set pause time to zero to resume timmer
			pauseTime = 0;
		}



	}

	void reStart () 
	{

		//reset the start time variable
		startTime = Time.time ;

		//chcek if it is count up time
		if (!CountUp) 
		{
			//resets the countdown timmer
			startTime += CountDown;
		}
	}
}




