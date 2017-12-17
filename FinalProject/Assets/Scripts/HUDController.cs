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
 * Gave Over label, timmer label, lowest time label and button. This code applies
 * the changes to the UI display and display specific labels 
 * when appropriate and apply an event to a button game object.
 * 
*/

public class HUDController : MonoBehaviour 
{

	//Declaire variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated game object that is called dwarf.
	[SerializeField] GameObject dwarf = null; 

	//Declaire variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated lifeLabel game object of the Canvas UI.
	[SerializeField] Text lifeLabel = null;

	//Declaire variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated scoreLabel game object of the Canvas UI.
	[SerializeField] Text scoreLabel = null;

	//Declaire variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated gameOverLabel game object of the Canvas UI.
	[SerializeField] Text gameOverLabel = null;

	//Declaire variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated highScoreLabel game object of the Canvas UI.
	[SerializeField] Text highScoreLabel = null;

	//Declaire variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated lowestTimeLabel game object of the Canvas UI.
	[SerializeField] Text lowestTimeLabel = null;

	//Declaire variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated resetBtn game object of the Canvas UI.
	[SerializeField] Button resetBtn = null;

	//Declaire variable that would be accessible to Unity Inspector.
	//declaring the maximum 120 seconds time when the countdown begins
	[SerializeField] private int CountDown = 120;

	//Declaire variable that would be accessible to Unity Inspector.
	//setting up the boolean flag Countup.
	[SerializeField] private bool CountUp = false;

	//Declaire variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated timerLabel game object of the Canvas UI.
	[SerializeField] Text timerLabel = null;

	//this label would store the paused time when the player has stopped playing
	private float pauseTime;

	//startTime this is depending whether the time starts at 0 counting up or counting down from 120 seconds.
	private float startTime;

	//setting up the isameover flag to false
	private bool isgameOvr = false;

	//setting up the isgameStart flag to false
	private bool isgameStart = false;

	//This variable is assigned to a designated AudioSource variable
	//private AudioSource _gameOverSound;

	//This function is called at the start function
	//where it would display the score and the life labels.
	//This function will make certain labels in Active state
	//which means enabled state that appear on the camera view. 
	//While making other labels in non Active mode setting the
	//game object in disabled mode, 
	//which means that this game object labels will not
	//appear in the scene and will not interact in the scene.
	private void initialize()
	{

		//call timmer function to restart the timmer when the game starts
		reStart ();

		//First setting up the score counter from Player class from its public property, the score value to 0.
		//its initial value.
		Player.Instance.Score = 0;

		//First setting up the life counter from Player class from its public property, the life value to 50.
		Player.Instance.Life = 50;

		//Setting life label in Heads Up Display
		//in UI, in its active state.
		//An active state means that this life label game object 
		//is enabled and is appeared in the scene.
		lifeLabel.gameObject.SetActive (true);

		//Setting score label in Heads Up Display
		//in UI, in its active state.
		//An active state means that this score label game object 
		//is enabled and is appeared in the scene.
		scoreLabel.gameObject.SetActive (true);

		//Setting timer label in Heads Up Display
		//in UI, in its active state.
		//An active state means that this score label game object 
		//is enabled and is appeared in the scene.
		timerLabel.gameObject.SetActive (true);

		//Setting lowest Time label in Heads Up Display
		//in UI, in a non-active state.
		//A non-active state means that this GameOver label game object 
		//is disabled and it is not appeared in the scene.
		lowestTimeLabel.gameObject.SetActive (false);


		//Setting Game Over label in Heads Up Display
		//in UI in non-active state.
		//A non-active state means that this GameOver label game object 
		//is disabled and it is not appeared in the scene.
		gameOverLabel.gameObject.SetActive (false);

		//Setting high score label in Heads Up Display
		//in UI, in a non-active state.
		//A non-active state means that this high score label game object 
		//is disabled and it is not appeared in the scene.
		highScoreLabel.gameObject.SetActive (false);

		//Setting up the button in Heads Up Display
		//in UI, in a non-active state.
		//A non-active state means that the button game object 
		//is disabled and it is not appeared in the scene.
		resetBtn.gameObject.SetActive (false);
	}

