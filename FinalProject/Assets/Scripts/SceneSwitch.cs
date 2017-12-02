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
			//which is boolean to see if its true,to see if
			//Dwarf has aquired the rare mineral.
			if (Player.Instance.HasMineral) 
			{
				//set boolean to won variable to true
				//to store the lowest time 
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
	public void StartScene()
	{

		//Get the integer of the index of the current scene
		//andstore it into a level variable
		level = SceneManager.GetActiveScene().buildIndex;

		//increase the currentlevel index by one.
		//This is in order to switch to the next index numberforthe next scene
		level++;

		//Check to see if the index boundary
		//If for example it is index 4, invoke this condition
		if (level > 4) 
		{
			//If it is true, and the index value is greater then 3
			//which means it is at the last index number. Then reset the index value to zero
			//To start from the begining of the scene's index number.
			level = 0;
			//Load the start scene in at index number 0.
			SceneManager.LoadScene (level);
		} 
		else 
		{
			//Then the scene Management will load the new scene either
			//with id that is related to the indicated scene, or by
			//the scene name. This specified scene's index number
			//will be loaded when this function is executed.
			//There can be aditional parameter that allows to control the
			//way to load the scene in a specific way. By default here, it is unloading
			//one scene and loading another new scene by its index number on top it, of the previous scene.
			SceneManager.LoadScene (level);

		}


		//SceneManager.GetSceneAt (3);
	}
}
