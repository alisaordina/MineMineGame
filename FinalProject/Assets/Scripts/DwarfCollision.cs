using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * The DwarfCollision.cs script
 * Alisa Ordina
 * id: 100967526
 * November 20, 2017
 * 
 * This script component is attached to Dwarf game Object in the scene.
 * This following code applies collision function to this game object.
 * This adds effects to the Dwarf game object when the collision 
 * between the Dwarf and enemy/monster happends and deducts life counter from the gameCtl game object which is attached to canvas.
 * And other collisions also adds points, when the dwarf has collided with the mineral and gold coins in the scene.
*/


public class DwarfCollision : MonoBehaviour 
{

	//Declaire variable that would be accessible to Unity Inspector.
	//This variable is assigned to a designated game object that is called HUDController
	//from HUDController script that is attached to canvas game object
	//[SerializeField] HUDController gameCtl = null;

	//For debug purposes
	//public bool hasJewel = false;

	//This is declaration of the collection/array of the audio sounds
	private AudioSource[] _audioSources;

	//This is declaration of audio source for score sound
	private AudioSource _scoreSound;

	//This is declaration of audio source for explosion sound
	private AudioSource _explosionSound;

	//This is declaration of audio source for the mineral sound
	private AudioSource _mineralSound;

	//This is declaration of the monster control for its monster control script
	//that is a script component that is attached to the monster game object in the scene.
	private MonsterController _monsterCtl;

	//This is declaration of the dwarf control for its dwarf control script
	//that is a script component that is attached to the dwarf game object in the scene.
	private DwarfController _dwarfCtrl;


	// Use this for initialization
	void Start () 
	{

		//Here the set up the collections/components of the Audio Source. 
		//This Audio Source Components is accessed from this specific game object
		//which this script is attached to Dwarf game object in the scene.
		//Basically, from this game object, the Get Components is invoked which allows to 
		//access the Audio Source.
		//This set up, so the specific methods could be applied in order to control 
		//this game object's Audio Sources and invoke Play method when appropriate.
		this._audioSources = gameObject.GetComponents<AudioSource>();

		//building the collection/array of sound souces in the specific order of the specific sound source
		//This is declaration for score/gold piece sound
		this._scoreSound = this._audioSources [0];

		//building the collection/array of sound souces in the specific order of the specific sound source
		//This is declaration for the explosion sound
		this._explosionSound = this._audioSources [1];

		//building the collection/array of sound souces in the specific order of the specific sound source
		//This is declaration for the mineral sound
		this._mineralSound = this._audioSources [2];



	}

	// Update is called once per frame
	void Update () 
	{

	}

