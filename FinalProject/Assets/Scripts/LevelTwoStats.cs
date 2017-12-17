using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The LevelTwoStats.cs script
 * Alisa Ordina
 * id: 100967526
 * November 18, 2017
 * 
 * This script component is attached to the Dwarf game object in the Level 2,
 * this script is the Dwarf's/Player's Level Two Status.
 * This is where all the Dwarf's level Two powers are stored.
 * For example if this Player/Dwarf would level up this level two 
 * Dwarf's power status would be updated.
 * 
 * 
*/
public class LevelTwoStats : MonoBehaviour 
{

	//Declaire variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated game object that is called HUDController
	//from HUDController script that is attached to canvas game object
	[SerializeField] HUDController gameCtl = null;

	// Use this for initialization
	void Start () 
	{
		//used previously
		//----------------------------------
		//Player.Instance.IsLevelUp = true;
		//----------------------------------

		//This level two status update when the game starts with the Dwarf's Life counter starting at 100%
		Player.Instance.Life = 100;


	}

	// Update is called once per frame
	void Update () {

	}
}
