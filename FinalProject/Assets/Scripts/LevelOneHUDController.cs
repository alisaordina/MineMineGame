using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * The LevelOneHUDController.cs script
 * Alisa Ordina
 * id: 100967526
 * November 23, 2017
 * 
 * This script component is attached to Canvas game object in the scene for the level One.
 * This script will connect with UI interface and keeps track of user's points.
 * To be able to access these UI elements the UnityEngine.UI class is imported
 * by coding: using UnityEngine.UI
 * This following code applies updates to the 
 * timmer label, lowest time label. This code applies
 * the changes to the UI display and display specific labels 
 * when appropriate for level one only.
 * 
*/
public class LevelOneHUDController : MonoBehaviour 
{

	//Declaire variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated timeToBeatLabel game object of the Canvas UI.
	[SerializeField] private Text timeToBeatLabel = null;

	//Declaire variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated yourTimeLabel game object of the Canvas UI.
	[SerializeField] private Text yourTimeLabel = null;

	// Use this for initialization
	void Start () 
	{

		//The float T is storing the current Lowest Time counter variable form the Player class
		float t = Player.Instance.LowestTime; 

		//Then parse the t into a string into a more friendly display string that is more readle to the user
		//into a more disirable view.
		string minutes = ((int)t / 60).ToString ();
		string seconds = (t % 60).ToString ("f2");

		//Then store the minutes and seconds into a timeToBeatLabel label in the UI.
		timeToBeatLabel.text = "Lowest Time: " +minutes+ ":" +seconds;

		//The float T is storing the current user's timmer, from the YourTime 
		//counter variable form the Player class
		t = Player.Instance.YourTime; 

		//Then parse the t into a string into a more friendly display string that is more readle to the user
		//into a more disirable view.
		minutes = ((int)t / 60).ToString ();
		seconds = (t % 60).ToString ("f2");

		//Then store the minutes and seconds into a yourTimeLabel label in the UI.
		yourTimeLabel.text = "Your Time: " + minutes + ":" + seconds;
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
