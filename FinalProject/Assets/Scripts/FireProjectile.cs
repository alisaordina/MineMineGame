using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The FireProjectile.cs script
 * Alisa Ordina
 * id: 100967526
* November 20, 2017
 * 
 * This script component is attached to Fire game object in the prefab folder.
 * This following code applies tranformation allowing the 
 * Fire object to move in the right direction or left direction, horizontally in x axis.
*/

public class FireProjectile : MonoBehaviour 
{
	


	//Declaire public variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated horizontal x axis speed of the fire's game object.
	[SerializeField] private float speed = 3f;

	//Declaire public variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated fire's projectile distance that 
	//would be fired by 3 x axis distance from Dwarf game object.
	[SerializeField] private float projectileDistance = 3;

	//Declaire variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated game object that is called enemy
	[SerializeField] GameObject enemy;

	//Declaire variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated enemy explosion game object that is called enemyExposion
	[SerializeField] GameObject enemyExposion;

	//Declaire private variable.
	//This variable is assigned to a designated game object that is called Dwarf
	private GameObject Dwarf;

	//Declaire private variable.
	//This variable is assigned to a designated integer of direction variable
	//to indicate whether it is positive or negative value and which direction in x axis it is flipt.
	private int direction;

	//This variable is from Unity the transform component 
	//The Trasform is defined in Unity as position, rotation and scale
	//Here it is going to be used for its coordinates, it is going to be used for the position.
	private Transform _tranform;

	//The private Vector2 is declaired 
	//later to be used to simply 
	//to track this game object's position in x and y axis.
	private Vector2 _currentPosition;

	//The private Vector2 is declaired 
	//later to be used as a initial position
	private Vector2 _initPosition;


	// Use this for initialization
	void Start () 
	{

		//setting up the initial position
		_initPosition = gameObject.transform.position;


		//Here the set up of the Audio Source component. 
		//This Audio Source Component is accessed from this specific game object
		//which the script is attached to, the fire game object in the scene.
		//Basically, from this game object the Get Component is invoked which allows to 
		//access the Audio Source.
		//This is set up so the specific methods could be applied to control 
		//this game object's Audio Source and invoke Play method when appropriate.
		//_hit = gameObject.GetComponent<AudioSource> ();

		//Here the set up of the Tranform. 
		//The Transform is accessed from this specific game object
		//which the script is attached to which is fire game object in the scene.
		//Basically, from this game object the Get Component is invoked which allows to 
		//access the Transform which is scale, rotation and its position.
		//This is set up so the specific methods could be applied to control 
		//this game object's position in the scene.
		_tranform = gameObject.GetComponent<Transform> ();

		//The _currentPosition will track the game object's position
		//here from _transform the position is invoked to access the game 
		//object's position in x and y coordinates
		//Update the game object's position
		_currentPosition = _tranform.position;

		//from Dwarf game object parent find a game object by name that is active called Dwarf in the scene
		//and store it in Dwarf game object variable
		Dwarf = GameObject.Find ("Dwarf");

		//From the Dwarf game object access its compoenent that is called DwarfController 
		//script and access the Dwarf's direction in x horizontal
		//and should store a negatibve 1 or positive 1 depending on the direction of the Dwarf
		//inhorizontal axis. Meaning if its negative 1, Dwarf is facing to the left and should
		//fire to the left subtract vector 2 if positive 1 then right direction add Vector2.
		direction = Dwarf.GetComponent<DwarfController> ().DwarfDirection();

		//direction = 1;

		//Besed on Dwarf direction apply the fire's projectile movement either to the left or right direction horizontally.
		this.gameObject.transform.localScale = new Vector3 (direction, 1, 1);


		
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
		_currentPosition = _tranform.position;


			/*this.gameObject.transform.localScale = new Vector3 (-1, 1, 1);
			_currentPosition -= new Vector2 (speed, 0);

			CheckBounds ();

			_tranform.position = _currentPosition;

		} else 
		{
			this.gameObject.transform.localScale = new Vector3 (1, 1, 1);
			_currentPosition +=new Vector2(speed, 0);

			CheckBounds ();

			_tranform.position = _currentPosition;
		}*/

		//Once the _currentPosition gets updated with the object's 
		//current position with x and y coordinates.
		//Then Vector2 get invoked in order to move this
		//game object in the scene. Based on Dwarf's direction
		//The fire projectile will fire either to the left or right 
		//from the Dwarf direction horizontally.
		_currentPosition += new Vector2 (speed * direction, 0);

		//if the game object has not reached the 
		//x axis boundary point. Then
		//the game object position will be updated.
		//Update the game object's position
		_tranform.position = _currentPosition;

		//Check if this game object has reached its x axis boundary
		//point. If it has the CheckBounds() function gets called.
		CheckBounds ();


		
	}

	//This function destroys the game object when the indicated distance from Dwarf game object is reached
	private void CheckBounds()
	{
		//Cheching if the fire game object has reachedits distance
		if(Mathf.Abs(_initPosition.x-_currentPosition.x) > projectileDistance)
		{
			
			//if it did reched its distance destroy this foregame object
			DestroyObject (this.gameObject);
		}

		/*if(_currentPosition.x > 6 || _currentPosition.x < -6)
		{
			_currentPosition.x = 6;

			DestroyObject (this.gameObject);
		}*/
	}
}
