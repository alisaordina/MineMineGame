﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosion : MonoBehaviour 
{

	public void DestroyMe()
	{
		//destroys, removes this blue explosion game object from the scene.
		Destroy (gameObject);
	}
}
