using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The DoorController.cs script
 * Alisa Ordina
 * id: 100967526
 * November 18, 2017
 * 
 * This script component is attached to the Door platform game object in the scene.
 * This following code applied to the door platform game object. When the Dwarf has completed the game's objective
 * which is retrieve the rare mineral and before to transition to the next scene the door opens to the next level. 
 * This is done through the couroutine maner.
 * 
 * 
*/
public class DoorController : MonoBehaviour 
{

	//Declaration of the public close delay variable that could be edited in Unity's Inspector 
	//in order to define the the delay in seconds which would be appropriate to apply for this Door
	//game object in the scene.
	//[SerializeField] private float closeDelay = 2f;

	//[SerializeField] HUDController gameCtl;

	//set the boolean isOpen variable to false
	private bool isOpen = false;

	//define the contant float variable a point in y axis where the
	//closed position is.
	const float CLOSED = -0.02f;

	//define the contant float variable a point in y axis where the
	//open position is.
	const float OPEN = -1.15f;

	//Checks if the collision between the Door's boundary collider was crossed by the 
	//Dwarf's game object's collider2D 
	public void OnTriggerEnter2D(Collider2D other)
	{
		//Check to see if the game's objection has been fulfilled/completed 
		//Check to see if the Dwarf character has a mineral
		//which is boolean to see if its true,to see if
		//Dwarf has picked up the rare mineral.
		if(Player.Instance.HasMineral)
		{
		//Open ();

		//StartCoroutine ("Open");

		//Checks to see if the door flag is equals to false, which checks if the door has been openedif the door opened has been triggered.
		//if the door is nor open then trigger this condition
		if (!isOpen) 
		{
			//call the couroutine open function
			StartCoroutine ("Open");

			//reset the isOpen boolean flag to true
			isOpen = true;
		}
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//function which triggers to open the door
	private IEnumerator Open()
	{
		//This condition that checks, to see if the door is not opened
		//if the door is not open then trigger this function 
		if (!isOpen) 
		{
			//reset the flag boolean isOpen to true
			//To indicate that the door has been open
			isOpen = true;

			//open the door, apply tranformation with the delay
			for (float p = CLOSED; p > OPEN; p = p - 0.1f)
			{
				//apply the changes
				gameObject.transform.position = new Vector2 (gameObject.transform.position.x, p);

				//delay 0.5 seconds
				yield return new WaitForSeconds (0.5f);

			}

			yield return new WaitForSeconds (0.1f);
			//call courutine close
			StartCoroutine ("Close");


		}

		/*Vector2 pos = gameObject.transform.position;

		for (int i = 0; i < 10; i++) 
		{
			pos = gameObject.transform.position;
			gameObject.transform.position = new Vector2 (pos.x, pos.y - 0.24f);

			yield return new WaitForSeconds (0.1f);
		}

		if (isOpen)
			StartCoroutine ("Close");*/
	}
	//courutine close
	private IEnumerator Close()
	{
		//reset the boolean 
		//isOpen = true;


		//open the door, apply tranformation with the delay
		for (float p = OPEN; p < CLOSED; p = p + 0.1f)
		{
			//aplly the changes
			gameObject.transform.position = new Vector2 (gameObject.transform.position.x, p);

			//delay less then a second
			yield return new WaitForSeconds (0.1f);
		}
		//reset the boolean value isOpen to false
		//this is in order to start coroutine is open again when appropriate.
		isOpen = false;

		/*Vector2 pos = gameObject.transform.position;
		yield return new WaitForSeconds (closeDelay);
		for (int i = 0; i < 10; i++) 
		{
			pos = gameObject.transform.position;

			gameObject.transform.position = new Vector2 (pos.x, pos.y + 0.24f);
			yield return new WaitForSeconds (0.1f);
		}
		isOpen = false;*/
	}

	/*private IEnumerator Close()
	{

		Vector2 pos = gameObject.transform.position;
		yield return new WaitForSeconds (closeDelay);
		for (int i = 0; i < 10; i++) 
		{
			pos = gameObject.transform.position;

			gameObject.transform.position = new Vector2 (pos.x, pos.y + 0.24f);
			yield return new WaitForSeconds (0.1f);
		}
		isOpen = false;
	}*/


}
