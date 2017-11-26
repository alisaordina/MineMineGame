using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The FireProjectile.cs script
 * Alisa Ordina
 * id: 100967526
 * November 20, 2017
 * 
 * This script component is attached to Platform game object in the prefab folder.
 * This following code applies tranformation allowing the 
 * Platfrom object to move it vertically up and down direction.
*/

public class PlafromController : MonoBehaviour 
{

	//Declaire public variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated vertical y axis speed of the platfrom's game object.
	[SerializeField] private float speed = 5f;

	//Declaire public variables that would be accessible to Unity Inspector.
	//This variable is assigned to a designated y axis start point
	//which is one of the boundary point.
	//This is the start y axis point that is a top 
	//boundary point, where the platfrom starts moving down
	//and stay within the bounds.
	[SerializeField] private float startY;

	//Declaire public variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated end y axis point called LowY
	//which is the bottom boundary point.
	//This is the end y axis point, which is a bottom boundary
	//where the platfrom resets and starts to move upwards.
	[SerializeField] private float LowY;

	//the current point of the platform y point to track its coordinates in y axis.
	[SerializeField] private float CurrentY;

	//This variable is from Unity the transform component 
	//The Trasform is defined in Unity as position, rotation and scale
	//Here it is going to be used for its coordinates, it is going to be used for the position.
	private Transform _transform;

	//The private Vector2 is declaired 
	//later to be used to simply 
	//to track this game object's position in y axis.
	private Vector2 _currentPosition;

	//the boolean value isGoingdown set to true.
	//later it is used to check the direction of the latform 
	//in order to invoke appropriate function when it is appropriate.
	private bool isGoingDown = true;




	// Use this for initialization
	void Start () 
	{

		//Here the set up of the Tranform. 
		//The Transform is accessed from this specific game object
		//which the script is attached to which is platform game object in the scene.
		//Basically, from this game object the Get Component is invoked which allows to 
		//access the Transform which is scale, rotation and its position.
		//This is set up so the specific methods could be applied to control 
		//this game object's position in the scene.
		_transform = gameObject.GetComponent<Transform> ();

		//The _currentPosition will track the game object's position
		//here from _transform the position is invoked to access the game 
		//object's position in x and y coordinates
		//Update the game object's position
		_currentPosition = _transform.position;


		//moveUp();


	}

	// Update is called once per frame
	void Update () 
	{


		//In the update function that is executed per frame,
		//this is where it is good idea to move this game object 
		//and track its x and y coordinates. This is where
		//the boundaries are applied in order to reset this game object when it 
		//has reached the ending x axis point and reset it to the starting x axis
		//point in order for this star game object to stay in the camera view.

		//The _currentPosition will track the game object's position
		//here from _transform the position is invoked to access the game 
		//object's position in x and y coordinates.
		//This tracks the game object's position and update its current position.
		_currentPosition = _transform.position;




		//To store the current update y point
		CurrentY = _currentPosition.y;

		//Check if the platfrom is moving downwards
		if (isGoingDown) 
		{
			//If it is true,if the platfrom starts to go down.
			//Then move the platfrom down by its indicated speed.
			_currentPosition -= new Vector2 (0, speed);
			//check to see if the platfrom has reached its boundary end y, LowY 
			//point
			if (_currentPosition.y < LowY) 
			{
				//If it is true and the platfrom has reached its boundary LowY
				//point then resetthe flag isGoingDown to false.
				isGoingDown = false;
			}

		} 
		else 
		{
			//If the platfrom is no longer going down then
			//movethe platfrom upwards + bythe indicated speed value.
			_currentPosition += new Vector2 (0, speed);

			//Check to see if the platfrom has reached its boundary upwards startY point
			if (_currentPosition.y > startY) 
			{
				//if it is true, and the platfrom has reached its startY boundary point
				//then reset the flag boolean isGoingDown to true.
				isGoingDown = true;
			}
		}

		//update the position
		_transform.position = _currentPosition;


		

	}





}
