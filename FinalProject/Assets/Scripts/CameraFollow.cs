using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	[SerializeField] Transform target;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () 
	{

		gameObject.transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

	}
}
