using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTwoStats : MonoBehaviour {

	//Declaire variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated game object that is called HUDController
	//from HUDController script that is attached to canvas game object
	[SerializeField] HUDController gameCtl;
	// Use this for initialization
	void Start () 
	{
		
		//Player.Instance.IsLevelUp = true;
		Player.Instance.Life=100;


	}

	// Update is called once per frame
	void Update () {

	}
}
