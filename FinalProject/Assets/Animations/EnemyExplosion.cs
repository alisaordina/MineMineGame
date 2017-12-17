using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The EnemyExplosion.cs script
 * Alisa Ordina
 * id: 100967526
* November 17, 2017
 * 
 * The following script component is attached to enemy explosion gameObject in the prefab folder.
 * This following code applies the destroy method of this object 
 * in order to destroy this game object and remove it from the scene.
 * This is done at the end of the last explosion's animation frame
*/

public class EnemyExplosion : MonoBehaviour 
{

	public void DestroyMe()
	{
		//destroys, removes this blue explosion game object from the scene.
		Destroy (gameObject);
	}
}
