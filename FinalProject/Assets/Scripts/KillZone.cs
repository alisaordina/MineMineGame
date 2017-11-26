using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The KillZone.cs script
 * Alisa Ordina
 * id: 100967526
 * November 18, 2017
 * 
 * This script component is attached to Killzone game object in the scene.
 * This following code allows the Dwarf game object to return to its spawn point position,
 * its initial position if the Dwarf game object has collided with the specific killzone's boundary axis point.
 * 
 * 
*/

public class KillZone : MonoBehaviour 
{



	//Declaire variable that would be accessible to Unity Inspector.
	//This variable is assigned as a Tranform to a designated game object that is called spawnPoint
	[SerializeField] Transform spawnPoint;

	//Checks if the collision between the killzone's boundary was crossed by the 
	//Dwarf's game object's collider2D 
	public void OnCollisionEnter2D(Collision2D other)
	{
		
		//If the Dwarf game object has crossed that killzone's boundary point 
		//then reset the Dwarf's position to the spawn position point/initial position point.

		other.gameObject.transform.position = spawnPoint.position;
	}
}
