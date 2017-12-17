using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Monster.cs script
 * Alisa Ordina
 * id: 100967526
 * November 17, 2017
 * 
 * This is a Monster class that will keep track of
 * Monster's life counter. 
 * This class will take some resposibility of the Monster's life
 * counter and reset life only when the game restarts again.
 * 
*/
public class Monster
{

	//The MonsterController class is added
	//This is for Monster class to access the MonsterController class 
	//in order to apply the MonsterController's methods
	//It basically establishes a link a communication between the
	//Monster class and the MonsterController class.
	public MonsterController monsterCtl;

	//used previously
	//--------------------------------------------------------------------------
	//private Player declaired under _instance variable
	/*static private Monster _instance;

	//So, this class is implemented in a way that cannot be instantiated 
	//any where else just inside this class. This is done by private constructor.
	static public Monster Instance
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
				_instance = new Monster ();
			}
			//return the Player's class intance
			return _instance;
		}
	}*/
	//--------------------------------------------------------------------------

	//The Monster constructor that contructs with the one parameter that takes the
	//MonsterController class.
	public Monster(MonsterController mCtl)
	{
		monsterCtl = mCtl;

	}

	//Setting up the life variable to its initial/default value that is 3
	private int _life = 3;

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


		}
	}

	//The Hurt method/function that
	//applied the updates to the life counter and 
	//calls the MonterController function when appropriate.
	//When the Monster gets 0 life counter than the MonterController
	//isPassed() function/method gets called
	public void Hurt ()
	{
	
		//Update life, decrease life by one
		_life--;

		//If life has been dipleated
		if (_life <= 0) 
		{
			//If it is true
			//This will call the HUDController's
			//method called gameOver method in order to  
			//display the specific UI onto the scene
			//gameCtl.KnockedOver();
			monsterCtl.PassedOut();
		} 
		else 
		{


			//previously used
			//----------------------------------------------
			//If the life couter has not been dipleated
			//This will call the HUDController's
			//method called updateUI method in order to  
			//update the life label in the UI onto the scene
			//gameCtl.updateUI ();
			//----------------------------------------------

		}
	}

	//reset function for the life counter
	public void MonsterLifeRst()
	{

		//Reset and set up the life variable to its initial/default value that is 3.
		_life = 3;

	}
}
