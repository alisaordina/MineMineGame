using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour 
{

	[SerializeField] HUDController gameCtl;
	 static int level = 0;

	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag.Equals ("Player")) 
		{
			if (Player.Instance.HasMineral) 
			{
				Player.Instance.HasWon = true;
				StartScene ();
			}
			/*if (other.gameObject.GetComponent<DwarfCollision> ().hasJewel) 
			{
				StartScene ();
			}*/
		}
	}

	public void StartScene()
	{

		//Application.LoadLevel (level++);

		SceneManager.LoadScene (level++);

		//SceneManager.GetSceneAt (3);
	}
}
