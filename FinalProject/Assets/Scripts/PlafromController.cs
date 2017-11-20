using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlafromController : MonoBehaviour 
{


	[SerializeField] private float speed = 5f;

	[SerializeField] private float startY;

	[SerializeField] private float LowY;
	//-6.82
	[SerializeField] private float CurrentY;


	private Transform _transform;


	private Vector2 _currentPosition;

	private bool isGoingDown = true;




	// Use this for initialization
	void Start () 
	{
		
		_transform = gameObject.GetComponent<Transform> ();


		_currentPosition = _transform.position;


		//moveUp();


	}

	// Update is called once per frame
	void Update () 
	{



		_currentPosition = _transform.position;





		CurrentY = _currentPosition.y;

		if (isGoingDown) {
			_currentPosition -= new Vector2 (0, speed);
			if (_currentPosition.y < LowY) {
				isGoingDown = false;
			}
		} else {
			_currentPosition += new Vector2 (0, speed);
			if (_currentPosition.y > startY) {
				isGoingDown = true;
			}
		}

		_transform.position = _currentPosition;


		

	}





}
