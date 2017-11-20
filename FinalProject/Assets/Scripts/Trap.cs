using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour 
{

	[SerializeField] GameObject explosion =null;


	[SerializeField] HUDController gameCtl;



	void OnTriggerEnter2D(Collider2D other)
	{

		if (other.gameObject.tag.Equals ("Player")) 
		{

			DwarfController Dc = other.gameObject.GetComponent<DwarfController> ();



			if (Dc != null) 
			{


				Instantiate (explosion).GetComponent<Transform> ().position = this.gameObject.GetComponent<Transform> ().position;

				Dc.Death ();

				gameCtl.GetComponent<HUDController> ().DeadGameOver ();



			}

		}
	}


}