	public void gameOver()
	{



		//reset the hasMineral boolean value to false 
		Player.Instance.HasMineral = false;

		//Access the Dwarf game object's component script under name DwarfController script 
		//and store it under variable Dc.
		DwarfController Dc = dwarf.gameObject.GetComponent<DwarfController> ();


		//Check if the script is assigned from the Dwarf game object 
		//if it does contain the script which means if it is not null
		if (Dc!= null) 
		{

			//If it istrue and contains the script
			//invoke form Dwarf's controller script Death method/function
			Dc.Death();

			//Then start coroutine that is called DiactivateAfterDeath()
			//which delays to diactivate the Dwarf game object in 3 seconds in order to show the 
			//full Dwarf's death animation, and then diactivate the Dwarf game object when appropriately.
			StartCoroutine(DiactivateAfterDeath());

		}

	
		//Setting life label in Heads Up Display
		//in UI, in a Non-active state.
		//A non-active state means that the life label game object 
		//is disabled and it is not appeared in the scene.
		lifeLabel.gameObject.SetActive (false);

		//Setting score label in Heads Up Display
		//in UI, in a Non-active state.
		//A non-active state means that the score label game object 
		//is disabled and it is not appeared in the scene.
		scoreLabel.gameObject.SetActive (false);

		//Setting timer Label in Heads Up Display
		//in UI, in a Non-active state.
		//A non-active state means that the timer Label game object 
		//is disabled and it is not appeared in the scene.
		timerLabel.gameObject.SetActive (true);

		//Setting game over label in Heads Up Display
		//in UI, in its active state.
		//An active state means that this game over label game object 
		//is enabled and is appeared in the scene.
		gameOverLabel.gameObject.SetActive (true);

		//Setting high score label in Heads Up Display
		//in UI, in its active state.
		//An active state means that this high score label game object 
		//is enabled and is appeared in the scene.
		highScoreLabel.gameObject.SetActive (true);

		//Setting lowest Time Label in Heads Up Display
		//in UI, in its active state.
		//An active state means that this lowest Time Label game object 
		//is enabled and is appeared in the scene.
		lowestTimeLabel.gameObject.SetActive (true);

		//Call EndUI function
		EndUI();

		//Setting reset button in Heads Up Display
		//in UI, in its active state.
		//An active state means that this reset button game object 
		//is enabled and is appeared in the scene.
		resetBtn.gameObject.SetActive (true);

	}

	//This is a Coroutine function which would add a delay in diactivating the Dwarf game object
	//in about 3 seconds. This is called in order to allow to play a full death/hurt animation and then diactivate the
	//Dwarf game object appropriately.
	IEnumerator DiactivateAfterDeath() 
	{
		//this will wait 3 seconds
		yield return new WaitForSeconds(3); 

		//This is setting up the Dwarf game object, 
		//in a Non-active state.
		//A non-active state means that the Dwarf game object 
		//is disabled and it is not appeared in the scene.
		dwarf.gameObject.SetActive (false);
	}

	//This function, updates the following labels 
	public void EndUI()
	{
		
		//change the boolean isgameOvr variable to true
		isgameOvr = true;


		//Update the label and display the high score counter in the UI label.
		highScoreLabel.text = "Highest Score: " +Player.Instance.HighScore;



			//If the player lost the timmer will be stored for displaying purposes
			float endTime = Player.Instance.Timmer;


			//Parse the string into a more friendly display string that is more readle to the user
			//into a more disirable view.
			string minutes = ((int)endTime / 60).ToString ();
			string seconds = (endTime % 60).ToString ("f2");


			//Store the minutes and seconds into a timer label in the UI.
			timerLabel.text = "Time: " + minutes + ":" + seconds;




	}




