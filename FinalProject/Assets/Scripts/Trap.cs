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
 * This following code gets called when the tag on collision is eaqual to
 * Player->Dwarf game object's tag in orderto invoke intantiate/create 
 * the explosion game object and invoke the Dwarf game objectcontroller script
 * and call Death function in orderto invoke the death() and Dwarf's Death animation
 * andto invoke the HUD Controller display to indicate to the user that the Dwarf
 * game object has died and game is over.
 * 
*/

public class Trap : MonoBehaviour 
{

	//Declaire variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated game object that is called explosion
	[SerializeField] GameObject explosion =null;

	//Declaire variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated game object that is called HUDController
	//this controlls the canvas HeadUp Display
	[SerializeField] HUDController gameCtl;


	//On collision function to detect the other collider2D
	void OnTriggerEnter2D(Collider2D other)
	{

		//Check if the ame object's tag is equals ti the Player tag
		if (other.gameObject.tag.Equals ("Player")) 
		{

			//If the tag is equals to the player tag then access the tag's game object's
			//DwarfController script
			DwarfController Dc = other.gameObject.GetComponent<DwarfController> ();


			//Check if the scrpt is assigned to this game object 
			//if it does contain itandit is not null
			if (Dc != null) 
			{

				//Create/intantiate an explosion onto the trap's game object's position 
				Instantiate (explosion).GetComponent<Transform> ().position = this.gameObject.GetComponent<Transform> ().position;

				//invoke form Dwarf's controller script Death method/function
				Dc.Death ();

				//Invoke function DeadGameOver() method from the HUDCONtrollerscript that is attached to canvas.
				gameCtl.GetComponent<HUDController> ().DeadGameOver ();



			}

		}
	}


}
