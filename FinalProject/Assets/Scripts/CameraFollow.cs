using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The CameraFollow.cs script
 * Alisa Ordina
 * id: 100967526
 * November 17, 2017
 * 
 * This script component is attached to Camera game object in the scene.
 * This following code allows to access the cameras's transformation component 
 * in order to assign the Dwarf game object's position to the camera's tranformation only x and y axis points. 
 * This allows for the camera to follow to the Dwarf game object in the scene.
 * In oredr to do that, this script camera follow script is attached to the camera game object. 
 * 
*/

public class CameraFollow : MonoBehaviour 
{

	//Declaire variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated Dwarf game object that is called target
	[SerializeField] Transform target = null;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () 
	{
		//In the update the tranform of the camera is applied to the Dwarf x and y axis position
		//in order to apply Dwarf game object's tranformation position of the x and y axis point and
		//camera z is assigned to the initial camera z point axis, because we are only using x and y axis for 2D game.
		//This is applied in order for the camera to follow/stay on to the Dwarf's game object's position.
		gameObject.transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

	}
}
