using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The Trap.cs script
 * Alisa Ordina
 * id: 100967526
 * November 17, 2017
 * 
 * This script component is attached to Trap game object in the scene.
 * This following code gets called when the tag on collision is equal to
 * Player->Dwarf game object's tag in order to invoke intantiate/create 
 * the explosion game object and invoke the Dwarf game object controller script
 * and call Death function in order to invoke the death() method and Dwarf's Death animation
 * and to invoke the HUD Controller display to indicate to the user that the Dwarf
 * game object has died and the game is over.
 * 
*/

public class Trap : MonoBehaviour 
{

	//Declaire variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated game object that is called explosion
	[SerializeField] GameObject explosion = null;

	//Declaire variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated game object that is called HUDController
	//this controlls the canvas HeadUp Display
	[SerializeField] HUDController gameCtl = null;




	//On collision function is made in order to detect the other collider2D
	void OnTriggerEnter2D(Collider2D other)
	{
		
		//Check if the game object's tag is equals to the Player tag
		if (other.gameObject.tag.Equals ("Player")) 
		{
			
			//If the tag is equals to the player tag then access the tag's game object's
			//DwarfController script.
			DwarfController Dc = other.gameObject.GetComponent<DwarfController> ();


			//Check if the scrpt is assigned to this game object 
			//if it does contain it and it is not null this statement gets executed
			if (Dc != null) 
			{

				//invoke form Dwarf's controller script Death method/function
				Dc.Death ();

				//Create/intantiate an explosion onto the trap's game object's position 
				Instantiate (explosion).GetComponent<Transform> ().position = this.gameObject.GetComponent<Transform> ().position;

				//Invoke function DeadGameOver() method from the HUDCController script that is attached to canvas.
				gameCtl.GetComponent<HUDController> ().gameOver();

			}

		}
	}


}