	public void DeadGameOver()
	{

		//Setting up the life label in Heads Up Display
		//in UI, in a non-active state.
		//A non-active state means that the life label game object 
		//is disabled and it is not appeared in the scene.
		lifeLabel.gameObject.SetActive (false);

		//Setting up the score label in Heads Up Display
		//in UI, in a non-active state.
		//A non-active state means that the score label game object 
		//is disabled and it is not appeared in the scene.
		scoreLabel.gameObject.SetActive (false);

		//Setting timer Label in Heads Up Display
		//in UI, in its active state.
		//An active state means that this timer Label game object 
		//is enabled and is appeared in the scene. 
		timerLabel.gameObject.SetActive (true);

		//Setting game over label in Heads Up Display
		//in UI, in its active state.
		//An active state means that this game over label game object 
		//is enabled and is appeared in the scene.
		gameOverLabel.gameObject.SetActive (true);

		//Setting high score label in Heads Up Display
		//in UI, in its active state.
		//An active state means that this high score label game object 
		//is enabled and is appeared in the scene.
		highScoreLabel.gameObject.SetActive (true);

		//Setting lowest Time Label in Heads Up Display
		//in UI, in its active state.
		//An active state means that this lowest Time Label game object 
		//is enabled and is appeared in the scene.
		lowestTimeLabel.gameObject.SetActive (true);

		//Reset the hasMineral boolean value to false 
		Player.Instance.HasMineral = false;

		//If the tag is equals to the player tag then access the tag's game object's
		//DwarfController script
		DwarfController Dc = dwarf.gameObject.GetComponent<DwarfController> ();


		//Check if the script is assigned from the Dwarf game object 
		//if it does contain the script which means if it is not null
		if (Dc!= null) 
		{


			//If it is true and contains the script
			//invoke form Dwarf's controller script Hurth method/function
			Dc.Hurt();

			//Then start coroutine that is called DiactivateAfterDeath()
			//which delays to diactivate the Dwarf game object in 3 seconds in order to show the 
			//full Dwarf's hurt animation, and then diactivate the Dwarf game object when appropriately.
			StartCoroutine(DiactivateAfterDeath());

		}


		//Call EndUI function
		EndUI();


		//Setting reset button in Heads Up Display
		//in UI in active state
		//An active means that the button game object 
		//is enabled and is appeared in the scene.
		resetBtn.gameObject.SetActive (true);


	}


	//This function will apply change to the life 
	//and score labels by referencing the player class in its
	//public properties that contains score and life properties. The Player class
	//keeps the track of the score and life counters.
	public void updateUI()
	{
		//Update the score label with its score's counter from 
		//the Player's public property which is score counter
		//that keeps track of the Dwarf's game object's score counter.
		scoreLabel.text = "Score: " + Player.Instance.Score;

		//Update the life label with its life's counter variable from 
		//the Player's public property which is life counter
		//that keeps track of the Dwarf's game object's life counter.
		lifeLabel.text = "Life: " + Player.Instance.Life +"% "; 

	}
		


