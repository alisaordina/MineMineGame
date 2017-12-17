using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * The SceneSwitch.cs script
 * Alisa Ordina
 * id: 100967526
 * November 18, 2017
 * 
 * This script component is attached to LevelTransition game object in the scene.
 * This following code allows the Dwarf game object when it has completed the game's objective
 * which is retrieve the rare mineral is to transition the sceen to the next level. 
 * This is done by Scene Management and its load new scene method that comes from SceneManagement's api.
 * 
 * 
*/
public class SceneSwitch : MonoBehaviour 
{


	//setting up the int level variable
	 static int level;



	//Checks if the collision between the SwitchScene's boundary collider was crossed by the 
	//Dwarf's game object's collider2D 
	public void OnTriggerEnter2D(Collider2D other)
	{
		//check to see if the object tag is equals to Player, the Dwarf game object's tag
		if (other.gameObject.tag.Equals ("Player")) 
		{
			//Check to see if the game's objection has been completed 
			//Check to see if the Dwarf character has a mineral
			//which is boolean to see if its true, to see if
			//Dwarf has found and picked up the rare mineral.
			if (Player.Instance.HasMineral) 
			{
				//If it is true then set boolean to won variable to true
				//in order to store the lowest time 
				Player.Instance.HasWon = true;

				//reset the hasMineral boolean to false
				Player.Instance.HasMineral = false;

				//If all these conditions are true
				//Then invoke StartScene function
				StartScene ();
			}

		}
	}

	//This function will set and switch the scene to the indicated/specified scene
	//in orderly manner
	public void StartScene()
	{

		//Get the integer of the index of the current scene
		//and store it into a level variable
		level = SceneManager.GetActiveScene().buildIndex;

		//increase the current level index by one.
		//This is in order to switch to the next index number for the next scene
		level++;

		//Check to see if its is within the index boundary
		//If for example it is index 4, then invoke this condition
		if (level > 4) 
		{
			//If it is true, and the index value is greater than 4
			//which means it is at the last index number. Then reset the index value to zero
			//To start from the begining of the scene's index number.
			level = 0;

			//Then load the start scene in at index number 0.
			SceneManager.LoadScene (level);
		} 

		//If it did not yet reach its last index scene number, then this condition gets invoked
		else 
		{
			//Then the scene Management will load the new scene either
			//with id that is related to the indicated scene, or by
			//the scene name. This specified scene's index number
			//will be loaded when this function is executed.
			//There can be additional parameter that allows to control the
			//way to load the scene in a specific way. By default here, it is unloading
			//one scene and loading another new scene by its index number on top it, of the previous scene.
			SceneManager.LoadScene (level);

		}

	}

	//If the user decides to go back and redo level one
	//This function is triggered which is attached to the button
	public void RedoLevelOne()
	{

		//Declaire the level acene index as 1
		//Where the level one scene exists
		level = 1;

		//Then load scene index under number 1
		//that contains level one scene game.
		SceneManager.LoadScene (level);
	}
}