	//This is the function that would detect the collision with
	//this Dwarf game object. When Unity calculates that the collider2D object
	//has intercepted with a different game object this function would be executed.
	//This is the fact that when the Dwarf object has intercepted with another
	//game object it triggered (intercepted) with each other.
	//This function is executed and the follwoing conditions are invoked.
	public void OnTriggerEnter2D(Collider2D other)
	{

		//When the tags are created onto specific game objects, this is where
		//the tags are taken onto the advantage.
		//When the game object's tag is equal to 'trap' tag and 
		//when the collision happends, the interception with the another game object happends
		//this function is triggered and the condition statements 
		//are invoked which compares the game object's tags.
		//When the game object's tag is equal to 'trap' tag
		if (other.gameObject.tag.Equals ("Trap")) 
		{
			
			//If the Dwarf game object's explosionSound Audio Source is assigned
			//and does not equal to null,
			//then this function gets invoked with its statement.
			if (_explosionSound != null) 
			{
				//If the Dwarf game object has explosionSound Audio Source component then 
				//play its attached audio clip
				_explosionSound.Play ();

			}

		}

		//Access this game object's component script under name DwarfController script 
		//and stored it under variable _dwarfCtl
		_dwarfCtrl = this.gameObject.GetComponent<DwarfController> ();

		//If the dwarfCtrl component is assigned and is not null
		//then execute the stamement
		if (_dwarfCtrl != null) 
		{

			//if in the dwarf controller script that stored in dwarfCtrl
			//variable if the Dwarf is not isDwarfDead meaning not dead execute 
			//under this statement
			if (!_dwarfCtrl.isDwarfDead()) 
			{
				
				//When the game object's tag is equal to gold_piece
				if (other.gameObject.tag.Equals ("gold_piece")) 
				{

					//If it is true and the game object has intercepted the 
					//gold_piece game object with its defined tag of gold_piece.
					//Then log it on to the screen that the intercept
					//has happened with the gold_piece.
					Debug.Log ("Collision With The Gold Piece\n");

					//then destroy the gold_piece game object from the scene
					Destroy (other.gameObject);

					//If the Dwarf game object's scoreSound Audio Source is assigned
					//and does not equal to null,
					//then this function gets invoked with its statement.
					if (_scoreSound != null) 
					{
						//If the Dwarf has Audio Source component is set up then 
						//play its attached audio clip
						_scoreSound.Play ();

					}
					//When the Dwarf has intercepted with the
					//gold_piece game object. The Dwarf's points 
					//gets 20 points added to its score counter,
					//to its current score.
					//Since the HUDController script has access to the Player
					//class. The Player's public property could be
					//used to update the score counter and increase the value
					//by 20 points.
					Player.Instance.Score += 20;

				}


				//When the game object's tag is equal to 'mineral' tag
				if (other.gameObject.tag.Equals ("mineral")) 
				{

					//If it is true and the game object has intercepted the 
					//mineral game object with its defined tag of mineral.
					//Then log it on to the screen that the intercept
					//has happened with the mineral.
					Debug.Log ("Collision With The mineral\n");

					//then destroy the mineral game object from the scene
					Destroy (other.gameObject);

					//for debug purposes chandes hasmineral boolean to true
					//hasJewel = true;

					//set the boolean flag has mineral to true.
					Player.Instance.HasMineral = true;

					//If the Dwarf game object's mineralSound Audio Source is assigned
					//and does not equal to null,
					//then this function gets invoked with its statement.
					if (_mineralSound != null) 
					{
						//If the Dwarf game object's has Audio Source mineralSound component set up then 
						//play its attached audio clip
						_mineralSound.Play ();

					}


					//When the Dwarf has intercepted with the
					//mineral game object. The Dwarf's points 
					//gets 500 points added to its score counter,
					//to its current score.
					//Since the HUDController script has access to the Player
					//class. The Player's public property could be
					//used to update the score counter and increase the value
					//by 500 points.
					Player.Instance.Score += 500;

				}



		//If the collision happends, the interception with another game object's 
		//defined tag as enemy this function is triggered and the condition statements 
		//are invoked which compares the game object's tags.
		//When the game object's tag is equal to an enemy.
		else if (other.gameObject.tag.Equals ("enemy")) 
				{

					//other.gameObject.GetComponent<CapsuleCollider2D> ().enabled = false;

					//If it is true and the game object has intercepted the 
					//enemy game object with its defined tag of enemy. 
					//Then log it on to the screen that the intercept
					//has happened with the enemy.
					Debug.Log ("Collision With The Enemy\n");

					//Since the Dwarf 
					//has intercepted with the enemy game object 
					//the Dwarf's life would be decressed by 20 points.
					//Since the HUDController script has access to the Player
					//class. The Player's public property could be
					//used to update the life counter and decrese the value
					//by 20 points.
					Player.Instance.Life -= 20;

					//Since the Dwarf
					//has intercepted with the enemy game object 
					//the coroutine function Blink() would be called, only if the Dwarf game object is active.
					//Which adds color to the Dwarf material to make the
					//game object blink with its apprence.
					//Calling simply by startCoroutine(function name)
					//by its name simply calls the coroutine function.
					if (this.gameObject.activeSelf) 
					{
						//calling the coroutine function called blink.
						StartCoroutine ("Blink");
					}

				} 

		//If the collision happends, the interception with another game object's 
		//defined tag as Monster this function is triggered and the condition statements 
		//are invoked which compares the game object's tags.
		//When the game object's tag is equal to Monster.
		else if (other.gameObject.tag.Equals ("Monster"))
				{
					//Then access the Monster's game object's script component called MonsterController script that is attached
					//to Monster game object and stored it under variable _monsterCtl
					_monsterCtl = other.gameObject.GetComponent<MonsterController> ();

					//If it is true and the game object has intercepted, the 
					//tag named 'Monster' game object with its defined tag of Monster. 
					//Then log it on to the screen that the intercept
					//has happened with the Monster.
					Debug.Log ("Collision With The Monster\n");

					//If the _monsterCtl component is assigned and is not null
					//then execute the stamement
					if (_monsterCtl!= null) 
					{

						//if in the monster controller script that stored in _monsterCtl
						//variable if the Moster is not isPassed meaning not dead execute 
						//under this statement
						if (!_monsterCtl.isPassed ()) 
						{

							//Since the Dwarf game object
							//has intercepted with the Monster game object 
							//the Dwarf's life would be decressed by 30 points.
							//Since the HUDController script has access to the Player
							//class. The Player's public property could be
							//used to update the life counter and decrese the value
							//by 30 points.
							Player.Instance.Life -= 30;

							//Since the Dwarf
							//has intercepted with the Monster game object 
							//the coroutine function Blink() would be called, only if the Dwarf game object is active.
							//Which adds color to the Dwarf material to make the
							//game object blink with its apprence.
							//Calling simply by startCoroutine(function name)
							//by its name simply calls the coroutine function.
							if (this.gameObject.activeSelf) 
							{
								//calling the coroutine function called blink.
								StartCoroutine ("Blink");
							}
						}
					}
				}
			}
		}

	}

