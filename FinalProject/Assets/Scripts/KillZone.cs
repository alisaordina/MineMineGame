using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour {

	// Use this for initialization
	[SerializeField] Transform spawnPoint;

	public void OnCollisionEnter2D(Collision2D other)
	{

		other.gameObject.transform.position = spawnPoint.position;
	}
}
