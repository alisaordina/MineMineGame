using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour 
{

	[SerializeField] private float closeDelay = 2f;

	//[SerializeField] HUDController gameCtl;

	private bool isOpen = false;

	const float CLOSED = -0.02f;

	const float OPEN = -1.15f;

	public void OnTriggerEnter2D(Collider2D other)
	{
		if(Player.Instance.HasMineral)
		{
		//Open ();
		StartCoroutine ("Open");
		/*if (!isOpen) 
		{
			StartCoroutine ("Open");
			isOpen = true;
		}*/
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private IEnumerator Open()
	{

		if (!isOpen) 
		{
			isOpen = true;

			for (float p = CLOSED; p > OPEN; p = p - 0.1f)
			{
				gameObject.transform.position = new Vector2 (gameObject.transform.position.x, p);

				yield return new WaitForSeconds (0.5f);

			}

			//yield return new WaitForSeconds (0.1f);

			//StartCoroutine ("Close");


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

	private IEnumerator Close()
	{

		//isOpen = true;

		for (float p = OPEN; p < CLOSED; p = p + 0.1f)
		{
			gameObject.transform.position = new Vector2 (gameObject.transform.position.x, p);

			yield return new WaitForSeconds (0.1f);
		}

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