	// Use this for initialization
	void Start () 
	{

		//Setting up the Dwarf game object 
		//in an active state
		//An active means that the Dwarf game object 
		//is enabled and is appeared in the scene.
		dwarf.gameObject.SetActive (true);

		//This establishes the link between 
		//the HUDController class and between
		//the Player class. This way the HUDController could access 
		//the Player's public score and life counter variables properties. 
		//This only works because the Player class has access of the HUDController 
		//class that is noted in the Player script code.
		//Without that this would not work.
		//Then the Player has to be intantiated in order for the
		//HUDController to communicate the score and life and other variables.
		//So, here the Player class is introduced to the HUDController class.
		Player.Instance.gameCtl = this;
	
		//The initialize function is called to display the appropriate 
		//UI labels in Heads Up Display in the scene.
		initialize();

		//Initialize the time get Time read only and store it in
		//the variable start time
		startTime = Time.time;

		//Check to see if it is Count up
		if (!CountUp) 
		{
			//if it is true and it is not
			//set as count up then count down
			//from updated count down variable
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
			//Start the timmer either counting up or counting down.
			reStart ();

			//reset the boolean isgamestart to false
			isgameStart = false;
		}

		//check to see if it is a coundown time 
		//by checking if the boolean countup is false
		if (!CountUp) 
		{
			//if it is true and it is not count up time
			//then subtract from start time 30 to the current going time
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

		//Checking if game is over in count up timmer
		if (CountUp && isgameOvr) 
		{

			//If it is true and game is over then call timmer's function pause because
			//the game is over.
			pause (t);

			//Parse the string into a more friendly display string that is more readle to the user
			//into a more disirable view.
			string minutes = ((int)Player.Instance.LowestTime / 60).ToString ();
			string seconds = (Player.Instance.LowestTime % 60).ToString ("f2");


			//Store the minutes and seconds into a lowest Time label in the UI.
			lowestTimeLabel.text = "Best Time: " +  minutes + ":" + seconds;


			//Reset boolean hasWon variable to false
			Player.Instance.HasWon = false;

			//Reset the boolean variable isgameOvr to false.
			isgameOvr = false;
		}

		//Checking if game is over in count down timmer
		if (!CountUp && isgameOvr) 
		{

			//If it is true and game is over then call timmer's function pause because
			//the game is over.
			pause (t);

			//Parse the string into a more friendly display string that is more readle to the user
			//into a more disirable view.
			string minutes = ((int)Player.Instance.LowestTimeTwo / 60).ToString ();
			string seconds = (Player.Instance.LowestTimeTwo % 60).ToString ("f2");

			//Store the minutes and seconds into a lowest Time label in the UI.
			lowestTimeLabel.text = "Best Time: " +  minutes + ":" + seconds;

		
			//Reset boolean hasWon variable to false
			Player.Instance.HasWon = false;

			//Reset the boolean variable isgameOvr to false.
			isgameOvr = false;
		}

		//check to see if the player has won when it is count up timmer
			if (CountUp && Player.Instance.HasWon) 
			{

				//If it is true and the user has won in this game by completing the game objection
				//then call timmer's function pause because
				//the game is over.
				pause (t);

				//if it is true and the player has won then update
				//the lowest time variable to the t variable
				Player.Instance.LowestTime = t;

				//Update the Timer variable as YourTime variable
				Player.Instance.YourTime = t;

				//Reset hasWon variable
				Player.Instance.HasWon = false;
			}

			//check to see if the player has won when it is count down timmer
			if (!CountUp && Player.Instance.HasWon) 
			{

				//If it is true and the user has won in this game by completing the game objection
				//then call timmer's function pause because
				//the game is over.
				pause (t);

				//Since it is count down we need to get the total time that the player has completed
				//the total time subtract their paused time
				float TimeCompleted = CountDown - t;
				
				//if it is true and the player has won then update
				//the lowest time Two for level two variable to the TimeCompleted variable
				Player.Instance.LowestTimeTwo = TimeCompleted;

				//Update the Timer variable as YourTime variable
				Player.Instance.YourTime = TimeCompleted;

				
				//Reset hasWon variable
				Player.Instance.HasWon = false;
			}
			

			//This detects if the time is paused
			if (pauseTime > 0) 
			{
				//exists method
				return;
			}


		
			//If the timmer is not paused
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

		//Check to see if it iscount down and the timmer has hit zero or less
		//then this statement get invoked
		if (!CountUp && t <= 0) 
		{
			 
			//Call method/function DeadGameOver()
			//in order to apply appropriate labels to the scene and pause the timmer.
			DeadGameOver ();

			//Parse the string into a more friendly display string that is more readle to the user
			//into a more disirable view.
			string minutes = ((int)Player.Instance.LowestTimeTwo / 60).ToString ();
			string seconds = (Player.Instance.LowestTimeTwo % 60).ToString ("f2");

			lowestTimeLabel.text = "Best Time: " +  minutes + ":" + seconds;

		}

		//Previously used
		//---------------------------------------------------------------------------------------
		/*if (CountUp && t >= 170) 
		{
			
			//pause (t);

			DeadGameOver ();
			//Parse the string into a more friendly display string that is more readle to the user
			//into a more disirable view.


			string minutes = ((int)Player.Instance.LowestTime / 60).ToString ();
			string seconds = (Player.Instance.LowestTime % 60).ToString ("f2");

			lowestTimeLabel.text = "Best Time: " +  minutes + ":" + seconds;
			/*update player intsance timmer variable
			Player.Instance.Timmer = t;
			//update timmer label in UI

			timerLabel.text = minutes + ":" + seconds;
		}*/
		//resetScore ();
		//---------------------------------------------------------------------------------------


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

	//Method/function that will pause the timmer
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
			//check to see if the time is set to count up
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

	//Method/function that will ReStart the timmer
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

	//Previously used
	//---------------------------------------------------------------------------------------
	/*
	private void resetScore ()
	{
		int lvl;
		lvl = SceneManager.GetActiveScene().buildIndex;

		if(Input.GetKeyUp(KeyCode.R))
		{

			if (lvl == 1) 
			{
				Player.Instance.resetLvl1Score();
			
			}
			if(lvl == 3)
			{
				Player.Instance.resetLvl2Score();
			}

		}

	}*/

	//---------------------------------------------------------------------------------------

}




