using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTwoHUDController : MonoBehaviour 
{

	[SerializeField] private Text lowestLabel;

	[SerializeField] private Text yourTimeLabel;

	// Use this for initialization
	void Start () 
	{
		float t = Player.Instance.LowestTimeTwo; 


		string minutes = ((int)t / 60).ToString ();
		string seconds = (t % 60).ToString ("f2");

		lowestLabel.text = "Lowest Time: " +minutes+ ":" +seconds;

		t = Player.Instance.YourTime; 


		minutes = ((int)t / 60).ToString ();
		seconds = (t % 60).ToString ("f2");

		yourTimeLabel.text = "Your Time: " + minutes + ":" + seconds;

	}

	// Update is called once per frame
	void Update () {

	}
}