	//This function will consist of fading the
	//game object it means gradually reducing 
	//an object's alpha (opasity) value until it because invisible
	//The in order for the fading to be visible alpha must be reduced over
	//a sequence of frames and then after curtain seconds this game object
	//will appear.
	//The coroutine is applied that returns the
	//IENumerator in order to step in and out and to
	//do the blinking of this game object.
	private IEnumerator Blink()
	{
		//declairing color
		Color c;

		//accessing this game object's components which is render
		//and storing it in the renderer variable.
		//Render is a component that allows to access and modify how the
		//sprite is rendered on the screen.
		//The renderer has a material which has colors that is 
		//can be painted over the game object. This material handles its colors.
		//So, through the render's material, can access the color
		//and play with the invisibility/transperancy apha (opasity) of this game object
		//of this sprite.
		Renderer renderer = gameObject.GetComponent<Renderer> ();

		//So this code starts with fully opaque 
		//Also this code delays the blinking 
		//by invoking waitforseconds
		//This slows the blinking down.
		//The blinking will happened only 3 times.
		for(int i =0; i <3; i++)
		{
			//since the invisibility in invoked the
			//whole loop in one frame rate the second
			//loop is also initialized and in the
			//next loop in the next frame the object should be visible
			for(float f = 1f; f >= 0; f -= 0.1f)
			{

				//accessing the material from the 
				//specific game object from its renderer
				//component.
				c = renderer.material.color;

				//setting up, updating its material's color of that game object
				c.a = f;

				//apply the updated color to the game object
				renderer.material.color = c;

				//Yield allows to get back to this loop 
				//it is similar to step in and out from the loop.
				yield return new WaitForSeconds (.1f);
			}
			//Apllying visibility to the game object to 
			//make it visible in the next frame.
			for(float f = 0; f <= 1; f +=0.1f)
			{

				//accessing the material from the 
				//specific game object from its renderer
				//component.
				c = renderer.material.color;

				//setting up, updating its material's color of that game object
				c.a = f;

				//apply the updated color to the game object
				renderer.material.color = c;

				//Yield allows to get back to this loop 
				//it is similar to step in and out from the loop.
				yield return new WaitForSeconds (.1f);
			}
		}
	}
}
