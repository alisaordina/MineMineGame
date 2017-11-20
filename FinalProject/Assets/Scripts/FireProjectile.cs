using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour 
{
	
	private Transform _tranform;

	private Vector2 _currentPosition;

	[SerializeField] private float speed = 3f;

	[SerializeField] private float projectileDistance = 3;

	[SerializeField] GameObject enemy;

	[SerializeField] GameObject enemyExposion;

	private GameObject Dwarf;

	private int direction;

	private Vector2 _initPosition;


	// Use this for initialization
	void Start () 
	{

		_initPosition = gameObject.transform.position;

		_tranform = gameObject.GetComponent<Transform> ();

		_currentPosition = _tranform.position;

		Dwarf = GameObject.Find ("Dwarf");
		direction = Dwarf.GetComponent<DwarfController> ().DwarfDirection();

		//direction = 1;

		this.gameObject.transform.localScale = new Vector3 (direction, 1, 1);


		
	}
	
	// Update is called once per frame
	void Update () 
	{

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

		_currentPosition += new Vector2 (speed * direction, 0);


		_tranform.position = _currentPosition;

		CheckBounds ();


		if (this.gameObject.transform.position == enemy.gameObject.GetComponent<Transform> ().position) 
		{

			Instantiate (enemyExposion).GetComponent<Transform> ().position = enemy.gameObject.GetComponent<Transform> ().position;

			DestroyObject (enemy);
			Destroy (this.gameObject);
		}
		
	}

	private void CheckBounds()
	{
		if(Mathf.Abs(_initPosition.x-_currentPosition.x) > projectileDistance)
		{
			

			DestroyObject (this.gameObject);
		}

		/*if(_currentPosition.x > 6 || _currentPosition.x < -6)
		{
			_currentPosition.x = 6;

			DestroyObject (this.gameObject);
		}*/
	}
}
