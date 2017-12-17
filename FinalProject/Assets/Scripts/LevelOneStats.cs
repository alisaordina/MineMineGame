using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The LevelOneStats.cs script
 * Alisa Ordina
 * id: 100967526
 * November 18, 2017
 * 
 * This script component is attached to the Dwarf game object in thelevel one,
 * this script is the Dwarf's/Player's Level One Status.
 * This is where all the Dwarf's level One powers are stored.
 * For example if this Player/Dwarf would level up this level one 
 * Dwarf's power status would be updated.
 * 
 * 
*/

public class LevelOneStats : MonoBehaviour 
{


	// Use this for initialization
	void Start () 
	{

		//This level one status update when game starts with the Dwarf's Life counter starting at 50%
		Player.Instance.Life = 50;

	}

	// Update is called once per frame
	void Update () 
	{

	}
}
