using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelOneHUDController : MonoBehaviour 
{
	[SerializeField] private Text timeToBeatLabel;

	[SerializeField] private Text yourTimeLabel;

	// Use this for initialization
	void Start () 
	{
		float t = Player.Instance.LowestTime; 


		 string minutes = ((int)t / 60).ToString ();
		string seconds = (t % 60).ToString ("f2");

		timeToBeatLabel.text = "Lowest Time: " +minutes+ ":" +seconds;

		t = Player.Instance.YourTime; 


		 minutes = ((int)t / 60).ToString ();
		seconds = (t % 60).ToString ("f2");

		yourTimeLabel.text = "Your Time: " + minutes + ":" + seconds;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
